import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { ApiService } from '../../core/services/api.service';
import { AuthorViewDto, AuthorCreateDto, UpdateAuthorDto } from '../../core/models/author.model';

@Injectable({
    providedIn: 'root'
})
export class AuthorsService {
    constructor(private apiService: ApiService) { }

    getAuthors(count: number = 5): Observable<AuthorViewDto[]> {
        const params = new HttpParams().set('n', count.toString());
        return this.apiService.get<AuthorViewDto[]>('authors', params);
    }

    getAuthor(id: string): Observable<UpdateAuthorDto> {
        return this.apiService.get<UpdateAuthorDto>(`authors/${id}`);
    }

    createAuthor(dto: AuthorCreateDto): Observable<AuthorViewDto> {
        return this.apiService.post<AuthorViewDto>('authors', dto);
    }

    updateAuthor(dto: UpdateAuthorDto): Observable<any> {
        return this.apiService.put<any>('authors', dto);
    }

    deleteAuthor(id: string): Observable<any> {
        return this.apiService.delete<any>(`authors/${id}`);
    }
}
