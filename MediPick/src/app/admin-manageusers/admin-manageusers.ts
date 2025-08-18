import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

declare var bootstrap: any;

interface UserAddress {
  userStreet: string;
  userCity: string;
  userState: string;
  userCountry: string;
  userZipCode: string;
}

interface User {
  userId: number;
  userName: string;
  userEmail: string;
  userPhone: string;
  role: string;
  joinedDate: string;
  userAddress: UserAddress;
}

@Component({
  selector: 'app-admin-manageusers',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './admin-manageusers.html',
  styleUrls: ['./admin-manageusers.css'],
})
export class AdminManageUsers implements OnInit {
  users: User[] = [];
  errorMessage: string = '';
  selectedUser: User | null = null;
  editUser: User | null = null;
  private editModalInstance: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    const token = localStorage.getItem('jwt') || '';
    this.http
      .get<any[]>('http://localhost:5184/api/User/AllUsers', {
        headers: { Authorization: `Bearer ${token}` },
      })
      .subscribe({
        next: (res) => {
          this.users = res.map(u => ({
            userId: u.userId,
            userName: u.userName,
            userEmail: u.userEmail,
            userPhone: u.userPhone,
            role: u.role,
            joinedDate: u.createdAt,
            userAddress: {
              userStreet: u.address?.street || '',
              userCity: u.address?.city || '',
              userState: u.address?.state || '',
              userCountry: u.address?.country || '',
              userZipCode: u.address?.zipCode || ''
            }
          }));
          this.errorMessage = '';
        },
        error: () => {
          this.errorMessage = 'Failed to load users. Please try again.';
        },
      });
  }

  getInitials(name?: string): string {
    if (!name) return 'NA';
    const parts = name.trim().split(' ');
    if (parts.length === 1) return parts[0].charAt(0).toUpperCase();
    return (parts[0].charAt(0) + parts[1].charAt(0)).toUpperCase();
  }

  openUserDetailsModal(user: User) {
    this.selectedUser = user;
    const modalElement = document.getElementById('userDetailsModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  }

}