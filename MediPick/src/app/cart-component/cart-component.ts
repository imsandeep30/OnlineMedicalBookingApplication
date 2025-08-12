import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MedicineCard } from '../medicine-card/medicine-card';
import { CartDto, CartItemDTO } from '../cart-dto';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { CartService } from '../cart.service';
import { TopNavbar } from "../top-navbar/top-navbar";
import { HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { RouterOutlet } from '@angular/router';
interface UserAddress {
  userStreet: string;
  userCity: string;
  userState: string;
  userZipCode: string;
  userCountry: string;
}

@Component({
  selector: 'app-cart-component',
  standalone: true,
  imports: [CommonModule, MedicineCard, TopNavbar, RouterOutlet],
  templateUrl: './cart-component.html',
  styleUrls: ['./cart-component.css']
})
export class CartComponent implements OnInit, OnDestroy {

  userName: string = localStorage.getItem('userName') || 'Guest';
  cartItems: any[] = [];
  cartTotal: number = 0;

  userAddress: UserAddress = {
    userStreet: '',
    userCity: '',
    userState: '',
    userZipCode: '',
    userCountry: ''
  };

  private routeSub!: Subscription;
  paymentMethod: string = ''; 
  constructor(private http: HttpClient, private router: Router, private cartService: CartService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadCart();
    this.loadAddress();
  }

  loadCart(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) return;

    this.cartService.getCart(userId).subscribe({
      next: response => {
        this.cartItems = response.items;
        this.cartTotal = response.totalPrice;
      },
      error: err => console.error('Error fetching cart', err)
    });
  }

  removeItem(item: CartItemDTO): void {
    const userId = localStorage.getItem('userId');
    if (!userId) return;

    this.cartService.removeItem(userId, item.medicineId).subscribe({
      next: () => {
        this.cartItems = this.cartItems.filter(i => i !== item);
        this.loadCart();
      },
      error: err => console.error('Error removing item', err)
    });
  }

  loadAddress(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) return;

    this.http.get<any>(`http://localhost:5184/api/User/Profile/${userId}`, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    }).subscribe({
      next: response => {
        this.userAddress = response.userAddress;
        console.log('User address:', this.userAddress);
      },
      error: err => console.error('Error loading address', err)
    });
  }
  selectPaymentMethod(method: string) {
    this.paymentMethod = method;
  }

  placeOrder() {
    if (!this.paymentMethod) {
      alert('Please select a payment method before placing order');
      return;
    }
    this.router.navigate(['payment'], {
      queryParams: { method: this.paymentMethod }
    });

  }
  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }
}
