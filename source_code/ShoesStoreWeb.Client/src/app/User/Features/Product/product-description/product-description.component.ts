import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReviewComponent } from '../../Review/review/review.component';

@Component({
  selector: 'app-product-description',
  imports: [CommonModule, ReviewComponent],
  templateUrl: './product-description.component.html',
  styleUrl: './product-description.component.css',
})
export class ProductDescriptionComponent {
  activeTab: string = 'description';
}
