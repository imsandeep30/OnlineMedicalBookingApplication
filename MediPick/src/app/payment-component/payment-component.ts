import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { EmailService } from '../Services/email-service';
//transaction playload
interface transactionPlayload{
  orderId:number;
  paymentMethod:string;
}
@Component({
  selector: 'app-payment-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment-component.html',
  styleUrls: ['./payment-component.css']
})
export class PaymentComponent implements OnInit {
  OrderId : number =0;
  paymentMethod: string = '';
  processing: boolean = false;
  paymentSuccess: boolean = false;
  selectedMethod: string = '';
  customerEmail : string= localStorage.getItem("userEmail") || '';
  constructor(private route: ActivatedRoute, private router: Router,private http:HttpClient,private emailService: EmailService) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.OrderId = params['Id'];
      this.paymentMethod = params['method'] || '';
    });
  }

  onPaymentMethodClick(method: string) {
  if (this.processing || this.paymentSuccess) return; // prevent double clicks

  this.selectedMethod = method;
  this.processing = true;
  this.paymentSuccess = false;

  const payload: transactionPlayload = {
    orderId: this.OrderId,
    paymentMethod: this.paymentMethod
  };

  this.http.post<any>('http://localhost:5184/api/Transaction/AddTransaction', payload,{
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
        }).subscribe({
        next: (response) => {
          console.log('Transaction response:', response);
          this.onPaymentSuccess();
          setTimeout(() => {
            this.processing = false;
            this.paymentSuccess = true;
          }, 2000);
        },
    error: (err) => {
      console.error('Transaction error:', err);
      this.processing = false;
      alert('Payment failed, please try again.');
    }
  });
}

  bookMore() {
    this.router.navigate(['customer-dashboard/medicine-catalogue']); // adjust path as needed
  }

  goBack() {
    window.history.back();
    this.cancelOrder(this.OrderId);
  }
  cancelOrder(orderId: number): void {
    const url = `http://localhost:5184/api/Order/Cancel/${orderId}`;
    this.http.delete(url, { headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }, responseType: 'text' }).subscribe({
      next: (message) => {
        console.log(message);
      },
      error: (err) => {
        console.log("Error cancelling order:", err);
        alert(err.error || 'Failed to cancel the order');
      }
    });
  }
  onPaymentSuccess() {
    this.emailService.sendPaymentSuccessEmail(this.customerEmail)
      .subscribe({
        next: () => console.log('Email sent successfully!'),
        error: err => console.error('Failed to send email', err)
      });
  }
}
