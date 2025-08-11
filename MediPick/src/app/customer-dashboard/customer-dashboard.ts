import { Component, OnInit } from '@angular/core';
import { CommonModule, NumberFormatStyle } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { TopNavbar } from '../top-navbar/top-navbar';
@Component({
  selector: 'app-customer-dashboard',
  imports: [ CommonModule, RouterModule, TopNavbar],
  templateUrl: './customer-dashboard.html',
  styleUrl: './customer-dashboard.css'
})
export class CustomerDashboard implements OnInit {

  userName: string = '';
  totalOrders : number = 0;
  deliveredOrders : number = 0;
  totalSpent : number = 0;
  pendingOrders: number = 0;
  recentActivity : any = [];
  processingOrders: number = 0;

  ngOnInit(): void {
    console.log(localStorage.getItem('userName'));
    const storedUsername = localStorage.getItem('userName');
    if (storedUsername) {
      this.userName = storedUsername;
    } else {
      this.userName = 'Guest';
    }
  }
}
