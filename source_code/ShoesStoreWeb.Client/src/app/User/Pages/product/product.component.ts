import { Component } from '@angular/core';
import { ProductDetailComponent } from '../../Features/Product/product-detail/product-detail.component';
import { RelatedProductComponent } from '../../Features/Product/related-product/related-product.component';
import { ProductDescriptionComponent } from '../../Features/Product/product-description/product-description.component';

@Component({
  selector: 'app-product',
  imports: [
    ProductDetailComponent,
    RelatedProductComponent,
    ProductDescriptionComponent,
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
})
export class ProductComponent {}
