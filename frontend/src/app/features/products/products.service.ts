import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '../../core/services/api.service';
import { TitleDto, CreateTitleDto, UpdateTitleDto } from '../../core/models/product.model';

@Injectable({
    providedIn: 'root'
})
export class ProductsService {
    constructor(private apiService: ApiService) { }

    getProducts(count: number = 10): Observable<TitleDto[]> {
        const params = new HttpParams().set('n', count.toString());
        return this.apiService.get<TitleDto[]>('products', params);
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
