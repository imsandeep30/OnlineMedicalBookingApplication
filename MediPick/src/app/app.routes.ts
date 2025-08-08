import { Routes } from '@angular/router';
import { Register } from './register/register';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { CustomerDashboard } from './customer-dashboard/customer-dashboard';
import { Login } from './login/login';
export const routes: Routes = [
    {path:'register',component:Register},
    {path:'login',component:Login},
    {path:'admin-dashboard',component:AdminDashboard},
    {path:'customer-dashboard',component:CustomerDashboard}
];
