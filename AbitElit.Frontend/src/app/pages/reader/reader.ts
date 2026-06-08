import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApplicantService, Applicant } from '../../services/applicant.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reader',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reader.html', 
  styleUrls: ['./reader.css']
})
export class ReaderComponent implements OnInit {
  applicants: Applicant[] = [];
  
  filterMinScore: number | null = null;
  filterSchool: number | null = null;
  isFilterOpen = false; 

  role = 'Член приймальної комісії';

  constructor(
    private applicantService: ApplicantService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadApplicants(); 
  }

  loadApplicants() {
    this.applicantService.getAll().subscribe({
      next: (data) => {
        this.applicants = data;
      },
      error: (err) => console.error('Помилка завантаження', err)
    });
  }

  applyFilter() {
    this.applicantService.getFiltered(this.filterMinScore || undefined, this.filterSchool || undefined)
      .subscribe({
        next: (data) => this.applicants = data,
        error: (err) => console.error('Помилка фільтрації', err)
      });
  }

  //Відкрити-закрити фільтр
  toggleFilter() {
    this.isFilterOpen = !this.isFilterOpen;
  }

  exportWord() {
    this.applicantService.exportToWord(this.filterMinScore || undefined, this.filterSchool || undefined)
      .subscribe({
        next: (blob) => {
          const url = window.URL.createObjectURL(blob);
          const a = document.createElement('a');
          a.href = url;
          a.download = `Звіт_${new Date().toLocaleDateString()}.docx`;
          a.click();
          window.URL.revokeObjectURL(url);
        },
        error: (err) => console.error('Помилка експорту', err)
      });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}