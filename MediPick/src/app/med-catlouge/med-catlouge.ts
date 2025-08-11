import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {FormsModule } from '@angular/forms';
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
      next: data => this.medicines = data,
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

    this.http.post<Medicine[]>('http://localhost:5184/api/Medicine/filter', filterPayload, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
    .subscribe({
      next: data => {
        if (this.sortBy === 'price') {
          data.sort((a, b) => a.price - b.price);
        } else {
          data.sort((a, b) => a.medicineName.localeCompare(b.medicineName));
        }
        this.medicines = data;
      },
      error: err => console.error('Error filtering medicines', err)
    });
  }

  addToCart(medicine: Medicine) {
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
