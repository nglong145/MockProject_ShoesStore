import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '../auth/models/user.model';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-presonal-info',
  imports: [CommonModule, FormsModule],
  templateUrl: './presonal-info.component.html',
  styleUrl: './presonal-info.component.css'
})
export class PresonalInfoComponent {
  constructor(private authService: AuthService) {}
  user$?: Observable<User>;
  editUser!: User;
  userSubsription?: Subscription;

  ngOnInit() : void{
    this.user$ = this.authService.getUserInfo();
    this.user$.subscribe((user) => {
      this.editUser = { ...user };
    });
  }
  
  ngOnDestroy() : void {
    this.userSubsription?.unsubscribe();
  }

  updateProfile(): void {
    this.userSubsription = this.authService.updateUserInfo(this.editUser).subscribe((user) => {
      this.user$ = this.authService.getUserInfo();
    });
    console.log('Thông tin đã được cập nhật:');
  }
}
