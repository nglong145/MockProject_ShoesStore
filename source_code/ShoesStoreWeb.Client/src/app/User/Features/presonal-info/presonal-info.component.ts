import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-presonal-info',
  imports: [CommonModule, FormsModule],
  templateUrl: './presonal-info.component.html',
  styleUrl: './presonal-info.component.css'
})
export class PresonalInfoComponent {
  user = {
    name: 'Trần Thanh C',
    email: 'tranthanhc@gmail.com',
    phone: '0265236154',
    address: 'Yên Mỹ - Hưng Yên'
  };

  updateProfile() {
    // Thêm logic để cập nhật thông tin
    console.log('Thông tin đã được cập nhật:', this.user);
  }
}
