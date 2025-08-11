import { Routes } from '@angular/router';
import { Register } from './register/register';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { CartComponent } from './cart-component/cart-component';
import { CustomerDashboard } from './customer-dashboard/customer-dashboard';
import { Login } from './login/login';
import { OrderAdmin } from './order-admin/order-admin';
import { OrderBoth } from './order-both/order-both';
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
            {path:'orderby-userid',component:OrderBoth}
        ]
    },
];
