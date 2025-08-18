import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule,Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { EmailService } from '../Services/email-service';
import { HttpHeaders } from '@angular/common/http';
interface ResponsePlayload{
  userId: number;
  name :string;
  password : string;
  email : string;
}
interface EmailRequest{
  gmail : string;
}
interface PasswordResetDTO
{
    UserId : number;
    OldPassword:string;
    NewPassword : string;
}
@Component({
  selector: 'app-forgotpassword',
  standalone: true,
  imports: [CommonModule,RouterModule,FormsModule],
  templateUrl: './forgotpassword.html',
  styleUrls: ['./forgotpassword.css'] 
})
export class Forgotpassword {
  email = '';
  otp = '';
  generatedOtp = '';
  userId: number | null = null;
  step = 1; // 1: enter email, 2: enter OTP, 3: reset password
  newPassword = '';
  confirmPassword = '';
  EmailRequest : EmailRequest ={
    gmail : '',
  }
  PasswordResetDTO : PasswordResetDTO = {
    UserId : 0,
    OldPassword:'',
    NewPassword : '',
  }
  ResponsePlayload : ResponsePlayload ={
      userId: 0,
      name : '',
      password : '',
      email : '',
  }
  constructor(private route : Router,private http : HttpClient,private emailService : EmailService){}
  emailError = false;
  otpError = false;
  passwordError = false;
  isSubmitting = false;
  searchEmail() {
    if (this.isSubmitting) return; // prevent double click
    this.isSubmitting = true;
    this.http.post<ResponsePlayload>('http://localhost:5184/api/User/SearchMail', this.EmailRequest)
      .subscribe({
        next: (res) => {
          this.isSubmitting = false;
          console.log(res);
          if (res) {
            this.PasswordResetDTO.OldPassword=res.password;
            this.PasswordResetDTO.UserId=res.userId;
            this.emailError = false;
            this.generatedOtp = Math.floor(100000 + Math.random() * 900000).toString();
            this.emailService.OtpGeneration(this.generatedOtp, this.EmailRequest.gmail)
              .subscribe(() => console.log('OTP sent successfully'));
            this.step = 2;
          } else {
            this.emailError = true;
          }
        },
        error: (err) => {
          if (err.status === 404) {
            this.isSubmitting=false;
            this.emailError = true; // show "Email not found"
          }
        }
      });
  }

  OtpCheck() {
    if (this.otp === this.generatedOtp) {
      this.otpError = false;
      this.step = 3;
    } else {
      this.otpError = true;
    }
  }

  PasswordValidate() {
    if (this.newPassword !== this.confirmPassword) {
      this.passwordError = true;
      return;
    }
    this.passwordError = false;

    this.PasswordResetDTO.NewPassword = this.newPassword;
    console.log("Sending DTO:", this.PasswordResetDTO);

    this.http.post<any>('http://localhost:5184/api/User/ResetPassword', this.PasswordResetDTO)
      .subscribe({
        next: () => {
          // console.log(reset);
          this.step = 4;
        },
        error: err => console.error('Error resetting password', err)
      });
  }

 }