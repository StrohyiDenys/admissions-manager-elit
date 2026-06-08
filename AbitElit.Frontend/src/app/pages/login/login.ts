import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html', 
  styleUrls: ['./login.css']   
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onLogin() {
    this.errorMessage = '';
    const loginData = { username: this.username, password: this.password };

    this.authService.login(loginData).subscribe({
      next: (response) => {
        this.authService.saveToken(response.token);
        
        const role = this.authService.getRole();
        if (role === 'Admin') {
          this.router.navigate(['/admin']);
        } else if (role === 'Reader') {
          this.router.navigate(['/reader']);
        } else {
          this.errorMessage = 'Невідома роль!';
        }
      },
      error: () => {
        this.errorMessage = 'Невірний логін або пароль!';
      }
    });
  }
}