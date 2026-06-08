import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5142/api/Auth';

  constructor(private http: HttpClient) {}

  login(data: any) {
    return this.http.post<{token: string}>(`${this.apiUrl}/login`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('jwt_token', token);
  }

  logout() {
    localStorage.removeItem('jwt_token');
  }

  getRole(): string {
    const token = localStorage.getItem('jwt_token');
    if (!token) return '';
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role || '';
    } catch(e) {
      return '';
    }
  }
}