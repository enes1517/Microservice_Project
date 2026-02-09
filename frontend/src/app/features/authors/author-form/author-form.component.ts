import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthorsService } from '../authors.service';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthorCreateDto, UpdateAuthorDto } from '../../../core/models/author.model';

@Component({
    selector: 'app-author-form',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatCheckboxModule
    ],
    templateUrl: './author-form.component.html',
    styleUrl: './author-form.component.scss'
})
export class AuthorFormComponent implements OnInit {
    authorForm: FormGroup;
    isEditMode = false;
    authorId: string | null = null;
    loading = false;

    constructor(
        private fb: FormBuilder,
        private authorsService: AuthorsService,
        private notificationService: NotificationService,
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.authorForm = this.fb.group({
            auFname: ['', [Validators.required, Validators.maxLength(20)]],
            auLname: ['', [Validators.required, Validators.maxLength(40)]],
            phone: ['', Validators.required],
            address: [''],
            city: [''],
            contract: [false]
        });
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            if (params['id']) {
                this.isEditMode = true;
                this.authorId = params['id'];
                this.loadAuthor(params['id']);
            }
        });
    }

    loadAuthor(id: string): void {
        this.loading = true;
        this.authorsService.getAuthor(id).subscribe({
            next: (author) => {
                this.authorForm.patchValue(author);
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    onSubmit(): void {
        if (this.authorForm.valid) {
            this.loading = true;
            const formValue = this.authorForm.getRawValue();

            if (this.isEditMode) {
                const updateDto: UpdateAuthorDto = {
                    id: this.authorId!,
                    ...formValue
                };
                this.authorsService.updateAuthor(updateDto).subscribe({
                    next: () => {
                        this.notificationService.success('Yazar başarıyla güncellendi');
                        this.router.navigate(['/authors']);
                    },
                    error: () => {
                        this.loading = false;
                    }
                });
            } else {
                const createDto: AuthorCreateDto = formValue;
                this.authorsService.createAuthor(createDto).subscribe({
                    next: () => {
                        this.notificationService.success('Yazar başarıyla eklendi');
                        this.router.navigate(['/authors']);
                    },
                    error: () => {
                        this.loading = false;
                    }
                });
            }
        }
    }

    cancel(): void {
        this.router.navigate(['/authors']);
    }
}
