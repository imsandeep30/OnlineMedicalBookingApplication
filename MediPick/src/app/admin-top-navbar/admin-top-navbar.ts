import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterLinkActive } from '@angular/router';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-admin-top-navbar',
  standalone: true,
  imports: [CommonModule, RouterLinkActive, RouterLink],
  templateUrl: './admin-top-navbar.html',
  styleUrl: './admin-top-navbar.css'
})
export class AdminTopNavbar {
  get userRole(): string | null {  
    return localStorage.getItem('role');
  }

  get userName(): string | null {
    return localStorage.getItem('userName');
  }

  constructor(private router: Router) {}
  //userRole: string | null = localStorage.getItem('role'); // or from your auth service

  get homeRoute(): string {
    
    console.log('User Role:', this.userRole);
    if (this.userRole === 'admin') {
      return '/admin-dashboard';
    } else {
      return '/customer-dashboard';
    }
  }
  logout() {

    // Clear user data from localStorage or auth service
    localStorage.clear();
    // Redirect to login page or home
    this.router.navigate(['/login']);
  }
}
