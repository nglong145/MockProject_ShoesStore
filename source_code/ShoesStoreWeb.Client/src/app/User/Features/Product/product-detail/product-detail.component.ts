import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css',
})
export class ProductDetailComponent {
  product: any;
  images: string[] = [];
  selectedImage: string = '';
  sizes: number[] = [];
  selectedSize: number | null = null;
  quantity: number = 1;

  fakeData = {
    id: 1,
    name: 'Giày Adidas Stan Smith Fairway M20324',
    category: 'Giày nam',
    price: 560000,
    images: ['assets/Images/blog1.1.jpeg'],
    sizes: [35, 36, 37, 38, 40, 41, 42, 43],
  };

  ngOnInit() {
    this.product = this.fakeData;
    this.images = this.product.images;
    this.selectedImage = this.images[0];
    this.sizes = this.product.sizes;
  }

  selectImage(image: string) {
    this.selectedImage = image;
  }

  selectSize(size: number) {
    this.selectedSize = size;
  }

  increaseQuantity() {
    this.quantity++;
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
