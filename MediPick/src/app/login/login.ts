import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../DTOS/login-dto';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  
  login: LoginDto = { userEmail: '', userPassword: '' };
  errMessage: string = '';
  isLoading: boolean = false; //  added loading state
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
  loginUser() {
    this.errMessage = '';
    this.isLoading = true; //  start loading

    this.http.post<any>('http://localhost:5184/api/User/Login', this.login)
      .subscribe({
        next: (data) => {
          this.isLoading = false; //  stop loading

          if (data && data.token) {
            // Save user details
            localStorage.setItem('jwt', data.token);
            localStorage.setItem('userId', data.userId);
            localStorage.setItem('userName', data.userName);
            localStorage.setItem('role', data.role);
            localStorage.setItem('userEmail', data.userEmail);
            localStorage.setItem('active', 'active');

            // Redirect based on role
            if (data.role === 'Admin') {
              this.router.navigate(['/admin-dashboard']);
            } else if (data.role === 'User') {
              this.router.navigate(['/customer-dashboard']);
            } else {
              this.errMessage = 'Invalid Email or Password';
            }
          } else {
            this.errMessage = 'Invalid Email or Password';
          }
        },
        error: (err) => {
          console.error('Login error:', err);
          this.isLoading = false; // stop loading
          
          this.errMessage = err?.error?.message || 'Invalid Email or Password';
        }
      });
  }
}
