import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService, Product } from '../../services/product.service';
import { ProductModalComponent } from '../product-modal/product-modal.component';

@Component({
    selector: 'app-product-list',
    standalone: true,
    imports: [CommonModule, ProductModalComponent],
    templateUrl: './product-list.component.html',
    styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {
    products: Product[] = [];
    selectedProduct: Product | undefined = undefined;
    isModalOpen = false;

    constructor(private productService: ProductService) { }

    ngOnInit(): void {
        this.loadProducts();
    }

    loadProducts(): void {
        this.productService.getProducts().subscribe({
            next: (data) => {
                this.products = data;
            },
            error: (err) => console.error('Failed to load products', err)
        });
    }

    openCreateModal(): void {
        this.selectedProduct = undefined; // Reset for creation
        this.isModalOpen = true;
    }

    // FIXED: Explicitly handle product selection for editing
    openEditModal(product: Product): void {
        this.selectedProduct = { ...product }; // Copy object to prevent direct mutation
        this.isModalOpen = true;
    }

    closeModal(shouldRefresh: boolean): void {
        this.isModalOpen = false;
        this.selectedProduct = undefined;
        if (shouldRefresh) {
            this.loadProducts();
        }
    }

    deleteProduct(id: string): void {
        if (confirm('Are you sure you want to delete this product?')) {
            this.productService.deleteProduct(id).subscribe({
                next: () => {
                    this.loadProducts();
                },
                error: (err) => console.error('Failed to delete product', err)
            });
        }
    }
}
