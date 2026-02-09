import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { AuthorsService } from '../authors.service';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthorViewDto } from '../../../core/models/author.model';

@Component({
    selector: 'app-authors-list',
    standalone: true,
    imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
    templateUrl: './authors-list.component.html',
    styleUrl: './authors-list.component.scss'
})
export class AuthorsListComponent implements OnInit {
    authors: AuthorViewDto[] = [];
    displayedColumns: string[] = ['fullName', 'phone', 'location', 'bookCount', 'actions'];
    loading = true;

    constructor(
        private authorsService: AuthorsService,
        private notificationService: NotificationService
    ) { }

    ngOnInit(): void {
        this.loadAuthors();
    }

    loadAuthors(): void {
        this.loading = true;
        this.authorsService.getAuthors(50).subscribe({
            next: (data) => {
                this.authors = data;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    deleteAuthor(id: string): void {
        if (confirm('Bu yazarı silmek istediğinizden emin misiniz?')) {
            this.authorsService.deleteAuthor(id).subscribe({
                next: () => {
                    this.notificationService.success('Yazar başarıyla silindi');
                    this.loadAuthors();
                }
            });
        }
    }
}
