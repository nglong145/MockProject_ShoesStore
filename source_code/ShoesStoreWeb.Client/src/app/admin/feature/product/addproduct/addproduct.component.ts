import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../models/product.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ProductService } from '../service/product.service';

@Component({
  selector: 'app-addproduct',
  imports: [FormsModule, CommonModule],
  templateUrl: './addproduct.component.html',
  styleUrl: './addproduct.component.css'
})
export class AddproductComponent {
  model: Product;
  // brand$?: BrandService
  addProductSub?: Subscription;
  baseUrl: string = "https://localhost:7158/Images/Product/";


  constructor(private router: Router, private productService: ProductService){
    this.model = {
      productId: '',
      productName:'',
      productImage:'',
      price: 0,
      description:'',
      status:'',
      brandId: ''
    }
  }

  onClick(event : Event):void{
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
    
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.model.productImage = this.baseUrl + input.files[0].name; // Lấy tên file
    } else {
      this.model.productImage = ''; // Không có file được chọn
    }
  }
  onFormSubmit(){
    console.log(this.model);
    
  }
}
