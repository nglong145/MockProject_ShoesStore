import { Routes } from '@angular/router';
import { ShoppingCartComponent } from './User/Features/cart/shopping-cart/shopping-cart.component';
import { PaymentComponent } from './User/Features/payment/payment/payment.component';
import { AdminLayoutComponent } from './Layout/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './Layout/user-layout/user-layout.component';
import { HomePageComponent } from './User/Pages/home-page/home-page.component';
import { ShopComponent } from './User/Pages/shop/shop.component';
import { ProductComponent } from './User/Pages/product/product.component';

export const routes: Routes = [
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [],
  },
  {
    path: '',
    component: UserLayoutComponent,
    children: [
      {
        path: '',
        component: HomePageComponent,
      },

      {
        path: 'shop',
        component: ShopComponent,
      },

      {
        path: 'product',
        component: ProductComponent,
      },

      {
        path: 'cart',
        component: ShoppingCartComponent,
      },

      {
        path: 'checkout',
        component: PaymentComponent,
      },
      // Thêm các route khác cho user
    ],
  },
];
