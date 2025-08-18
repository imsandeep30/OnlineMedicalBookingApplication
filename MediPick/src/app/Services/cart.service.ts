import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Medicine {
  medicineId: number;
  medicineName: string;
  description: string;
  price: number;
  brand: string;
  quantityAvailable: number;
}

interface CartDto {
  items: any[];
  totalPrice: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5184/api/Cart';

  constructor(private http: HttpClient) {}

  // Helper method to get Authorization header
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt') || '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getCart(userId: string): Observable<CartDto> {
    return this.http.get<CartDto>(`${this.apiUrl}/${userId}`, {
      headers: this.getAuthHeaders()
    });
  }
  addToCart(userId: string, medicine: Medicine, quantity: number): Observable<any> {
    const medicineID: string = medicine.medicineId.toString();
    return this.http.post(`${this.apiUrl}/add-or-update-item/${userId}/${medicineID}/${quantity}`, medicine, {
      headers: this.getAuthHeaders(),
      responseType: 'text'
    });
  }
  removeItem(userId: string, medicineId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove-item/${userId}/${medicineId}`, {
      headers: this.getAuthHeaders()
    });
  }
}
