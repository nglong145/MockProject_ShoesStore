import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../models/product.model';
import { Router } from '@angular/router';
import { ProductService } from '../service/product.service';

@Component({
  selector: 'app-list-product',
  imports: [CommonModule],
  templateUrl: './list-product.component.html',
  styleUrl: './list-product.component.css'
})
export class ListProductComponent {

  products$? : Observable<Product[]>
  productsSub?: Subscription;

  constructor(private router: Router, private productService: ProductService){
  }
  
  ngOnInit(): void{
    this.products$ = this.productService.getAllProduct();
  }

  onClick(event : Event):void{
    const selectedValue = (event.target as HTMLSelectElement).value;
    if (selectedValue === 'Block') {
      console.log('User selected Block');
    } else if (selectedValue === 'Active') {
      console.log('User selected Active');
    }
    
  }

  updateProduct(productId: string){
    this.router.navigateByUrl(`admin/product/update/${productId}`);
  }

  // Khi click delete chỉ change status về string chứ không xóa
  deleteProduct(productId: string){
    if(confirm('Bạn chắc chắn muốn xóa Product này?')){
      this.productsSub = this.productService.deleteProduct(productId).subscribe(
        {
          next: response =>{
            this.products$ = this.productService.getAllProduct();
          },
          error: err =>{
            console.log(err);
          }
        }
      )
    }
  }
  addProduct(){
    this.router.navigateByUrl('admin/product/add');
  }
}