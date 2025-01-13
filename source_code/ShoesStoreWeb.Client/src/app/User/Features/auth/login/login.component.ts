import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  model: LoginRequest;

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

  onFormSubmit() {
    console.log(this.model);
    this.authService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.cookieService.set(
          'Authentication',
          `Bearer ${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict'
        );

        this.authService.setUser({ email: this.model.email });
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
