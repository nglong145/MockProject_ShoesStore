import { Routes } from '@angular/router';
import { ShoppingCartComponent } from './User/Features/cart/shopping-cart/shopping-cart.component';
import { PaymentComponent } from './User/Features/payment/payment/payment.component';
import { AdminLayoutComponent } from './Layout/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './Layout/user-layout/user-layout.component';
import { HomePageComponent } from './User/Pages/home-page/home-page.component';
import { ShopComponent } from './User/Pages/shop/shop.component';
import { ProductComponent } from './User/Pages/product/product.component';

import { AddproductComponent } from './admin/product/addproduct/addproduct.component';
import { UpdateProductComponent } from './admin/product/update-product/update-product.component';
import { AddBrandComponent } from './admin/brand/add-brand/add-brand.component';
import { UpdateBrandComponent } from './admin/brand/update-brand/update-brand.component';
import { AddBlogComponent } from './admin/blog/add-blog/add-blog.component';
import { UpdateBlogComponent } from './admin/blog/update-blog/update-blog.component';

import { PresonalInfoComponent } from './User/Features/presonal-info/presonal-info.component';
import { TrackingOrderComponent } from './User/Features/tracking-order/tracking-order.component';
import { LoginComponent } from './User/Features/auth/login/login.component';
import { RegisterComponent } from './User/Features/auth/register/register.component';


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
        path: 'register',
        component: RegisterComponent,
      },
      {
        path: 'login',
        component: LoginComponent,
      },
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

      {
        path: 'personal-info',
        component: PresonalInfoComponent,
      },
      {
        path: 'personal-info/order',
        component: TrackingOrderComponent,
      },
      // Thêm các route khác cho user
    ],
  },
  {
    path: 'admin/product/add',
    component: AddproductComponent
  },
  {
    path: 'admin/product/update',
    component: UpdateProductComponent
  },
  {
    path: 'admin/brand/add',
    component: AddBrandComponent
  },
  {
    path: 'admin/brand/update',
    component: UpdateBrandComponent
  },
  {
    path: 'admin/blog/add',
    component: AddBlogComponent
  },
  {
    path: 'admin/blog/update',
    component: UpdateBlogComponent
  }
];
