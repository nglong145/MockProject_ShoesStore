import { Component } from '@angular/core';

@Component({
  selector: 'app-list-customer',
  imports: [],
  templateUrl: './list-customer.component.html',
  styleUrl: './list-customer.component.css'
})
export class ListCustomerComponent {
  onClick(event : Event):void{
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
      // Thực hiện các hành động khi chọn "Block"
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
      // Thực hiện các hành động khi chọn "Active"
    }
    
  }
}
