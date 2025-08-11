import { Component, OnInit } from '@angular/core';
import { CommonModule, NumberFormatStyle } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { TopNavbar } from '../top-navbar/top-navbar';
@Component({
  selector: 'app-customer-dashboard',
  imports: [ CommonModule, RouterModule, TopNavbar],
  templateUrl: './customer-dashboard.html',
  styleUrls: ['./customer-dashboard.css']
})
export class CustomerDashboard {

}
