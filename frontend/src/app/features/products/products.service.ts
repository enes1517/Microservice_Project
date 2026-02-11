import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '../../core/services/api.service';
import { TitleDto, CreateTitleDto, UpdateTitleDto } from '../../core/models/product.model';

export interface ProductFilterParams {
    pageNumber?: number;
    pageSize?: number;
    title?: string;
    type?: string;
    price?: number;
}

@Injectable({
    providedIn: 'root'
})
export class ProductsService {
    constructor(private apiService: ApiService) { }

    getProducts(count: number = 10): Observable<TitleDto[]> {
        const params = new HttpParams().set('n', count.toString());
        return this.apiService.get<TitleDto[]>('products', params);
    }

    getProductsWithFilters(filterParams: ProductFilterParams = {}): Observable<{ items: TitleDto[], totalCount: number }> {
        let params = new HttpParams();

        if (filterParams.pageNumber) {
            params = params.set('pageNumber', filterParams.pageNumber.toString());
        }
        if (filterParams.pageSize) {
            params = params.set('pageSize', filterParams.pageSize.toString());
        }
        if (filterParams.title) {
            params = params.set('title', filterParams.title);
        }
        if (filterParams.type) {
            params = params.set('type', filterParams.type);
        }
        if (filterParams.price) {
            params = params.set('price', filterParams.price.toString());
        }

        return this.apiService.get<{ items: TitleDto[], totalCount: number }>('products', params);
    }

    getProduct(id: string): Observable<TitleDto> {
        return this.apiService.get<TitleDto>(`products/${id}`);
    }

    createProduct(dto: CreateTitleDto): Observable<TitleDto> {
        return this.apiService.post<TitleDto>('products', dto);
    }

    updateProduct(dto: UpdateTitleDto): Observable<any> {
        return this.apiService.put<any>('products', dto);
    }

    deleteProduct(id: string): Observable<any> {
        return this.apiService.delete<any>(`products/${id}`);
    }
}
