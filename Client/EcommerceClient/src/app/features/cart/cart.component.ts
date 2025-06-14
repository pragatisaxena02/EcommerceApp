import { Component, input } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItem } from '../../shared/models/cart';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { OrderSummaryComponent } from "../order-summary/order-summary.component";
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterLink, CartItemComponent, OrderSummaryComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
constructor( public cartService: CartService ) { }
item = input.required<CartItem>
}
