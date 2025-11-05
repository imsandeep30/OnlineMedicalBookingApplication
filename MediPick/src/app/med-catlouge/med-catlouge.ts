import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../Services/cart.service';
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
  quantity?: number;
  imageUrl?: string; // added
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
  selectedKeywords: string[] = [];
  private searchSubject = new Subject<string>();

  // Keywords
  keywordsList: string[] = ['Fever', 'Cold', 'Cough', 'Headache', 'Pain', 'Allergy'];
  newKeyword: string = '';

  constructor(private http: HttpClient, private cartService: CartService) {}

  ngOnInit() {
    this.loadMedicines();

    this.searchSubject.pipe(debounceTime(500)).subscribe(searchText => {
      this.filterMedicines(searchText);
    });
  }

  loadMedicines() {
    this.http.get<Medicine[]>('http://localhost:5184/api/Medicine/all-medicines', {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
    .subscribe({
      next: data => {
        this.medicines = data.map(m => ({
          ...m,
          quantity: 1,
          imageUrl: `Assets/${m.medicineName.trim().replace(/\s+/g, '')}.jpg`,
        }));
        console.log(this.medicines);
      },
      error: err => console.error('Error fetching medicines', err)
    });
  }

  onSearchChange(searchValue: string) {
    this.searchSubject.next(searchValue);
  }

  toggleKeyword(keyword: string) {
    if (this.selectedKeywords.includes(keyword)) {
      this.selectedKeywords = this.selectedKeywords.filter(k => k !== keyword);
    } else {
      this.selectedKeywords.push(keyword);
    }
    this.filterMedicines(this.searchTerm);
  }

  addKeyword() {
    const trimmed = this.newKeyword.trim();
    if (trimmed && !this.keywordsList.includes(trimmed)) {
      this.keywordsList.push(trimmed);
    }
    if (trimmed && !this.selectedKeywords.includes(trimmed)) {
      this.selectedKeywords.push(trimmed);
    }
    this.newKeyword = '';
    this.filterMedicines(this.searchTerm);
  }

  removeKeyword(keyword: string) {
    this.selectedKeywords = this.selectedKeywords.filter(k => k !== keyword);
    this.filterMedicines(this.searchTerm);
  }

  filterMedicines(searchText: string) {
    const filterPayload = {
      searchText: searchText || null,
      min: null,
      max: null,
      onlyAvailable: false,
      problemKeywords: this.selectedKeywords.length > 0 ? this.selectedKeywords : null
    };

    this.http.post<Medicine[]>('http://localhost:5184/api/Medicine/filter-medicines', filterPayload, {
      headers: { 'Authorization': 'Bearer ' + localStorage.getItem('jwt') }
    })
    .subscribe({
      next: data => {
        console.log('Sending filter payload:', filterPayload);
        if (this.sortBy === 'price') {
          data.sort((a, b) => a.price - b.price);
        } else {
          data.sort((a, b) => a.medicineName.localeCompare(b.medicineName));
        }
        this.medicines = data.map(m => ({
          ...m,
          quantity: 1,
          imageUrl: `Assets/${m.medicineName.trim().replace(/\s+/g, '')}.jpg`
        }));
      },
      error: (err) => console.error('Error filtering medicines', err)
    });
  }

  decreaseQuantity(medicine: Medicine) {
    if (medicine.quantity && medicine.quantity > 1) {
      medicine.quantity--;
    }
  }

  increaseQuantity(medicine: Medicine) {
    medicine.quantity = (medicine.quantity ?? 0) + 1;
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
      error: ( err : any) => console.error('Failed to add to cart', err)
    });
  }
}
