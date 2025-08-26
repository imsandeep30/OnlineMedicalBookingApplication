import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { EmailService } from '../Services/email-service';

interface transactionPlayload {
  orderId: number;
  paymentMethod: string;
}

@Component({
  selector: 'app-payment-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment-component.html',
  styleUrls: ['./payment-component.css']
})
export class PaymentComponent implements OnInit {
  originalOrderId: number | null = null;
  OrderId: number = 0;
  paymentMethod: string = '';
  processing: boolean = false;
  paymentSuccess: boolean = false;
  selectedMethod: string = '';
  customerEmail: string = localStorage.getItem("userEmail") || '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private emailService: EmailService,
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.OrderId = params['Id'];
      this.paymentMethod = params['method'] || '';
    });

    this.verifyOrderOwnership(this.OrderId);
  }

  onPaymentMethodClick(method: string) {
    if (this.processing || this.paymentSuccess) return;

    this.selectedMethod = method;
    this.processing = true;
    this.paymentSuccess = false;

    const payload: transactionPlayload = {
      orderId: this.OrderId,
      paymentMethod: this.paymentMethod
    };

    this.http.post<any>('http://localhost:5184/api/Transaction/AddTransaction', payload, {
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
    this.router.navigate(['customer-dashboard/medicine-catalogue']);
  }

  goBack() {
    window.history.back();
    this.cancelOrder(this.OrderId);
  }

  cancelOrder(orderId: number): void {
    if (!orderId) return;

    const url = `http://localhost:5184/api/Order/Cancel/${orderId}`;
    this.http.delete(url, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') },
      responseType: 'text'
    }).subscribe({
      next: (message) => console.log("Order cancelled:", message),
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

  verifyOrderOwnership(orderId: number) {
    const url = `http://localhost:5184/api/Order/GetByOrderId?orderId=${orderId}`;

    this.http.get<any>(url, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    }).subscribe({
      next: (order) => {
        const loggedInUserId = Number(localStorage.getItem("userId"));
        console.log("Fetched order:", order);
        console.log("Logged-in userId:", loggedInUserId);
        console.log("Original orderId:", this.originalOrderId);

        if (order.userId !== loggedInUserId) {
          this.router.navigate(['/access-denied']);
        }
      },
      error: () => {
        this.router.navigate(['/access-denied']);
      }
    });
  }
}
