import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../login-dto';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-login',
  imports: [FormsModule,RouterLink,CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  login:LoginDto={userEmail:'',userPassword:''};
  errMessage:string='';
  constructor(private http: HttpClient,private router:Router){}
  loginUser(){
    this.http.post<any>('http://localhost:5184/api/User/Login',this.login)
    .subscribe((data)=>{
      if(data!=null){
        console.log("Login successful");
        console.log(data);
        localStorage.setItem('jwt',data.token);
        localStorage.setItem('userId',data.userId);
        localStorage.setItem('userName',data.userName);
        localStorage.setItem('role',data.role);
        localStorage.setItem('active',"active")
        if(data.role==="Admin"){
          this.router.navigate(['/admin-dashboard']);
        }
        else if(data.role=='User'){
          this.router.navigate(['/customer-dashboard']);
        }else{
          this.errMessage="Invalid Email or Password";
        }
      }
    })
  }
}
