import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthorsService } from '../authors.service';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthorCreateDto, UpdateAuthorDto } from '../../../core/models/author.model';

@Component({
    selector: 'app-author-form',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatCheckboxModule
    ],
    templateUrl: './author-form.component.html',
    styleUrl: './author-form.component.scss'
})
export class AuthorFormComponent implements OnInit {
    authorForm: FormGroup;
    isEditMode = false;
    authorId: string | null = null;
    loading = false;

    constructor(
        private fb: FormBuilder,
        private authorsService: AuthorsService,
        private notificationService: NotificationService,
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.authorForm = this.fb.group({
            auFname: ['', [Validators.required, Validators.maxLength(20)]],
            auLname: ['', [Validators.required, Validators.maxLength(40)]],
            phone: ['', Validators.required],
            address: [''],
            city: [''],
            contract: [false]
        });
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.isEditMode = true;
                this.authorId = params['id'];
                this.loadAuthor(params['id']);
            }
        });
    }

    loadAuthor(id: string): void {
        this.loading = true;
        this.authorsService.getAuthor(id).subscribe({
            next: (author: any) => {
                // Map PascalCase (or camelCase) DTO to camelCase form controls
                // Using 'any' type for author to allow flexible property access
                this.authorForm.patchValue({
                    auFname: author.AuFname || author.auFname,
                    auLname: author.AuLname || author.auLname,
                    phone: author.Phone || author.phone,
                    address: author.Address || author.address,
                    city: author.City || author.city,
                    contract: author.Contract !== undefined ? author.Contract : author.contract
                });
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    onSubmit(): void {
        if (this.authorForm.valid) {
            this.loading = true;
            const formValue = this.authorForm.getRawValue();

            if (this.isEditMode) {
                const updateDto: UpdateAuthorDto = {
                    Id: this.authorId!,
                    AuFname: formValue.auFname,
                    AuLname: formValue.auLname,
                    Phone: formValue.phone,
                    Address: formValue.address,
                    City: formValue.city,
                    Contract: formValue.contract
                };
                this.authorsService.updateAuthor(updateDto).subscribe({
                    next: () => {
                        this.notificationService.success('Yazar başarıyla güncellendi');
                        this.router.navigate(['/authors']);
                    },
                    error: (err) => {
                        console.error('Güncelleme hatası:', err);
                        this.notificationService.error('Yazar güncellenirken bir hata oluştu');
                        this.loading = false;
                    }
                });
            } else {
                const createDto: AuthorCreateDto = {
                    AuFname: formValue.auFname,
                    AuLname: formValue.auLname,
                    Phone: formValue.phone,
                    Address: formValue.address,
                    City: formValue.city,
                    Contract: formValue.contract
                };
                this.authorsService.createAuthor(createDto).subscribe({
                    next: () => {
                        this.notificationService.success('Yazar başarıyla eklendi');
                        this.router.navigate(['/authors']);
                    },
                    error: () => {
                        this.loading = false;
                    }
                });
            }
        }
    }

    cancel(): void {
        this.router.navigate(['/authors']);
    }
}
