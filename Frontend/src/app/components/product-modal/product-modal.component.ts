import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product, ProductService } from '../../services/product.service';

@Component({
    selector: 'app-product-modal',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './product-modal.component.html',
    styleUrl: './product-modal.component.css'
})
export class ProductModalComponent implements OnInit {
    @Input() product: Product | undefined;
    @Output() close = new EventEmitter<boolean>();

    formData: Partial<Product> = {
        name: '',
        type: 'Standard',
        price: 0
    };

    isSubmitting = false;

    constructor(private productService: ProductService) { }

    ngOnInit(): void {
        if (this.product) {
            this.formData = {
                id: this.product.id,
                name: this.product.name,
                type: this.product.type,
                price: this.product.price
            };
        }
    }

    save(): void {
        if (!this.formData.name) return;

        this.isSubmitting = true;

        if (this.product) {
            // Update
            // Ensure all fields are present for update
            const updatedProduct = { ...this.formData } as Product;
            this.productService.updateProduct(updatedProduct).subscribe({
                next: () => {
                    this.isSubmitting = false;
                    this.close.emit(true);
                },
                error: (err) => {
                    console.error(err);
                    this.isSubmitting = false;
                }
            });
        } else {
            // Create
            this.productService.createProduct(this.formData).subscribe({
                next: () => {
                    this.isSubmitting = false;
                    this.close.emit(true);
                },
                error: (err) => {
                    console.error('Create Error:', err);
                    this.isSubmitting = false;
                    alert(`Failed to create product!
Status: ${err.status}
Text: ${err.statusText}
Message: ${err.message}
Details: ${JSON.stringify(err.error)}`);
                }
            });
        }
    }

    cancel(): void {
        this.close.emit(false);
    }
}
