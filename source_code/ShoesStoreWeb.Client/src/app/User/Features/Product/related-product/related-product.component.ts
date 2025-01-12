import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-related-product',
  imports: [CommonModule],
  templateUrl: './related-product.component.html',
  styleUrl: './related-product.component.css',
})
export class RelatedProductComponent {
  products = [
    {
      id: 1,
      name: 'Giày Adidas Adizero Adios Pro EVO 1',
      category: 'Giày nam',
      price: 1200000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 2,
      name: 'Giày Adidas AdifOM Superstar',
      category: 'Giày nam',
      price: 860000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 3,
      name: 'Giày Nike Air Jordan 1 Retro',
      category: 'Giày nam',
      price: 950000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 4,
      name: 'Giày Nike Air Jordan 1 Low',
      category: 'Giày nam',
      price: 990000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 5,
      name: 'Giày Puma RS-X3',
      category: 'Giày nam',
      price: 760000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 6,
      name: 'Giày Converse Chuck Taylor',
      category: 'Giày nam',
      price: 700000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 7,
      name: 'Giày Vans Old Skool',
      category: 'Giày nam',
      price: 650000,
      image: 'assets/Images/blog1.1.jpeg',
    },
    {
      id: 8,
      name: 'Giày New Balance 574',
      category: 'Giày nam',
      price: 890000,
      image: 'assets/Images/blog1.1.jpeg',
    },
  ];

  chunkedProducts: any[] = [];

  ngOnInit() {
    this.chunkedProducts = this.chunkArray(this.products, 4);
  }

  chunkArray(array: any[], chunkSize: number): any[] {
    const results = [];
    for (let i = 0; i < array.length; i += chunkSize) {
      results.push(array.slice(i, i + chunkSize));
    }
    return results;
  }
}
