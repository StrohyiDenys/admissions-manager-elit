import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApplicantService, Applicant } from '../../services/applicant.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.html',
  styleUrls: ['./admin.css']
})
export class AdminComponent implements OnInit {
  applicants: Applicant[] = [];
  
  filterMinScore: number | null = null;
  filterSchool: number | null = null;
  isFilterOpen = false;

  role = 'Admin';

  showModal = false;
  modalTitle = '';   
  
  currentApplicant: Applicant = {
    id: 0,
    firstName: '',
    lastName: '',
    examScore: 0,
    schoolNumber: 0
  };

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
      next: (data) => this.applicants = data,
      error: (err) => console.error(err)
    });
  }

  applyFilter() {
    this.applicantService.getFiltered(this.filterMinScore || undefined, this.filterSchool || undefined)
      .subscribe({
        next: (data) => this.applicants = data,
        error: (err) => console.error(err)
      });
  }

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
          a.download = `Звіт_Адмін_${new Date().toLocaleDateString()}.docx`;
          a.click();
          window.URL.revokeObjectURL(url);
        }
      });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  dummyUpload() {
    alert('Функція імпорту з БД ще не реалізована на бэкенді :)');
  }


  openAddModal() {
    this.modalTitle = 'Додати абітурієнта';
    this.currentApplicant = { id: 0, firstName: '', lastName: '', examScore: 0, schoolNumber: 0 };
    this.showModal = true;
  }

  openEditModal(a: Applicant) {
    this.modalTitle = 'Редагувати абітурієнта';
    this.currentApplicant = { ...a }; 
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  saveApplicant() {
    if (this.currentApplicant.id === 0) {
      this.applicantService.create(this.currentApplicant).subscribe({
        next: () => {
          this.loadApplicants(); 
          this.closeModal();     
        },
        error: (err) => alert('Помилка!')
      });
    } else {
      this.applicantService.update(this.currentApplicant.id, this.currentApplicant).subscribe({
        next: () => {
          this.loadApplicants();
          this.closeModal();
        },
        error: (err) => alert('Помилка!')
      });
    }
  }

  deleteApplicant(id: number) {
    if (confirm('Ви впевнені, що хочете видалити цей запис?')) {
      this.applicantService.delete(id).subscribe({
        next: () => this.loadApplicants(),
        error: (err) => alert('Помилка!')
      });
    }
  }
}