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
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getUserProfile(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/profile/${userId}`, { headers: this.getAuthHeaders() });
  }
  
  deleteUserProfile(userId: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/delete/${userId}`, { headers: this.getAuthHeaders(),responseType: 'text' });
  }
  updateUserProfile(userData: any): Observable<string> {
    return this.http.put(`${this.apiUrl}/Update`, userData, {
      headers: this.getAuthHeaders(),
      responseType: 'text'
    });
  }
  resetPassword(userId: number, oldPassword: string, newPassword: string): Observable<string> {
    const url = `${this.apiUrl}/ResetPassword/${userId}/${oldPassword}/${newPassword}`;
    return this.http.get(url, { headers: this.getAuthHeaders(), responseType: 'text' });
  }

}
