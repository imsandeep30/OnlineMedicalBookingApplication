import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-payment-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment-component.html',
  styleUrls: ['./payment-component.css']
})
export class PaymentComponent implements OnInit {
  paymentMethod: string = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.paymentMethod = params['method'] || '';
    });
  }
  goBack() {
    window.history.back();
  }
}
