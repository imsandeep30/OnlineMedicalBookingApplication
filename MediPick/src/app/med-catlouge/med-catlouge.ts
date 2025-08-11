import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart.service';  // adjust path as per your project
import { RouterLink } from '@angular/router';
import { TopNavbar } from '../top-navbar/top-navbar';
interface Medicine {
  medicineId: number;
  medicineName: string;
  description: string;
  price: number;
  brand: string;
  quantityAvailable: number;
}

@Component({
  selector: 'app-med-catlouge',
  standalone: true,
  imports: [CommonModule,RouterLink, TopNavbar],
  templateUrl: './med-catlouge.html',
  styleUrls: ['./med-catlouge.css']
})
export class MedCatlouge implements OnInit {
  
  medicines: Medicine[] = [];
  filteredMedicines: Medicine[] = [];
  searchTerm = '';
  sortBy = 'name';

  constructor(private http: HttpClient, private cartService: CartService) {}

  ngOnInit() {
    this.loadMedicines();
  }

  loadMedicines() {
    this.http.get<Medicine[]>('http://localhost:5184/api/Medicine/all-medicines', {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
      .subscribe({
        next: (data) => {
          console.log('Medicines fetched successfully:', data);
          this.medicines = data;
        },
        error: (err) => {
          console.error('Error fetching medicines', err);
        }
      });
  }

  addToCart(medicine: Medicine) {
    console.log('Adding to cart:', medicine);
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.error('User not logged in');
      return;
    }
    this.cartService.addToCart(userId, medicine).subscribe({
      next: () => console.log('Added to cart:', medicine),
      error: err => console.error('Failed to add to cart', err)
    });
  }
}
