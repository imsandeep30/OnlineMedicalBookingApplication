import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Register } from './register/register';
import { TopNavbar } from './top-navbar/top-navbar';
import { provideRouter } from '@angular/router';
import { Login } from './login/login';
import { CartComponent } from './cart-component/cart-component';

import { provideHttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  imports: [Register, RouterOutlet, CartComponent, Login, TopNavbar],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class App {
  title = 'MediPick';
}
