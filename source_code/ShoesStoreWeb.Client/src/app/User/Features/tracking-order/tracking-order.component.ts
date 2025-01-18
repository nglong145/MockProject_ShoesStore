import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OrderVM } from '../models/Order';
import { Observable, Subscription } from 'rxjs';
import { PersonalInfoService } from '../service/personal-info.service';
import { User } from '../../auth/models/user.model';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-tracking-order',
  imports: [CommonModule, FormsModule],
  templateUrl: './tracking-order.component.html',
  styleUrl: './tracking-order.component.css'
})
export class TrackingOrderComponent {
  user$?: Observable<User>;
  orders$?: Observable<OrderVM[]>;
  orderSubsription?: Subscription;

  constructor(private personalInfoService: PersonalInfoService,private authService: AuthService) {}
  
    ngOnInit() : void{
      this.orders$ = this.personalInfoService.getAllUnpaidOrderOfUser();
      this.user$ = this.authService.getUserInfo();
    }
  
    ngOnDestroy() : void {
      this.orderSubsription?.unsubscribe();
    }
  
}
