import { Routes } from '@angular/router';
import { AuthorsListComponent } from './authors-list/authors-list.component';
import { AuthorFormComponent } from './author-form/author-form.component';

export const AUTHORS_ROUTES: Routes = [
    {
        path: '',
        component: AuthorsListComponent
    },
    {
        path: 'new',
        component: AuthorFormComponent
    },
    {
        path: ':id/edit',
        component: AuthorFormComponent
    }
];
