import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Allorders } from '../allorders';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Orderstatus } from '../orderstatus';

@Component({
  selector: 'app-order-admin',
  imports: [CommonModule, FormsModule],
  templateUrl: './order-admin.html',
  styleUrl: './order-admin.css'
})
export class OrderAdmin {
  allorders: any[] = [];
  filteredOrders: any[] = [];
  orderstatus: Orderstatus = { orderId: 0, newStatus: '' };
  message: string = '';

  // Filter variables
  selectedDateRange: string = 'today';
  selectedStatus: string = '';
  searchQuery: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<Allorders[]>('http://localhost:5184/api/Order/GetAllOrders',
      { headers: { 'Authorization': `Bearer ${localStorage.getItem('jwt')} ` } })
      .subscribe((response) => {
        this.allorders = response;
        this.filteredOrders = [...this.allorders]; // default view
      });
  }

  applyFilters() {
    const today = new Date();
    this.filteredOrders = this.allorders.filter(order => {
      const orderDate = new Date(order.orderDate);

      // Date Range Filter
      let dateMatch = true;
      if (this.selectedDateRange === 'today') {
        dateMatch = orderDate.toDateString() === today.toDateString();
      } else if (this.selectedDateRange === 'thisWeek') {
        const startOfWeek = new Date(today);
        startOfWeek.setDate(today.getDate() - today.getDay());
        dateMatch = orderDate >= startOfWeek && orderDate <= today;
      } else if (this.selectedDateRange === 'thisMonth') {
        dateMatch = orderDate.getMonth() === today.getMonth() &&
                    orderDate.getFullYear() === today.getFullYear();
      }

      // Status Filter
      const statusMatch = this.selectedStatus ? order.orderStatus === this.selectedStatus : true;

      // Search Filter (case insensitive)
      const searchMatch = this.searchQuery
        ? Object.values(order).some(val =>
            val && val.toString().toLowerCase().includes(this.searchQuery.toLowerCase())
          )
        : true;

      return dateMatch && statusMatch && searchMatch;
    });
  }

  UpdateStatus(orderId: number) {
    this.orderstatus.orderId = orderId;

    this.http.put(
      'http://localhost:5184/api/Order/status',
      this.orderstatus,
      {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('jwt')}` },
        responseType: 'text'
      }
    ).subscribe({
      next: (response) => {
        this.message = response;
        this.ngOnInit(); // refresh list
      },
      error: (err) => {
        console.error('Error updating order status:', err);
        this.message = 'Failed to update order status.';
      }
    });
  }
}
