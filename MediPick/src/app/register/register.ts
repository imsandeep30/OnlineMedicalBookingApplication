import { Component } from '@angular/core';
import {  FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { User } from '../user';
import { RouterModule,Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,  FormsModule, HttpClientModule, RouterModule], 
  templateUrl: './register.html',
  styleUrls: ['./register.css'] 
})
export class Register {
  user: User = {
    userName: '',
    userPhone: '',
    userEmail: '',
    userPassword: '',
    userAddress: '',
    role: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  registerUser() {
    this.http.post('http://localhost:5184/api/User/Register', this.user, { responseType: 'text' })
      .subscribe({
        next: (response) => {
          console.log('Server response:', response);
          alert('User registered successfully!');
          this.user = {
            userName: '',
            userPhone: '',
            userEmail: '',
            userPassword: '',
            userAddress: '',
            role: ''
          };
          // Optionally, redirect to login or another page
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('Registration failed:', err);
          alert('Error during registration. Check console for details.');
        }
      });
  }
}
