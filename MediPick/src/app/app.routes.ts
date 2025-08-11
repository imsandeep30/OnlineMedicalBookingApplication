import { Routes } from '@angular/router';
import { Register } from './register/register';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { CartComponent } from './cart-component/cart-component';
import { CustomerDashboard } from './customer-dashboard/customer-dashboard';
import { Login } from './login/login';
import { MedCatlouge } from './med-catlouge/med-catlouge';
import { HomeComponent } from './home-component/home-component';
export const routes: Routes = [
    {path:'register',component:Register},
    {path:'login',component:Login},
    {path:'admin-dashboard',component:AdminDashboard},
    {path:'customer-dashboard',component:CustomerDashboard},
    {path : 'cart',component:CartComponent},
    {path : 'medicine-catalogue',component:MedCatlouge},
    {path : '**' , component:HomeComponent}
];
