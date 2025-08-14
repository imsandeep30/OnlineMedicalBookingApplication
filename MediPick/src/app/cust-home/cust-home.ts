import { Component, OnInit } from '@angular/core';
import { CommonModule, NumberFormatStyle } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TopNavbar } from '../top-navbar/top-navbar';
@Component({
  selector: 'app-cust-home',
  imports: [CommonModule, RouterModule, TopNavbar],
  standalone: true,
  templateUrl: './cust-home.html',
  styleUrls: ['./cust-home.css']
})
export class CustHome implements OnInit {
  userName: string = '';
  totalOrders : number = 0;
  deliveredOrders: number = 0;
  totalSpent : number = 0;
  pendingOrders: number = 0;
  recentActivity : any = [];
  processingOrders: number = 0;
  constructor (private http: HttpClient) {}
  ngOnInit(): void {
    console.log(localStorage.getItem('userName'));
    const storedUsername = localStorage.getItem('userName');
    if (storedUsername) {
      this.userName = storedUsername;
    } else {
      this.userName = 'Guest';
    }
    this.getTotalOrders();
  }
  //getting total order of the user
  getTotalOrders() {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('jwt');
    if (!userId) return;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    this.http.get<any[]>(`http://localhost:5184/api/Order/GetOrderByUserId/${userId}`, { headers })
      .subscribe({
        next: orders => {
          console.log(orders);
          this.totalOrders = orders.length;
          this.deliveredOrders = orders.filter(o => o.orderStatus === 'Confirmed').length;
          this.pendingOrders = orders.filter(o => o.orderStatus === 'Pending').length;
          this.processingOrders = orders.filter(o => o.orderStatus === 'Processing').length;
          this.totalSpent = orders.filter(o => o.orderStatus === 'Confirmed').reduce((sum, o) => sum + o.totalAmount, 0);
          this.recentActivity = orders.map(order => {
          const items = order.orderItems.map((item: any) => item.medicineName).join(', ');
            return {
              message: `You booked ${items} and the order was ${order.orderStatus}`
            };
          });
        },
        error: err => console.error('Error loading user orders:', err)
      });
  }
}
