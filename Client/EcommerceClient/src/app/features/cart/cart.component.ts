import { Component, input } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItem } from '../../shared/models/cart';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { OrderSummaryComponent } from "../order-summary/order-summary.component";
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterLink, CartItemComponent, OrderSummaryComponent, MatIcon],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
constructor( public cartService: CartService ) { }
item = input.required<CartItem>;

incrementQuantity( cartItem: CartItem) {
  this.cartService.addItemToCart( cartItem);
}

decrementQuantity( productId: number) {
  this.cartService.removeItemsFromcart(  productId, 1 );
}

removeItemFromCart(  productId: number, quantity: number) {
  this.cartService.removeItemsFromcart(  productId, quantity );
}
}
