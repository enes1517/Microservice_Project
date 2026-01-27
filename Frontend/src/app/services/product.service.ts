import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Matching Backend DTOs:
// GET: Id, Name, Type, Price
// POST: Id, Title, Price
// PUT: Id, Title, Type, Price

export interface Product {
    id: string;
    name: string; // Displayed as Title
    type: string; // Previously Status
    price: number; // Previously Description
}

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    // Direct connection to ProductService (bypassing Gateway for stability)
    private apiUrl = 'http://localhost:5225/api/products';

    constructor(private http: HttpClient) { }

    getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(this.apiUrl);
    }

    getProduct(id: string): Observable<Product> {
        return this.http.get<Product>(`${this.apiUrl}/${id}`);
    }

    createProduct(product: Partial<Product>): Observable<any> {
        // Map Frontend 'name' to Backend 'Title' for CreateTitleDto
        // PUBS DB 'title_id' is typically VARCHAR(6). UUID is too long.
        // Generating a short ID: 'BU' + 4 random digits
        const shortId = 'BU' + Math.floor(1000 + Math.random() * 9000).toString();

        const payload = {
            id: shortId,
            title: product.name,
            price: product.price || 0
        };
        return this.http.post(this.apiUrl, payload);
    }

    updateProduct(product: Product): Observable<any> {
        // Map Frontend 'name' to Backend 'Title' for UpdateTitleDto
        const payload = {
            id: product.id,
            title: product.name,
            type: product.type || 'Standard',
            price: product.price || 0
        };
        // Backend Put is on /api/products (body has ID)
        return this.http.put(`${this.apiUrl}`, payload);
    }

    deleteProduct(id: string): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
