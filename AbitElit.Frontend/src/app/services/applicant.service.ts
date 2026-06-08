import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export interface Applicant {
  id: number;
  lastName: string;
  firstName: string;
  examScore: number;
  schoolNumber: number;
}

@Injectable({ providedIn: 'root' }) 
export class ApplicantService {
  private apiUrl = 'http://localhost:5142/api/Applicants';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Applicant[]>(this.apiUrl);
  }

  getFiltered(minScore?: number, schoolNumber?: number) {
    let params = new HttpParams();
    if (minScore) params = params.set('minScore', minScore.toString());
    if (schoolNumber) params = params.set('schoolNumber', schoolNumber.toString());
    
    return this.http.get<Applicant[]>(`${this.apiUrl}/filter`, { params });
  }

  create(applicant: Applicant) {
    return this.http.post(this.apiUrl, applicant);
  }

  update(id: number, applicant: Applicant) {
    return this.http.put(`${this.apiUrl}/${id}`, applicant);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  exportToWord(minScore?: number, schoolNumber?: number) {
    let params = new HttpParams();
    if (minScore) params = params.set('minScore', minScore.toString());
    if (schoolNumber) params = params.set('schoolNumber', schoolNumber.toString());

    return this.http.get(`${this.apiUrl}/export`, { 
      params, 
      responseType: 'blob' 
    });
  }
}