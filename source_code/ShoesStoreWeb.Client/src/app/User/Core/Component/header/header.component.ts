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
    this.loadUser();
  }

  loadUser(): void {
    this.authService.getUserInfo().subscribe(
      (userInfo) => {
        this.user = userInfo;
      },
      (error) => {
        console.error('Error loading user info:', error);
      }
    );
  }

  logout(): void {
    this.authService.logout();
    this.user = null;
  }
}
