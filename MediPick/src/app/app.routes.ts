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
import { ManageMedicines } from './manage-medicines/manage-medicines';
import { authGuard } from './auth-guard';
import { AccessDenied } from './access-denied/access-denied';
import { AdminHome } from './admin-home/admin-home';
import { Forgotpassword } from './forgotpassword/forgotpassword';
import { PaymentComponent } from './payment-component/payment-component';

import { UsersSettings } from './users-settings/users-settings';
import { AdminReports } from './admin-reports/admin-reports';
import { AdminManageUsers } from './admin-manageusers/admin-manageusers';

export const routes: Routes = [
    {path: 'forgotpass' , component : Forgotpassword},
    {path:'register',component:Register},
    {
    path: 'access-denied',
    component: AccessDenied
    },
    {path:'login',component:Login},
    {
        path:'admin-dashboard',component:AdminDashboard,canActivate: [authGuard],data: { roles: ['Admin'] },children:[
             { path: '', component: AdminHome },
           {path:'all-orders',component:OrderAdmin},
           {path:'manage-medicines',component:ManageMedicines},
           {path:'admin-reports',component:AdminReports},
           {path:'admin-manageusers',component:AdminManageUsers}
        ]
   },
    {
        path:'customer-dashboard',component:CustomerDashboard,canActivate: [authGuard],data: { roles: ['User'] },children:[
            { path: '', component: CustHome },
            {path:'orderby-userid',component:OrderBoth},
            {path : 'cart',component:CartComponent},
            {path : 'medicine-catalogue',component:MedCatlouge},
            {path:'user-settings',component:UsersSettings}
        ]
    },
    { path: 'payment', component: PaymentComponent },
    {path : '**' , component:HomeComponent}
];
