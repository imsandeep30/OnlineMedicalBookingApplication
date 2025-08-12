import { Component,OnInit } from '@angular/core';
import { UserSettings } from '../user-settings';
import { FormsModule } from '@angular/forms';
import { UpperCasePipe } from '@angular/common';
@Component({
  selector: 'app-users-settings',
  imports: [FormsModule],
  templateUrl: './users-settings.html',
  styleUrl: './users-settings.css'
})
export class UsersSettings implements OnInit {
  userProfile: any = {};

  constructor(private userSettings: UserSettings) {}

  ngOnInit(): void {
    this.loadUserProfile(); // Fetch user profile data on component initialization
  }
  loadUserProfile(): void {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.warn('Missing userId in localStorage.');
      return;
    }
    this.userSettings.getUserProfile(Number(userId)).subscribe({
      next:response=>{
        this.userProfile=response
        console.log(this.userProfile);
      },
      error:(err)=>console.error('Error fetching Profile', err)
    });
  }
  updateProfile():void{
    const updatedData={
      userId:this.userProfile.userId,
      userName:this.userProfile.userName,
      userEmail:this.userProfile.userEmail,
      userPhone:this.userProfile.userPhone
    }
    this.userSettings.updateUser(updatedData).subscribe({
      next: response => {
        console.log('Profile updated successfully:', response);
        this.loadUserProfile(); // Reload the user profile after update
      },
      error: err => console.error('Error updating profile', err)
    });
  }

}
