import { Component,OnInit } from '@angular/core';
import { UserSettings } from '../user-settings';
import { FormsModule } from '@angular/forms';
import { CommonModule, UpperCasePipe } from '@angular/common';
@Component({
  selector: 'app-users-settings',
  imports: [FormsModule,CommonModule],
  templateUrl: './users-settings.html',
  styleUrl: './users-settings.css'
})
export class UsersSettings implements OnInit {
  activeTab: 'profile' | 'password' = 'profile'; // Default tab
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
  oldPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  passwordMessage: string = '';
  passwordMessageType: 'success' | 'error' | '' = '';
  constructor(private userSettings: UserSettings) {}

  ngOnInit(): void {
    this.loadUserProfile(); // Fetch user profile data on component initialization
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
      next:(response)=>{
        this.userProfile={
          ...response,
          userAddress: response.userAddress || {  // ✅ Ensure it exists even if backend sends null
            userStreet: '',
            userCity: '',
            userState: '',
            userCountry: '',
            userZipCode: ''
          }
        };
        
        console.log(this.userProfile);
      },
      error:(err)=>console.error('Error fetching Profile', err)
    });
  }
  saveChanges():void{
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.warn('Missing userId in localStorage.');
      return;
    }
    const updateData = {
      userId: Number(userId),
      name: this.userProfile.userName,
      phoneNumber: this.userProfile.userPhone,
      userAddress: {                         // must match AdressDTO in backend
      userStreet: this.userProfile.userAddress.userStreet,
      userCity: this.userProfile.userAddress.userCity,
      userState: this.userProfile.userAddress.userState,
      userCountry: this.userProfile.userAddress.userCountry,
      userZipCode: this.userProfile.userAddress.userZipCode
    }
    };
    this.userSettings.updateUserProfile(updateData).subscribe({
      next:(response)=>{
        console.log(response);
        alert('Profile updated successfully.');
        this.loadUserProfile();
      },
      error:(err)=>{
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
      next: (response) => {
        console.log('User deleted successfully:', response);
        alert('Your account has been deleted successfully.');
        
        // Clear local storage and redirect to login
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
    this.passwordMessage = 'Missing user ID.';
    this.passwordMessageType = 'error';
    return;
  }

  if (!this.oldPassword || !this.newPassword || !this.confirmPassword) {
    this.passwordMessage = 'Please fill all password fields.';
    this.passwordMessageType = 'error';
    return;
  }

  if (this.newPassword !== this.confirmPassword) {
    this.passwordMessage = 'New password and confirm password do not match.';
    this.passwordMessageType = 'error';
    return;
  }

  const passwordReset = {
    userId: Number(userId),
    oldPassword: this.oldPassword,
    newPassword: this.newPassword
  };

  this.userSettings.resetPassword(passwordReset).subscribe({
    next: (response) => {
      this.passwordMessage = response.message || 'Password updated successfully.';
      this.passwordMessageType = 'success';
      this.oldPassword = '';
      this.newPassword = '';
      this.confirmPassword = '';

      // Optional: Hide the message after 5 seconds
      setTimeout(() => {
        this.passwordMessage = '';
        this.passwordMessageType = '';
      }, 5000);
    },
    error: (err) => {
      this.passwordMessage = 'Failed to reset password. Please check your old password.';
      this.passwordMessageType = 'error';
    }
  });
 }
}
