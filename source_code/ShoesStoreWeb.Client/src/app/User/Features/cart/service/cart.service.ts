import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../models/Cart.model';
import { BASE_URL } from '../../../../app.config';
import { updateCartItem } from '../models/updateCartItem.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private http: HttpClient) { }

  getCartOfUser() : Observable<Cart> {
    return this.http.get<Cart>(`${BASE_URL}/Cart/get-cart-of-user`);
  }

  updateCartItem(cartId: string, productId: string, size: string, model: updateCartItem): Observable<void> {
    return this.http.put<void>(`${BASE_URL}/CartItem/update-cart-item/${cartId}/${productId}/${size}`, model);
  }

  deleteCartItem(cartId: string, productId: string, size: string): Observable<void> {
    return this.http.delete<void>(`${BASE_URL}/CartItem/remove-cart-item/${cartId}/${productId}/${size}`);
  }
}
