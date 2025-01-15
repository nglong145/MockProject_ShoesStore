import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Features/auth/services/auth.service';
import { User } from '../../../Features/auth/models/user.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  user: User | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Đăng ký vào BehaviorSubject để nhận thông tin người dùng khi có sự thay đổi
    this.authService.user().subscribe((user) => {
      this.user = user; // Cập nhật người dùng vào header
    });
  }

  logout(): void {
    this.authService.logout();
    this.user = null;
  }
}
