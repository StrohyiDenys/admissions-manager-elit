import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { ReaderComponent } from './pages/reader/reader';
import { AdminComponent } from './pages/admin/admin';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'reader', component: ReaderComponent },
  { path: 'admin', component: AdminComponent },
  { path: '**', redirectTo: 'login' } //редірект  на логін при заході на сайт за різними адресами
];