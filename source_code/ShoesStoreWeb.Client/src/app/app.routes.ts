import { Routes } from '@angular/router';
import { ShoppingCartComponent } from './User/Features/cart/shopping-cart/shopping-cart.component';
import { PaymentComponent } from './User/Features/payment/payment/payment.component';

export const routes: Routes = [
    // Shopping cart
    {
      path: 'cart',
      component: ShoppingCartComponent,
    },
    // payment
    {
      path: 'payment',
      component: PaymentComponent,
    },
];
