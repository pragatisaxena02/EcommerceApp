import { Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {

  constructor( private cartService: CartService) { }

  init() {

    if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
    const cartId = localStorage.getItem('cartId');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    return cart$;
  }
  return of(null);

  }
}
