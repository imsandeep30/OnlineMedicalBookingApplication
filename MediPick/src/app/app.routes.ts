import { Routes } from '@angular/router';
import { Register } from './register/register';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { CartComponent } from './cart-component/cart-component';
import { CustomerDashboard } from './customer-dashboard/customer-dashboard';
import { Login } from './login/login';
import { OrderAdmin } from './order-admin/order-admin';
import { OrderBoth } from './order-both/order-both';
import { MedCatlouge } from './med-catlouge/med-catlouge';
import { HomeComponent } from './home-component/home-component';
import { CustHome } from './cust-home/cust-home';
export const routes: Routes = [
    {path:'register',component:Register},
    {path:'login',component:Login},
    {
        path:'admin-dashboard',component:AdminDashboard,children:[
           {path:'all-orders',component:OrderAdmin},
        ]
   },
    {
        path:'customer-dashboard',component:CustomerDashboard,children:[
            { path: '', component: CustHome },
            {path:'orderby-userid',component:OrderBoth},
            {path : 'cart',component:CartComponent},
            {path : 'medicine-catalogue',component:MedCatlouge},
        ]
    },
    {path : '**' , component:HomeComponent}
];
