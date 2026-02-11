import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'products',
    loadChildren: () => import('./features/products/products.routes').then(m => m.PRODUCTS_ROUTES)
  },
  {
    path: 'authors',
    loadChildren: () => import('./features/authors/authors.routes').then(m => m.AUTHORS_ROUTES)
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];
