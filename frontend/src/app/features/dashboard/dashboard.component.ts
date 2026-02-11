import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { ProductsService } from '../products/products.service';
import { AuthorsService } from '../authors/authors.service';

interface DashboardBook {
    id: string;
    title: string;
    type: string;
    price?: number;
    royalty?: number;
}

interface DashboardAuthor {
    auId: string;
    auFname: string;
    auLname: string;
    city?: string;
    phone: string;
}

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
    recentBooks: DashboardBook[] = [];
    recentAuthors: DashboardAuthor[] = [];
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

        // Son 5 kitabı yükle
        this.productsService.getProductsWithFilters({ pageSize: 5, pageNumber: 1 }).subscribe({
            next: (data) => {
                const books = data.items;
                this.recentBooks = books.map(book => ({
                    id: book.id,
                    title: book.title,
                    type: book.type,
                    price: book.price,
                    royalty: book.royalty
                }));
                this.totalBooks = data.totalCount;
            },
            error: (error) => {
                console.error('Kitaplar yüklenirken hata:', error);
            }
        });

        // Son 5 yazarı yükle
        this.authorsService.getAuthorsWithFilters({ pageSize: 5, pageNumber: 1 }).subscribe({
            next: (data) => {
                const authors = data.items;
                this.recentAuthors = authors.map((author: any) => ({
                    auId: author.id,
                    auFname: author.fullName, // Mapping fullName to display correctly
                    auLname: '', // Not needed as we use FullName
                    city: author.location,
                    phone: author.phone
                }));
                this.totalAuthors = data.totalCount;
                this.loading = false;
            },
            error: (error) => {
                console.error('Yazarlar yüklenirken hata:', error);
                this.loading = false;
            }
        });
    }

    getAuthorFullName(author: DashboardAuthor): string {
        return author.auFname; // fullName is mapped to auFname in loadDashboardData
    }
}
