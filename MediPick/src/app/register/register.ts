import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

interface Address {
  userId: number;
  userStreet: string;
  userCity: string;
  userState: string;
  userZipCode: string;
  userCountry: string;
}

interface User {
  userName: string;
  userPhone: string;
  userEmail: string;
  userPassword: string;
  userAddress: Address;
  role: string;
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule, RouterModule],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class Register {
  user: User = {
    userName: '',
    userPhone: '',
    userEmail: '',
    userPassword: '',
    userAddress: {
      userId: 0,
      userStreet: '',
      userCity: '',
      userState: '',
      userZipCode: '',
      userCountry: ''
    },
    role: 'User',
  };
  confirmPassword: string = '';
  role : any;
  constructor(private http: HttpClient, private router: Router) {}
  ngOnInit(): void {
    this.IsAlreadyIn();
  }
  IsAlreadyIn(){
    if(localStorage.getItem('jwt')!=null){
      this.role  = localStorage.getItem("role");
      if(this.role == 'User'){
        this.router.navigate(['/customer-dashboard']);
      }
      else if(this.role=='Admin'){
        this.router.navigate(['/admin-dashboard']);
      }
    }
  }
  registerUser() {
    // You may want to validate password and confirm password here before sending
    if (this.user.userPassword !== this.confirmPassword) {
      alert('Password and Confirm Password do not match!');
      return;
    }
    this.http.post('http://localhost:5184/api/User/Register', this.user, { responseType: 'text' })
      .subscribe({
        next: (response) => {
          console.log('Server response:', response);
          alert('User registered successfully!');
          console.log('Password:', this.user.userPassword);
          console.log('Confirm Password:', this.confirmPassword);
          this.resetForm();
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('Registration failed:', err);
          alert('Error during registration. Check console for details.');
        }
      });
  }

  resetForm() {
    this.user = {
      userName: '',
      userPhone: '',
      userEmail: '',
      userPassword: '',
      userAddress: {
        userId: 0,
        userStreet: '',
        userCity: '',
        userState: '',
        userZipCode: '',
        userCountry: ''
      },
      role: 'User',
    };
    this.confirmPassword = '';

  }
}
