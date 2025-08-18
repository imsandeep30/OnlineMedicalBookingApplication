import { Component, OnInit } from '@angular/core';
import { UserSettings } from '../Services/user-settings';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users-settings',
  imports: [FormsModule, CommonModule],
  templateUrl: './users-settings.html',
  styleUrl: './users-settings.css'
})
export class UsersSettings implements OnInit {
  activeTab: 'profile' | 'password' = 'profile';

  userProfile: any = {
    userName: '',
    userEmail: '',
    userPhone: '',
    userId: '',
    userAddress: {
      userStreet: '',
      userCity: '',
      userState: '',
      userCountry: '',
      userZipCode: ''
    }
  };

  oldPassword = '';
  newPassword = '';
  confirmPassword = '';

  passwordMessage = '';
  passwordMessageType: 'success' | 'error' | '' = '';

  isLoading = false; // Added loading state

  constructor(private userSettings: UserSettings) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  setActiveTab(tab: 'profile' | 'password') {
    this.activeTab = tab;
  }

  loadUserProfile(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.warn('Missing userId in localStorage.');
      return;
    }

    this.userSettings.getUserProfile(Number(userId)).subscribe({
      next: (response) => {
        this.userProfile = {
          ...response,
          userAddress: response.userAddress || {
            userStreet: '',
            userCity: '',
            userState: '',
            userCountry: '',
            userZipCode: ''
          }
        };
      },
      error: (err) => console.error('Error fetching Profile', err)
    });
  }

  saveChanges(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.warn('Missing userId in localStorage.');
      return;
    }

    const updateData = {
      userId: Number(userId),
      name: this.userProfile.userName,
      phoneNumber: this.userProfile.userPhone,
      userAddress: {
        userStreet: this.userProfile.userAddress.userStreet,
        userCity: this.userProfile.userAddress.userCity,
        userState: this.userProfile.userAddress.userState,
        userCountry: this.userProfile.userAddress.userCountry,
        userZipCode: this.userProfile.userAddress.userZipCode
      }
    };

    this.userSettings.updateUserProfile(updateData).subscribe({
      next: (response) => {
        alert('Profile updated successfully.');
        this.loadUserProfile();
      },
      error: (err) => {
        console.error('Error updating profile:', err);
        alert('Failed to update profile. Please try again.');
      }
    });
  }

  deleteAccount(): void {
    if (!confirm('Are you sure you want to delete your account? This action cannot be undone.')) {
      return;
    }

    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.warn('Missing userId in localStorage.');
      return;
    }

    this.userSettings.deleteUserProfile(Number(userId)).subscribe({
      next: () => {
        alert('Your account has been deleted successfully.');
        localStorage.clear();
        window.location.href = '/login';
      },
      error: (err) => {
        console.error('Error deleting user:', err);
        alert('Failed to delete account. Please try again later.');
      }
    });
  }

  changePassword(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      this.showPasswordMessage('Missing user ID.', 'error');
      return;
    }

    if (!this.oldPassword || !this.newPassword || !this.confirmPassword) {
      this.showPasswordMessage('Please fill all password fields.', 'error');
      return;
    }

    if (this.newPassword !== this.confirmPassword) {
      this.showPasswordMessage('New password and confirm password do not match.', 'error');
      return;
    }

    const passwordReset = {
      userId: Number(userId),
      oldPassword: this.oldPassword,
      newPassword: this.newPassword
    };

    this.isLoading = true; //  Start loading

    this.userSettings.resetPassword(passwordReset).subscribe({
      next: (response) => {
        this.showPasswordMessage(response.message || 'Password updated successfully.', 'success');
        this.oldPassword = '';
        this.newPassword = '';
        this.confirmPassword = '';
        this.isLoading = false; //  Stop loading
      },
      error: (err) => {
        console.error('Password reset error:', err);
        this.showPasswordMessage(
          err?.error?.message || 'Failed to reset password. Please check your old password.',
          'error'
        );
        this.isLoading = false; // Stop loading
      }
    });
  }

  private showPasswordMessage(message: string, type: 'success' | 'error') {
    this.passwordMessage = message;
    this.passwordMessageType = type;
    setTimeout(() => {
      this.passwordMessage = '';
      this.passwordMessageType = '';
    }, 5000);
  }
}
