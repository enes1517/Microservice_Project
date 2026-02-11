import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProductsService } from '../products.service';
import { NotificationService } from '../../../core/services/notification.service';
import { TitleDto } from '../../../core/models/product.model';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';

@Component({
    selector: 'app-products-list',
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatDialogModule,
        MatInputModule,
        MatFormFieldModule,
        MatSelectModule,
        MatPaginatorModule,
        MatChipsModule,
        MatProgressSpinnerModule
    ],
    templateUrl: './products-list.component.html',
    styleUrl: './products-list.component.scss'
})
export class ProductsListComponent implements OnInit, OnDestroy {
    products: TitleDto[] = [];
    displayedColumns: string[] = ['id', 'title', 'type', 'price', 'royalty', 'actions'];
    loading = false;

    // Pagination
    pageNumber = 1;
    pageSize = 10;
    pageSizeOptions = [10, 25, 50, 100];
    totalRecords = 0;

    // Filtreleme formu
    filterForm: FormGroup;

    // Kitap türleri
    bookTypes = [
        'business',
        'mod_cook',
        'popular_comp',
        'psychology',
        'trad_cook',
        'UNDECIDED'
    ];

    // Active filters
    activeFilters: string[] = [];

    private destroy$ = new Subject<void>();

    constructor(
        private productsService: ProductsService,
        private notificationService: NotificationService,
        private dialog: MatDialog,
        private fb: FormBuilder
    ) {
        this.filterForm = this.fb.group({
            title: [''],
            type: [''],
            price: [null]
        });
    }

    ngOnInit(): void {
        this.loadProducts();
        this.setupFilterListeners();
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    setupFilterListeners(): void {
        // Title arama için debounce
        this.filterForm.get('title')?.valueChanges
            .pipe(
                debounceTime(500),
                distinctUntilChanged(),
                takeUntil(this.destroy$)
            )
            .subscribe(() => {
                this.pageNumber = 1;
                this.loadProducts();
            });

        // Type değişikliği
        this.filterForm.get('type')?.valueChanges
            .pipe(takeUntil(this.destroy$))
            .subscribe(() => {
                this.pageNumber = 1;
                this.loadProducts();
            });

        // Price değişikliği
        this.filterForm.get('price')?.valueChanges
            .pipe(
                debounceTime(500),
                distinctUntilChanged(),
                takeUntil(this.destroy$)
            )
            .subscribe(() => {
                this.pageNumber = 1;
                this.loadProducts();
            });
    }

    loadProducts(): void {
        this.loading = true;
        this.updateActiveFilters();

        const filterParams = {
            pageNumber: this.pageNumber,
            pageSize: this.pageSize,
            title: this.filterForm.get('title')?.value || undefined,
            type: this.filterForm.get('type')?.value || undefined,
            price: this.filterForm.get('price')?.value || undefined
        };

        this.productsService.getProductsWithFilters(filterParams).subscribe({
            next: (data) => {
                // Backend artık TitleDto ve TotalCount döndürüyor
                this.products = data.items;
                this.totalRecords = data.totalCount;
                this.loading = false;
            },
            error: (error) => {
                console.error('Ürünler yüklenirken hata:', error);
                this.notificationService.error('Ürünler yüklenirken bir hata oluştu');
                this.loading = false;
            }
        });
    }

    onPageChange(event: PageEvent): void {
        this.pageNumber = event.pageIndex + 1;
        this.pageSize = event.pageSize;
        this.loadProducts();
    }

    updateActiveFilters(): void {
        this.activeFilters = [];

        const title = this.filterForm.get('title')?.value;
        if (title) {
            this.activeFilters.push(`Başlık: ${title}`);
        }

        const type = this.filterForm.get('type')?.value;
        if (type) {
            this.activeFilters.push(`Tür: ${type}`);
        }

        const price = this.filterForm.get('price')?.value;
        if (price) {
            this.activeFilters.push(`Fiyat: ${price}`);
        }
    }

    clearFilters(): void {
        this.filterForm.reset();
        this.pageNumber = 1;
        this.activeFilters = [];
        this.loadProducts();
    }

    deleteProduct(id: string): void {
        if (confirm('Bu kitabı silmek istediğinizden emin misiniz?')) {
            this.productsService.deleteProduct(id).subscribe({
                next: () => {
                    this.notificationService.success('Kitap başarıyla silindi');
                    this.loadProducts();
                },
                error: () => {
                    this.notificationService.error('Kitap silinirken bir hata oluştu');
                }
            });
        }
    }
}

