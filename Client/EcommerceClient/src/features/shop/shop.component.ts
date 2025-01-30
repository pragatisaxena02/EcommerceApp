import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../app/shared/models/product';
import { ProductItemComponent } from "../../app/features/shop/product-item/product-item.component";
import {MatDialog} from '@angular/material/dialog';
import { FiltersDialogComponent } from '../../app/features/shop/filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import {MatMenu, MatMenuTrigger} from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../app/shared/models/shopParams';
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import { Pagination } from '../../app/shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
Search: any = "Search";
  constructor(private shopService: ShopService, public dialogService: MatDialog) {}
  
  title = 'Ecommerce Platform';
  selectedOptions = [
    {name : 'Alphabetical', value: 'name'},
    {name : 'Price: High-low', value:'priceDesc'},
    {name : 'Price: Low-high', value:'priceAsc'}
  ]
  products?: Pagination<Product>;
  shopParams = new ShopParams();
  pageSizeOptions = [5,10,20,30];
  showFirstLastButtons = true;

  ngOnInit(): void {
    this.initializeShop();
  }

    initializeShop()
    {
      this.shopService.getBrands();
      this.shopService.getTypes();
      this.getProducts();
    }

    getProducts()
    {
      this.shopService.getProducts( this.shopParams).subscribe(response => {
        console.log(response);
           this.products = response;
           console.log( this.products);
        });
    }
    onSortChange(event: MatSelectionListChange) {
      this.shopParams.sort = event.source.selectedOptions.selected.map(option => option.value)[0];
      this.shopParams.pageNumber = 1;
       this.getProducts();   
}
   onSearchChanges()
   {
    console.log('Inside search'+this.shopParams.search);
    this.getProducts();
   }
    openFiltersDialog()
    {
      const dialogRef = this.dialogService.open( FiltersDialogComponent,
        {
          minWidth : '500px',
          data:{
           selectedBrands: this.shopParams.brands,
           selectedTypes: this.shopParams.types
          }
        } );

        dialogRef.afterClosed().subscribe( {
          next: result => {
            if( result )
            {
              console.log( result );
              this.shopParams.brands = result.selectedBrands,
              this.shopParams.types = result.selectedTypes,
              this.shopParams.pageNumber = 0;
              this.shopService.getProducts( this.shopParams).subscribe({
                next: result => this.products = result,
                error : err => console.log(err)
              })
            }
          }
        })
    }

    handlePageEvent($event: PageEvent) {
      this.shopParams.pageSize = $event.pageSize;
      this.shopParams.pageNumber = $event.pageIndex;
      this.getProducts();
      }
}
