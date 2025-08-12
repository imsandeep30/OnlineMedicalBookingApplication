import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserSettings {
  private apiUrl = 'http://localhost:5184/api/User';

  constructor(private http: HttpClient) { }
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwt') || '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getUserProfile(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/profile/${userId}`, { headers: this.getAuthHeaders() });
  }
  updateUser(userData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/Update`, userData, {
      headers: this.getAuthHeaders()
    });
  }

}
