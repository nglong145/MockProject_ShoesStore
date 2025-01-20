import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Features/auth/services/auth.service';
import { User } from '../../../Features/auth/models/user.model';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { IMG_URL } from '../../../../app.config';
import { CartService } from '../../../Features/cart/service/cart.service';
import { CartItem } from '../../../Features/cart/models/CartItem.model';
import { Observable, Subscription } from 'rxjs';
import { Cart } from '../../../Features/cart/models/Cart.model';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  urlImage: string = `${IMG_URL}`;
  user: User | null = null;
  userId: string | null = null;

  cart$?: Observable<Cart>;
  cartSubsription?: Subscription;
  cartCount: number = 0;
  uniqueProductCount: number = 0;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe((user) => {
      this.user = user;
    });

    if (!this.user) {
      this.authService.getUserInfo().subscribe({
        next: (data) => {
          this.authService.setUser(data);
          this.userId = data.id;
        },
      });
    }

    // Tải dữ liệu giỏ hàng ban đầu
    this.cartService.loadCart();
    this.cartService.getCartOfUser().subscribe((cart: Cart) => {
      this.cartCount = cart.items.length; // Đếm số lượng sản phẩm dựa trên items
    });
  }

  logout(): void {
    this.authService.logout();
    // this.cartService.clearCart();
    this.router.navigate(['/login']);
  }
}
