import { Routes } from '@angular/router';
import { Register } from './register/register';
export const routes: Routes = [
    // Define your routes here
    { path: 'register', component: Register },
    { path: '', redirectTo: '/register', pathMatch: 'full' }
];
