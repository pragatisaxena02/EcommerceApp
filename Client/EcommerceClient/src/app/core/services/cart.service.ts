import { computed, Injectable, signal, Signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { map, take, takeUntil, tap } from 'rxjs';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  baseUrl = 'http://localhost:5001/';

  itemCount = computed(() => {
  return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0)
  });

  cart = signal<Cart|null>(null);
  constructor( private http : HttpClient) { }
 
  totals = computed(() =>{
    const cart = this.cart();
    if (!cart || !cart.items) return null;
    const subtotal = cart.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    const shipping = 0;
    const discount = 0;

    return{
      subtotal: subtotal,
      shipping: shipping,
      discount: discount,
      total: subtotal + shipping - discount
    }
  })


  getCart( id: string)
  {
     return this.http.get<Cart>( this.baseUrl+'cart?id='+id).
     pipe(
      map( cart => {
        this.cart.set(cart);
        return cart;
      })
     );
  }

  setCart( cart: Cart)
  {
    return this.http.post<Cart>( this.baseUrl+'cart', cart).
    pipe(
      take(1),
      tap( cart => this.cart.set(cart) )
    ).subscribe();
  }

  addItemToCart( item: CartItem | Product, quantity = 1) {

    console.log(`Adding item to cart: ${JSON.stringify(item)} with quantity: ${quantity}`);
    const cart = this.cart() ?? this.createCart();

    if( this.isProduct(item)){
      item = this.mapProductToCartItem(item);
    }

    cart.items = this.addOrUpdateCartItem(cart.items, item, quantity);
    this.setCart(cart);
  }

  private addOrUpdateCartItem(items: CartItem[], item: CartItem, quantity: number) : CartItem[]
  {
    const index = items.findIndex(i => i.productId === item.productId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      
      items[index].quantity += quantity;
    }

    return items;
  }
  
    private mapProductToCartItem(product: Product): CartItem {
    return {
      productId: product.id,
      productName: product.name,
      price: product.price,
      pictureUrl: product.imageUrl,
      quantity: 1,
      brand: product.brand,
      type: product.type
    }
  }

  private isProduct(item: CartItem | Product): item is Product {
    return (item as Product).id !== undefined;
  }

  private createCart() : Cart{
    const cart = new Cart();
//     if (typeof window !== 'undefined' && localStorage) {
//     localStorage.setItem('cart_id', cart.id);
// }
    return cart;
  }
}