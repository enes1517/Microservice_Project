import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ProductsService } from '../products.service';
import { NotificationService } from '../../../core/services/notification.service';
import { CreateTitleDto, UpdateTitleDto } from '../../../core/models/product.model';

@Component({
    selector: 'app-product-form',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatDatepickerModule,
        MatNativeDateModule
    ],
    templateUrl: './product-form.component.html',
    styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {
    productForm: FormGroup;
    isEditMode = false;
    productId: string | null = null;
    loading = false;

    constructor(
        private fb: FormBuilder,
        private productsService: ProductsService,
        private notificationService: NotificationService,
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.productForm = this.fb.group({
            title: ['', [Validators.required, Validators.maxLength(80)]],
            type: ['', [Validators.required, Validators.maxLength(12)]],
            price: [null, [Validators.min(0), Validators.max(10000)]],
            royalty: [null, [Validators.min(0), Validators.max(100)]],
            notes: ['', Validators.maxLength(200)],
            pubdate: [new Date(), Validators.required]
        });
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.isEditMode = true;
                this.productId = params['id'];
                this.loadProduct(params['id']);
            }
        });
    }

    loadProduct(id: string): void {
        this.loading = true;
        this.productsService.getProduct(id).subscribe({
            next: (product) => {
                console.log('🔍 Backend\'den gelen product:', product);
                console.log('🔍 product.title değeri:', product.title);
                this.productForm.patchValue(product);
                console.log('🔍 Form patchValue sonrası title:', this.productForm.get('title')?.value);
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    onSubmit(): void {
        if (this.productForm.valid) {
            this.loading = true;
            const formValue = this.productForm.getRawValue();

            if (this.isEditMode) {
                const updateDto: UpdateTitleDto = {
                    id: this.productId!,
                    ...formValue
                };
                console.log('📤 Backend\'e gönderilecek updateDto:', updateDto);
                console.log('📤 updateDto.title değeri:', updateDto.title);
                this.productsService.updateProduct(updateDto).subscribe({
                    next: () => {
                        this.notificationService.success('Kitap başarıyla güncellendi');
                        this.router.navigate(['/products']);
                    },
                    error: () => {
                        this.loading = false;
                    }
                });
            } else {
                const createDto: CreateTitleDto = formValue;
                this.productsService.createProduct(createDto).subscribe({
                    next: () => {
                        this.notificationService.success('Kitap başarıyla eklendi');
                        this.router.navigate(['/products']);
                    },
                    error: () => {
                        this.loading = false;
                    }
                });
            }
        }
    }

    cancel(): void {
        this.router.navigate(['/products']);
    }
}
