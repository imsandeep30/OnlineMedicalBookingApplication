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

  openEditUserModal(user: User) {
    this.editUser = { ...user, userAddress: { ...user.userAddress } };
    const modalEl = document.getElementById('editUserModal');
    if (modalEl) {
      this.editModalInstance = new bootstrap.Modal(modalEl);
      this.editModalInstance.show();
    }
  }

  closeEditModal() {
    this.editUser = null;
    if (this.editModalInstance) {
      this.editModalInstance.hide();
    }
  }

  submitEditUser() {
    if (!this.editUser) return;

    const token = localStorage.getItem('jwt') || '';
    const url = 'http://localhost:5184/api/User/Update';

    const payload = {
      userId: this.editUser.userId,
      name: this.editUser.userName,
      phoneNumber: this.editUser.userPhone,
      userEmail: this.editUser.userEmail,
      userAddress: {
        userStreet: this.editUser.userAddress.userStreet,
        userCity: this.editUser.userAddress.userCity,
        userState: this.editUser.userAddress.userState,
        userCountry: this.editUser.userAddress.userCountry,
        userZipCode: this.editUser.userAddress.userZipCode
      }
    };

    this.http.put(url, payload, {
      headers: { Authorization: `Bearer ${token}` },
      responseType: 'text'
    }).subscribe({
      next: () => {
        const index = this.users.findIndex(u => u.userId === this.editUser!.userId);
        if (index !== -1) {
          this.users[index] = { ...this.editUser! };
        }
        this.closeEditModal();
      },
      error: (err) => {
        console.error('Error details:', err);
        alert(`Update failed: ${err.error || 'Unknown error'}`);
      }
    });
  }

  deleteUser(userId: number) {
    if (!confirm('Are you sure you want to delete this user?')) return;

    const token = localStorage.getItem('jwt') || '';
    this.http.delete(`http://localhost:5184/api/User/Delete/${userId}`, {
      headers: { Authorization: `Bearer ${token}` },
      responseType: 'text'
    }).subscribe({
      next: () => {
        alert('User deleted successfully.');
        this.users = this.users.filter(u => u.userId !== userId);
      },
      error: (err) => {
        console.error('Delete failed:', err);
        if (err.status === 403) {
          alert('You do not have permission to delete this user.');
        } else {
          alert('Error deleting user.');
        }
      }
    });
  }
}