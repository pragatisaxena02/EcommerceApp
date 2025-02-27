import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../app/shared/models/product';
import { MatIcon } from '@angular/material/icon';

import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CurrencyPipe, MatIcon, MatButton, MatFormField, MatLabel, MatDivider],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{
  product : Product | undefined;

  constructor( public shopService: ShopService, public activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if( !id )
      return;

   this.shopService.getProductById(+id).subscribe(response => {
    console.log(response);
       this.product = response;
       console.log( this.product);
    });

   console.log( this.product);
  }
  
  public loadProduct( )
  {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if( !id )
      return;

   //this.shopService.getProductById(+id).subscribe({
   // next : response => this.product = response,
   // error : err => console.log( err )
  // });

  this.shopService.getProductById(+id).subscribe(response =>{
    this.product = response
    console.log( this.product )
   });

   console.log( this.product);
  }
}
