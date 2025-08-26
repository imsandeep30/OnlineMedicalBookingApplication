import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable'; // correct import
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

interface User {
  userId: number;
  userName: string;
  role: string;
}

interface Order {
  orderId: number;
  userId: number;
  orderDate: string;
  totalAmount: number;
}

interface Medicine {
  medicineId: number;
  medicineName: string;
  quantityAvailable: number;
}

@Component({
  selector: 'app-admin-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-reports.html',
  styleUrls: ['./admin-reports.css']
})
export class AdminReports implements OnInit {

  dateRanges = ['Last 7 Days', 'Last 30 Days', 'Last Year'];
  userTypes = ['All Users', 'User'];

  selectedDateRange = this.dateRanges[0];
  selectedUserType = this.userTypes[0];

  users: User[] = [];
  orders: Order[] = [];
  medicines: Medicine[] = [];

  totalUsers = 0;
  totalOrders = 0;
  totalSales = 0;
  lowStockMedicinesCount = 0;

  loading = false;
  errorMsg = '';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadAllData();
  }

  loadAllData() {
    this.loading = true;
    this.errorMsg = '';

    const token = localStorage.getItem('jwt') || '';
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const usersRequest = this.http.get<User[]>('http://localhost:5184/api/User/AllUsers', { headers });
    const ordersRequest = this.http.get<Order[]>('http://localhost:5184/api/Order/GetAllOrders', { headers });
    const medicinesRequest = this.http.get<Medicine[]>('http://localhost:5184/api/Medicine/all-medicines', { headers });
    
    import('rxjs').then(rxjs => {
      const { forkJoin } = rxjs;
      forkJoin([usersRequest, ordersRequest, medicinesRequest]).subscribe({
        next: ([usersData, ordersData, medicinesData]) => {
          this.users = usersData;
          this.orders = ordersData;
          this.medicines = medicinesData;
          this.applyFilters();
          this.loading = false;
        },
        error: err => {
          this.errorMsg = 'Failed to load data. Check your API and token.';
          this.loading = false;
        }
      });
    });
  }

  applyFilters() {
    let filteredUsers = this.users;
    if (this.selectedUserType !== 'All Users') {
      filteredUsers = this.users.filter(u => u.role === this.selectedUserType);
    }
    this.totalUsers = filteredUsers.length;

    let dateFrom = new Date();
    switch (this.selectedDateRange) {
      case 'Last 7 Days':
        dateFrom.setDate(dateFrom.getDate() - 7);
        break;
      case 'Last 30 Days':
        dateFrom.setMonth(dateFrom.getMonth() - 1);
        break;
      case 'Last Year':
        dateFrom.setFullYear(dateFrom.getFullYear() - 1);
        break;
      default:
        dateFrom = new Date(0);
    }

    const filteredOrders = this.orders.filter(order => {
      const orderDate = new Date(order.orderDate);
      if (orderDate < dateFrom) return false;

      if (this.selectedUserType !== 'All Users') {
        const user = this.users.find(u => u.userId === order.userId);
        if (!user || user.role !== this.selectedUserType) return false;
      }
      return true;
    });

    this.totalOrders = filteredOrders.length;
    this.totalSales = filteredOrders.reduce((sum, order) => sum + order.totalAmount, 0);
    this.lowStockMedicinesCount = this.medicines.filter(m => m.quantityAvailable < 50).length;
  }

  formatCurrency(value: number): string {
    return `₹${value.toLocaleString('en-IN')}`;
  }

  exportPDF() {
    const doc = new jsPDF();

    doc.text('Admin Reports', 14, 16);

    autoTable(doc, {
      startY: 20,
      head: [['Metric', 'Value']],
      body: [
        ['Total Users', this.totalUsers.toString()],
        ['Total Orders', this.totalOrders.toString()],
        ['Total Sales', 'Rs. ' + this.totalSales.toLocaleString('en-IN')],
        ['Low Stock Medicines', this.lowStockMedicinesCount.toString()]
      ],
    });

    doc.save('admin-reports.pdf');
  }

  exportExcel() {
    const data = [
      { Metric: 'Total Users', Value: this.totalUsers },
      { Metric: 'Total Orders', Value: this.totalOrders },
      { Metric: 'Total Sales', Value: this.totalSales },
      { Metric: 'Low Stock Medicines', Value: this.lowStockMedicinesCount }
    ];

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const workbook: XLSX.WorkBook = { Sheets: { 'Reports': worksheet }, SheetNames: ['Reports'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });

    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });
    saveAs(blob, 'admin-reports.xlsx');
  }
}
