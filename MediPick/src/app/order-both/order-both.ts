import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-both',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-both.html',
  styleUrls: ['./order-both.css']
})
export class OrderBoth implements OnInit {
  orderSteps = [
    'Pending',
    'Confirmed',
    'Shipped',
    'Out-for-Delivery',
    'Delivered'
  ];

  ordersbyuserid: any[] = [];
  userId: number = Number(localStorage.getItem('userId')) || 0;
  token: string | null = localStorage.getItem('jwt');
  cart: any = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  // Check if a progress step is completed based on order status
  isStepCompleted(currentStatus: string, step: string): boolean {
    // console.log(currentStatus,step);
    const currentIndex = this.orderSteps.indexOf(currentStatus);//Confirmed 1<-1
    const stepIndex = this.orderSteps.indexOf(step);//Confirmed
    return stepIndex <= currentIndex;
  }

  loadOrders(): void {
    if (!this.userId || !this.token) {
      console.warn('Missing userId or token in localStorage.');
      return;
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    const url = `http://localhost:5184/api/Order/GetOrderByUserId/${this.userId}`;

    this.http.get<any[]>(url, { headers }).subscribe({
      next: (response) => {
        //console.log('Orders fetched successfully:', response);
        this.ordersbyuserid = response;

        this.ordersbyuserid.forEach(order => {
          order.orderItems.forEach((item: any) => {
            this.getMedicineName(item.medicineId).subscribe({
              next: (medicine: any) => {
                item.medicineName = medicine.medicineName;
              },
              error: (err) => {
                console.error(`Error fetching medicine ${item.medicineId}:`, err);
                item.medicineName = 'Unknown';
              }
            });
          });
        });
      },
      error: (err) => {
        console.error('Error fetching orders:', err);
        this.ordersbyuserid = [];
      }
    });
  }

  getMedicineName(medicineId: number) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    const url = `http://localhost:5184/api/Medicine/${medicineId}`;
    return this.http.get(url, { headers });
  }

  cancelOrder(orderId: number): void {
    if (!this.token) {
      console.error("No token found");
      return;
    }
    if (!confirm('Are you sure you want to cancel this order?')) {
      return;
    }
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });
    const url = `http://localhost:5184/api/Order/Cancel/${orderId}`;
    this.http.delete(url, { headers, responseType: 'text' }).subscribe({
      next: (message) => {
        alert(message);
        this.loadOrders();
      },
      error: (err) => {
        console.log("Error cancelling order:", err);
        alert(err.error || 'Failed to cancel the order');
      }
    });
  }
}
