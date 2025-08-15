import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MedicineCard } from '../medicine-card/medicine-card';
import { CartItemDTO,CartDto } from '../DTOS/cart-dto';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { CartService } from '../Services/cart.service';
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
interface OrderPayload {
  userId: string;
  shippingAddress: string;
}
@Component({
  selector: 'app-cart-component',
  standalone: true,
  imports: [CommonModule, MedicineCard, TopNavbar, RouterOutlet],
  templateUrl: './cart-component.html',
  styleUrls: ['./cart-component.css']
})

export class CartComponent implements OnInit, OnDestroy {
  OrderPayload: OrderPayload = {
    userId: localStorage.getItem('userId') || '',
    shippingAddress: ''
  };
  userName: string = localStorage.getItem('userName') || 'Guest';
  cartItems: any[] = [];
  cartTotal: number = 0;
  ItemPrice: number = 0;
  userAddress: UserAddress = {
    userStreet: '',
    userCity: '',
    userState: '',
    userZipCode: '',
    userCountry: ''
  };
  FinalAddress : string = '';
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
        console.log('Cart items:', this.cartItems);
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
  routerParam='';
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
  OrderId : number = 0;
  placeOrder() {
    if (!this.paymentMethod) {
      alert('Please select a payment method before placing order');
      return;
    }
    const userId = localStorage.getItem('userId');
    this.FinalAddress = `${this.userAddress.userStreet}, ${this.userAddress.userCity}, ${this.userAddress.userState}, ${this.userAddress.userZipCode}, ${this.userAddress.userCountry}`;
    this.OrderPayload.shippingAddress = this.FinalAddress;
    this.OrderPayload.userId = userId || '';
    console.log('Order Payload:', this.OrderPayload);
    this.http.post<any>('http://localhost:5184/api/Order/place-order', this.OrderPayload, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
        }).subscribe({
          next: response => {
            this.OrderId = response.orderId.toString();
            console.log('Order placed successfully:', response.orderId);

            // Navigate only after we get the orderId
            this.router.navigate(['payment'], {
              queryParams: { method: this.paymentMethod, Id: this.OrderId }
            });
          },
      error: err => console.error('Error placing order', err)
    });

  }
  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }
}
