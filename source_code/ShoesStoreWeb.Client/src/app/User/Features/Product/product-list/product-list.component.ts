import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products = [
    {
      name: 'Giày Adidas Stan Smith Fairway M20324',
      price: 560000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Nike Air Jordan 1',
      price: 700000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Adidas AdiFOM',
      price: 560000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Nike Air Force 1',
      price: 700000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Adidas NMD_CS1',
      price: 888000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Nike Air Jordan 1 Low',
      price: 999000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Adidas NMD_R1',
      price: 1099000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
    {
      name: 'Giày Adidas Adizero',
      price: 1200000,
      imageUrl: 'assets/Images/blog1.1.jpeg',
    },
  ];

  currentPage = 1;
  itemsPerPage = 4;
  pages: number[] = [];

  constructor() {}

  ngOnInit(): void {
    this.calculatePages();
  }

  calculatePages(): void {
    const totalPages = Math.ceil(this.products.length / this.itemsPerPage);
    this.pages = Array(totalPages)
      .fill(0)
      .map((_, i) => i + 1);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
  }

  addToCart(product: any): void {
    console.log('Thêm sản phẩm vào giỏ:', product);
  }

  get displayedProducts() {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.products.slice(start, end);
  }
}
