import { Component } from '@angular/core';
import { AdminheaderComponent } from '../adminheader/adminheader.component';
import { AdminLayoutComponent } from '../../../Layout/admin-layout/admin-layout.component';
import { FormsModule } from '@angular/forms';
import { BrandService } from '../../feature/brand/service/brand-service.service';
import { Observable } from 'rxjs';
import { Brand } from '../../feature/brand/model/brand';
import { RouterLink } from '@angular/router';
import { Blog } from '../../feature/blog/models/blog.model';
import { Product } from '../../feature/product/models/product.model';
import { BlogService } from '../../feature/blog/services/blog.service';
import { ProductService } from '../../feature/product/service/product.service';
import { Customer } from '../../feature/customer/models/customer.model';
import { CustomerService } from '../../feature/customer/services/customer.service';

@Component({
  selector: 'app-index',
  imports: [AdminheaderComponent, FormsModule, RouterLink],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent {

  brands$?: Observable<Brand[]>;
  blogs$?: Observable<Blog[]>;
  products$?: Observable<Product[]>;
  customer$?: Observable<Customer[]>;


  brandCount: number = 0;
  blogCount: number = 0;
  productCount: number = 0;
  customerCount: number = 0;
  orderCount: number = 0;

  constructor(
    private brandService: BrandService,
    private blogService: BlogService,
    private productService: ProductService,
    private customerService: CustomerService,
  ) { }

  ngOnInit(): void {
    // Brand
    this.brands$ = this.brandService.getAllBrand();
    this.brands$.subscribe(
      {
        next: response => {
          this.brandCount = response.length;
          console.log(this.brandCount);
        },
        error: err => {
          console.log(err);
        }
      }
    );

    // Blog
    this.blogs$ = this.blogService.getAllBlog();
    this.blogs$.subscribe(
      {
       next: response =>{
        this.blogCount = response.length;
       },
       error: err =>{
        console.log(err);
       }
      }
    )

    // Product
    this.products$ = this.productService.getAllProduct();
    this.products$.subscribe(
      {
        next: response =>{
          this.productCount = response.length;
        },
        error: err =>{
          console.log(err);
        }
      }
    )

    // Customer
    this.customer$ = this.customerService.getAllCustomer();
    this.customer$.subscribe(
      {
        next: response =>{
          this.customerCount = response.length;
        },
        error: err =>{
          console.log(err);
        }
      }
    )
    // Order
  }
}
