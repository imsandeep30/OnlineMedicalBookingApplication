import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  private apiUrl = 'http://localhost:5184/api/Email/send';  // Your backend email API

  constructor(private http: HttpClient) {}

  sendPaymentSuccessEmail(to: string): Observable<any> {
    console.log(to);
    const body = `
      <html>
        <body style="font-family: Arial, sans-serif; margin:0; padding:0; background-color:#f4f4f4;">
          <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
              <td align="center">
                <table width="600" cellpadding="0" cellspacing="0" border="0" style="background-color:#ffffff; border-radius:10px; box-shadow:0 4px 8px rgba(0,0,0,0.1); overflow:hidden;">
                  
                  <!-- Header / Logo -->
                  <tr>
                    <td align="center" style="padding:20px; background-color:#4CAF50; color:white;">
                      <h1 style="margin:0;">✨ MEDIPICK ✨</h1>
                      <p style="margin:0; font-size:14px;">Your trusted medicine partner</p>
                    </td>
                  </tr>
                  
                  <!-- Body -->
                  <tr>
                    <td style="padding:30px; color:#333; font-size:16px;">
                      <h2 style="color:#4CAF50;">🎉 Payment Successful! 🎉</h2>
                      <p>Dear Customer,</p>
                      <p>Your payment has been processed successfully. Thank you for trusting MEDIPICK! 💊</p>
                      <p style="text-align:center; margin:30px 0;">
                        <span style="font-size:50px;">💚</span>
                      </p>
                      <p>Order details and next steps will be sent to you shortly.</p>
                    </td>
                  </tr>

                  <!-- Footer -->
                  <tr>
                    <td style="padding:20px; background-color:#f4f4f4; text-align:center; font-size:12px; color:gray;">
                      This is an automated email from MEDIPICK. Please do not reply. 📧
                    </td>
                  </tr>

                </table>
              </td>
            </tr>
          </table>
        </body>
      </html>
      `;
    // Add JWT token in Authorization header
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('jwt')
    });
    return this.http.post(this.apiUrl, {
      to,
      subject: 'Payment Successful',
      body
    }, { headers ,responseType: 'text' });
  }
  OtpGeneration(otp:string,to:string){
    const body = `
      <html>
          <body style="margin:0; padding:0; background-color:#121212; font-family: Arial, sans-serif; color:#fff;">
            <table align="center" width="100%" cellpadding="0" cellspacing="0" style="padding: 20px;">
              <tr>
                <td align="center">
                  <!-- Card Container -->
                  <div style="background-color:#1E1E1E; max-width:500px; width:100%; border-radius:10px; padding:30px; box-shadow:0 4px 12px rgba(0,0,0,0.3); text-align:center;">
                    <div style="font-size:40px; margin-bottom:10px;">⚠️</div>
                    <h2 style="color:#FFB84D; margin-bottom:10px;">Password Reset OTP</h2>
                    <p style="color:#ccc; font-size:14px; margin-bottom:20px;">
                      We received a request to reset your password. Please use the OTP below to proceed:
                    </p>
                    <div style="background-color:#262626; display:inline-block; padding:12px 20px; border-radius:6px; font-size:24px; font-weight:bold; color:#00E676; letter-spacing:2px;">
                      ${otp}
                    </div>
                    <p style="color:#999; font-size:13px; margin-top:20px;">
                      ⛔ Do not share this OTP with anyone for security reasons.<br>
                      This OTP is valid for <b>10 minutes</b>.
                    </p>
                  </div>
                </td>
              </tr>
            </table>
          </body>
        </html>
    `;
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('jwt')
    });
    return this.http.post(this.apiUrl, {
      to,
      subject: 'Otp Generated',
      body
    }, { headers ,responseType: 'text' });
  }
}
