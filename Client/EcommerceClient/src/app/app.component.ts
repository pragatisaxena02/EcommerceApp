import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';
import { ShopService } from '../core/services/shop.service';
import { response } from 'express';
import { ShopComponent } from "../features/shop/shop.component";
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from "./core/spinner/spinner.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, HeaderComponent, SpinnerComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
 
  title = 'Ecommerce';
}
