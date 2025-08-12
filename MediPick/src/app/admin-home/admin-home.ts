import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { forkJoin, map, switchMap } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './admin-home.html',
  styleUrls: ['./admin-home.css']
})
export class AdminHome implements OnInit {
  medicineStock: { category: string; count: number }[] = [];
  recentOrders: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadMedicineStock();
    this.loadRecentOrders();
  }

  loadMedicineStock() {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    this.http.get<any[]>('http://localhost:5184/api/Medicine/all-medicines', { headers })
      .subscribe({
        next: medicines => {
          this.medicineStock = medicines.map(med => ({
            category: med.medicineName, // using medicineName instead of category
            count: med.quantityAvailable
          }));
        },
        error: err => console.error('Error loading medicines:', err)
      });
  }

  loadRecentOrders() {
    const token = localStorage.getItem('jwt');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    this.http.get<any[]>('http://localhost:5184/api/Order/GetAllOrders', { headers })
      .pipe(
        switchMap(orders => {
          const userRequests = orders.map(order =>
            this.http.get<any>(`http://localhost:5184/api/User/Profile/${order.userId}`, { headers })
              .pipe(
                map(user => ({
                  id: order.orderId,
                  customer: user.userName,
                  status: order.orderStatus,
                  amount: order.totalAmount,
                  date: new Date(order.orderDate).toLocaleDateString(),
                }))
              )
          );
          return forkJoin(userRequests);
        })
      )
      .subscribe({
        next: data => this.recentOrders = data,
        error: err => console.error('Error loading orders:', err)
      });
  }
}
