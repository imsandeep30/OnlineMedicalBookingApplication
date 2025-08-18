import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-access-denied',
  imports: [CommonModule],
  templateUrl: './access-denied.html',
  styleUrls: ['./access-denied.css']
})
export class AccessDenied {
  Role: string = localStorage.getItem('role')!; 
  url: string = ''; 

  constructor(private router: Router) {
    if (this.Role === 'Admin') {
      this.url = '/admin-dashboard';
    } 
    else if(this.Role == 'User') {
      this.url = '/customer-dashboard';
    }
    else{
      this.url = '/home';
    }
  }
  goBack(){
    this.router.navigate([this.url]);
  }
}
