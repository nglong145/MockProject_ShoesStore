import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Features/auth/services/auth.service';
import { User } from '../../../Features/auth/models/user.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { IMG_URL } from '../../../../app.config';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  user: User | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.user$.subscribe((user) => {
      this.user = user;
    });

    if (!this.user) {
      this.authService.getUserInfo().subscribe({
        next: (data) => {
          this.authService.setUser(data);
        },
        error: (err) => {
          console.error('Không thể lấy thông tin người dùng:', err);
        },
      });
    }
  }

  logout(): void {
    this.authService.logout();
  }
}
