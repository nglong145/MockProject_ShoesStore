import { Component } from '@angular/core';
import { SideBarComponent } from '../../Core/Component/side-bar/side-bar.component';
import { ProductListComponent } from '../../Features/Product/product-list/product-list.component';

@Component({
  selector: 'app-shop',
  imports: [SideBarComponent, ProductListComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
})
export class ShopComponent {}
