import { CommonModule } from '@angular/common';
import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../models/product.model';
import { Size } from '../models/size.model';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css',
})
export class ProductDetailComponent implements OnInit, OnChanges {
  @Input() product?: Product;
  @Input() sizes: Size[] = [];

  selectedSize: string | null = null;
  sizeQuantity: number = 0;
  quantity: number = 1;
  isOutOfStock: boolean = false;

  ngOnInit(): void {
    this.updateTotalQuantity();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['sizes'] && changes['sizes'].currentValue) {
      this.updateTotalQuantity();
    }
  }

  updateTotalQuantity(): void {
    this.sizeQuantity = this.sizes.reduce(
      (total, size) => total + size.quantity,
      0
    );
  }

  validateQuantity(): void {
    if (this.quantity > this.sizeQuantity) {
      this.quantity = this.sizeQuantity;
    }
    if (this.quantity < 1) {
      this.quantity = 1;
    }
  }

  restrictInput(event: KeyboardEvent): void {
    const charCode = event.key.charCodeAt(0);
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
    }
  }

  selectSize(sizeName: string) {
    if (this.selectedSize === sizeName) {
      this.selectedSize = null;
      this.updateTotalQuantity();
      this.isOutOfStock = false;
    } else {
      this.selectedSize = sizeName;
      const size = this.sizes.find((s) => s.sizeName === sizeName);
      this.sizeQuantity = size ? size.quantity : 0;
      this.isOutOfStock = this.sizeQuantity === 0;
    }
  }

  increaseQuantity() {
    if (this.selectedSize && this.quantity < this.sizeQuantity) {
      this.quantity++;
    }
  }

  decreaseQuantity() {
    if (this.selectedSize && this.quantity > 1) {
      this.quantity--;
    }
  }

  AddtoCart(): void {
    if (this.selectedSize) {
      console.log('Added to cart:', {
        product: this.product,
        size: this.selectedSize,
        quantity: this.quantity,
      });
    }
  }
}
