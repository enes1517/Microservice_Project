import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { ProductsService } from '../products/products.service';
import { AuthorsService } from '../authors/authors.service';
import { TitleDto } from '../../core/models/product.model';
import { AuthorViewDto } from '../../core/models/author.model';

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, RouterLink],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
    totalBooks = 0;
    totalAuthors = 0;
    recentBooks: TitleDto[] = [];
    recentAuthors: AuthorViewDto[] = [];
    loading = true;

    constructor(
        private productsService: ProductsService,
        private authorsService: AuthorsService
    ) { }

    ngOnInit(): void {
        this.loadDashboardData();
    }

    loadDashboardData(): void {
        this.loading = true;

        this.productsService.getProducts(5).subscribe({
            next: (books) => {
                this.recentBooks = books;
                this.totalBooks = books.length;
            },
            error: (error) => {
                console.error('Error loading books:', error);
            }
        });

        this.authorsService.getAuthors(5).subscribe({
            next: (authors) => {
                this.recentAuthors = authors;
                this.totalAuthors = authors.length;
                this.loading = false;
            },
            error: (error) => {
                console.error('Error loading authors:', error);
                this.loading = false;
            }
        });
    }
}
