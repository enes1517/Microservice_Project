import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthorsService } from '../authors.service';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthorViewDto } from '../../../core/models/author.model';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';

@Component({
    selector: 'app-authors-list',
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        ReactiveFormsModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatFormFieldModule,
        MatSelectModule,
        MatPaginatorModule,
        MatChipsModule,
        MatProgressSpinnerModule
    ],
    templateUrl: './authors-list.component.html',
    styleUrl: './authors-list.component.scss'
})
export class AuthorsListComponent implements OnInit, OnDestroy {
    authors: any[] = [];
    displayedColumns: string[] = ['fullName', 'location', 'phone', 'actions'];
    loading = false;

    // Pagination
    pageNumber = 1;
    pageSize = 10;
    pageSizeOptions = [10, 25, 50, 100];
    totalRecords = 0;

    // Filtreleme formu
    filterForm: FormGroup;

    // Şehir listesi (örnek - backend'den gelebilir)
    cities = [
        'Oakland',
        'Berkeley',
        'San Jose',
        'Palo Alto',
        'Menlo Park',
        'Corvallis',
        'Salt Lake City',
        'Rockville',
        'Vacaville',
        'Covelo',
        'Lawrence',
        'Nashville',
        'Ann Arbor',
        'Gary',
        'Walnut Creek'
    ];

    // Active filters
    activeFilters: string[] = [];

    private destroy$ = new Subject<void>();

    constructor(
        private authorsService: AuthorsService,
        private notificationService: NotificationService,
        private fb: FormBuilder
    ) {
        this.filterForm = this.fb.group({
            fullname: [''],
            location: ['']
        });
    }

    ngOnInit(): void {
        this.loadAuthors();
        this.setupFilterListeners();
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    setupFilterListeners(): void {
        // Fullname arama için debounce
        this.filterForm.get('fullname')?.valueChanges
            .pipe(
                debounceTime(500),
                distinctUntilChanged(),
                takeUntil(this.destroy$)
            )
            .subscribe(() => {
                this.pageNumber = 1;
                this.loadAuthors();
            });

        // Location değişikliği
        this.filterForm.get('location')?.valueChanges
            .pipe(takeUntil(this.destroy$))
            .subscribe(() => {
                this.pageNumber = 1;
                this.loadAuthors();
            });
    }

    loadAuthors(): void {
        this.loading = true;
        this.updateActiveFilters();

        const filterParams = {
            pageNumber: this.pageNumber,
            pageSize: this.pageSize,
            fullname: this.filterForm.get('fullname')?.value || undefined,
            location: this.filterForm.get('location')?.value || undefined
        };

        this.authorsService.getAuthorsWithFilters(filterParams).subscribe({
            next: (data) => {
                this.authors = data.items;
                this.totalRecords = data.totalCount;
                this.loading = false;
            },
            error: (error) => {
                console.error('Yazarlar yüklenirken hata:', error);
                this.notificationService.error('Yazarlar yüklenirken bir hata oluştu');
                this.loading = false;
            }
        });
    }

    onPageChange(event: PageEvent): void {
        this.pageNumber = event.pageIndex + 1;
        this.pageSize = event.pageSize;
        this.loadAuthors();
    }

    updateActiveFilters(): void {
        this.activeFilters = [];

        const fullname = this.filterForm.get('fullname')?.value;
        if (fullname) {
            this.activeFilters.push(`İsim: ${fullname}`);
        }

        const location = this.filterForm.get('location')?.value;
        if (location) {
            this.activeFilters.push(`Şehir: ${location}`);
        }
    }

    clearFilters(): void {
        this.filterForm.reset();
        this.pageNumber = 1;
        this.activeFilters = [];
        this.loadAuthors();
    }

    deleteAuthor(id: string): void {
        if (confirm('Bu yazarı silmek istediğinizden emin misiniz?')) {
            this.authorsService.deleteAuthor(id).subscribe({
                next: () => {
                    this.notificationService.success('Yazar başarıyla silindi');
                    this.loadAuthors();
                },
                error: () => {
                    this.notificationService.error('Yazar silinirken bir hata oluştu');
                }
            });
        }
    }
}
