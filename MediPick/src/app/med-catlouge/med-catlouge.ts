import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../cart.service';
import { RouterLink } from '@angular/router';
import { TopNavbar } from '../top-navbar/top-navbar';
import { debounceTime, Subject } from 'rxjs';

interface Medicine {
  medicineId: number;
  medicineName: string;
  description: string;
  price: number;
  brand: string;
  quantityAvailable: number;
  quantity?: number; // optional property for local quantity control
}

@Component({
  selector: 'app-med-catlouge',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, TopNavbar],
  templateUrl: './med-catlouge.html',
  styleUrls: ['./med-catlouge.css']
})
export class MedCatlouge implements OnInit {

  medicines: Medicine[] = [];
  searchTerm = '';
  sortBy = 'name';
  private searchSubject = new Subject<string>();

  constructor(private http: HttpClient, private cartService: CartService) {}

  ngOnInit() {
    this.loadMedicines();

    // Debounce search input
    this.searchSubject.pipe(debounceTime(300)).subscribe(searchText => {
      this.filterMedicines(searchText);
    });
  }

  loadMedicines() {
    this.http.get<Medicine[]>('http://localhost:5184/api/Medicine/all-medicines', {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
    .subscribe({
      next: data => {
        // Add quantity property initialized to 1 for each medicine
        this.medicines = data.map(m => ({ ...m, quantity: 1 }));
      },
      error: err => console.error('Error fetching medicines', err)
    });
  }

  onSearchChange(searchValue: string) {
    this.searchSubject.next(searchValue);
  }

  filterMedicines(searchText: string) {
    const filterPayload = {
      searchText: searchText || null,
      min: null,
      max: null,
      onlyAvailable: false,
      keywords: null
    };

    this.http.post<Medicine[]>('http://localhost:5184/api/Medicine/filter-medicines', filterPayload, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
    .subscribe({
      next: data => {
        if (this.sortBy === 'price') {
          data.sort((a, b) => a.price - b.price);
        } else {
          data.sort((a, b) => a.medicineName.localeCompare(b.medicineName));
        }
        // Set quantity to 1 for filtered results as well
        this.medicines = data.map(m => ({ ...m, quantity: 1 }));
      },
      error: err => console.error('Error filtering medicines', err)
    });
  }

  decreaseQuantity(medicine: Medicine) {
    if (medicine.quantity && medicine.quantity > 1) {
      medicine.quantity--;
    }
  }

  increaseQuantity(medicine: Medicine) {
    if (medicine.quantity) {
      medicine.quantity++;
    } else {
      medicine.quantity = 1;
    }
  }

  addToCart(medicine: Medicine) {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      console.error('User not logged in');
      return;
    }
    const qty = medicine.quantity ?? 1;
    this.cartService.addToCart(userId, medicine, qty).subscribe({
      next: () => console.log('Added to cart:', medicine),
      error: err => console.error('Failed to add to cart', err)
    });
  }
  colors = [
      '#a3d9a5', '#f7f3b2', '#c2f0fc', '#e7cbf5', '#f4b0b0', '#a9c5ba',
      '#5a3e36', '#2f4f4f', '#4b0082', '#800000', '#355e3b', '#3b3b6d',
      '#6b4226', '#4a4a4a', '#5c4033', '#3e5f8a', '#7b3f00', '#403d58',
      '#2c3e50', '#34495e', '#1b2631', '#4a235a', '#7d6608', '#784212',
      '#512e5f', '#1c2833', '#283747', '#6e2c00', '#4d5656', '#6c3483',
      '#6e2f2f', '#1b4f72', '#4a235a', '#7e5109', '#5d6d7e', '#3a3b3c'
  ];
  getMedicineColor(name: string): string {
    const index = name.charCodeAt(0) % this.colors.length;
    return this.colors[index];
  }
}
