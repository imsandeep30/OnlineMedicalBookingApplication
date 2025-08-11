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
@Component({
  selector: 'app-cart-component',
  standalone: true,
  imports: [CommonModule, MedicineCard, TopNavbar],
  templateUrl: './cart-component.html',
  styleUrls: ['./cart-component.css']
})
export class CartComponent implements OnInit, OnDestroy {
  userName: string = localStorage.getItem('userName') || 'Guest';
  cartItems: any[] = [];
  cartTotal: number = 0;
  private routeSub!: Subscription;

  // Inject CartService in constructor
constructor(private http: HttpClient, private router: Router, private cartService: CartService) {}
  ngOnInit(): void {
    this.loadCart();
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
  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }
}
