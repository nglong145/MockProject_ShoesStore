import { Component, OnInit } from '@angular/core';
import { ProductDetailComponent } from '../../Features/Product/product-detail/product-detail.component';
import { RelatedProductComponent } from '../../Features/Product/related-product/related-product.component';
import { ProductDescriptionComponent } from '../../Features/Product/product-description/product-description.component';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Features/Product/services/product.service';
import { Product } from '../../Features/Product/models/product.model';
import { Subscription } from 'rxjs';
import { Size } from '../../Features/Product/models/size.model';

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
export class ProductComponent implements OnInit {
  id: string | null = null;
  product?: Product;
  paramsSubcription?: Subscription;
  sizes: Size[] = [];
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.paramsSubcription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
      },
    });

    if (this.id) {
      this.productService.getProductById(this.id).subscribe({
        next: (response) => {
          this.product = response;
        },
      });
      this.productService.getSizeByProductId(this.id).subscribe({
        next: (response) => {
          this.sizes = response;
          this.sizes.sort((a, b) => a.sizeName.localeCompare(b.sizeName));
        },
      });
    }
  }
}
