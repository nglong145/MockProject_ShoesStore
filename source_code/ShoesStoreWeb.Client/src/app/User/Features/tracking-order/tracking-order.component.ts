import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tracking-order',
  imports: [CommonModule, FormsModule],
  templateUrl: './tracking-order.component.html',
  styleUrl: './tracking-order.component.css'
})
export class TrackingOrderComponent {
  orders = [
    {
      id: 'ĐH250101-0001',
      productName: "Giày Adidas Adizero Adios Pro EVO 1 'White Black' IH5564",
      productImage: 'https://via.placeholder.com/80',
      price: 1200000,
      status: 'Đã giao'
    },
    {
      id: 'ĐH250103-0001',
      productName: "Giày Adidas Adizero Adios Pro EVO 1 'White Black' IH5564",
      productImage: 'https://via.placeholder.com/80',
      price: 1200000,
      status: 'Đang giao'
    }
  ];
}
