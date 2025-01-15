import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  imports: [CommonModule],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css',
})
export class SideBarComponent {
  @Output() filterChange = new EventEmitter<any>();

  brands = [
    { id: '1', name: 'Nike' },
    { id: '2', name: 'Adidas' },
  ];
  sizes = ['36', '37', '38', '39', '40', '41'];
  priceRanges = [
    { min: 0, max: 500000, label: '< 500.000 VND' },
    { min: 500000, max: 1000000, label: '500.000 - 1.000.000 VND' },
    { min: 1000000, max: 2000000, label: '1.000.000 - 2.000.000 VND' },
    { min: 2000000, max: null, label: '> 2.000.000 VND' },
  ];
  selectedBrandId?: string;
  selectedSize?: string;
  selectedPriceRange?: { min: number; max: number };

  onBrandChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    if (target) {
      this.selectedBrandId = target.value || undefined;
      this.emitFilterChange();
    }
  }

  onSizeChange(size: string): void {
    this.selectedSize = size === this.selectedSize ? undefined : size;
    this.emitFilterChange();
  }

  onPriceRangeChange(range: any): void {
    if (
      this.selectedPriceRange &&
      this.selectedPriceRange.min === range.min &&
      this.selectedPriceRange.max === range.max
    ) {
      this.selectedPriceRange = undefined;
    } else {
      this.selectedPriceRange = range;
    }
    this.emitFilterChange();
  }

  emitFilterChange(): void {
    this.filterChange.emit({
      brandId: this.selectedBrandId,
      size: this.selectedSize,
      minPrice: this.selectedPriceRange?.min,
      maxPrice: this.selectedPriceRange?.max,
    });
  }
}
