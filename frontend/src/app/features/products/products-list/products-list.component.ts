import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ProductsService } from '../products.service';
import { NotificationService } from '../../../core/services/notification.service';
import { TitleDto } from '../../../core/models/product.model';

@Component({
    selector: 'app-products-list',
    standalone: true,
    imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
    templateUrl: './products-list.component.html',
    styleUrl: './products-list.component.scss'
})
export class ProductsListComponent implements OnInit {
    products: TitleDto[] = [];
    displayedColumns: string[] = ['id', 'title', 'type', 'price', 'royalty', 'actions'];
    loading = true;

    constructor(
        private productsService: ProductsService,
        private notificationService: NotificationService,
        private dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.loadProducts();
    }

    loadProducts(): void {
        this.loading = true;
        this.productsService.getProducts(50).subscribe({
            next: (data) => {
                this.products = data;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    deleteProduct(id: string): void {
        if (confirm('Bu kitabı silmek istediğinizden emin misiniz?')) {
            this.productsService.deleteProduct(id).subscribe({
                next: () => {
                    this.notificationService.success('Kitap başarıyla silindi');
                    this.loadProducts();
                }
            });
        }
    }
}
