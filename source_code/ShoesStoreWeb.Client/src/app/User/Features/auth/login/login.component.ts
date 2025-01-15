import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  model: LoginRequest;
  loading = false;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      email: '',
      password: '',
    };
  }

  onFormSubmit(): void {
    console.log(this.model);
    this.authService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);

        // Sau khi đăng nhập thành công, chuyển hướng về trang chủ và cập nhật thông tin người dùng
        this.router.navigateByUrl('/').then(() => {
          // Cập nhật thông tin người dùng vào BehaviorSubject
          this.authService.getUserInfo().subscribe((user) => {
            this.authService.setUser(user); // Cập nhật thông tin người dùng vào BehaviorSubject
          });
        });
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
