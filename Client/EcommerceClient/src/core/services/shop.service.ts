import { Injectable } from '@angular/core';
import { Product } from '../../app/shared/models/product';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../app/shared/models/pagination';
import { ShopParams } from '../../app/shared/models/shopParams';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

baseUrl= "https://localhost:5001/";
  products : Product[] = [];
  brands : string[] = [];
  types : string[] = [];
  constructor(private http: HttpClient) {}

  getProducts( shopPrams: ShopParams) 
  {
    let params= new HttpParams();

    if( shopPrams.brands!= undefined && shopPrams.brands.length > 0)
    {
      shopPrams.brands.forEach(brand => {
        params = params.append('Brands', brand);
    });
    }
    if( shopPrams.types && shopPrams.types.length > 0)
     {
      shopPrams.types.forEach(type => {
        params = params.append('Types', type);
    });
  }
    
    if( shopPrams.sort != null && shopPrams.sort.length > 0)
      {       
         params = params.append('sort', shopPrams.sort);
      }
      if( shopPrams.search != null && shopPrams.search!=undefined && shopPrams.search.length>0)
        {       
           params = params.append('search', shopPrams.search);
        }
     params = params.append('pageSize', shopPrams.pageSize);
     params = params.append('pageIndex', shopPrams.pageNumber);

      return this.http.get<Pagination<Product>>(this.baseUrl+'Product?'+params);  
  }

  getBrands()
  {
    if( this.brands.length > 0) return;
    return this.http.get<string[]>( this.baseUrl+ 'Product/brands').subscribe( {
      next: response => this.brands = response
    })
  }
  getProductById( id : number)
  {
    let params= new HttpParams();

    params = params.append('id', id);
    return this.http.get<Product>( this.baseUrl+'Product/Id?'+params);
  }

  getTypes()
  {
    if( this.types.length > 0) return;
    return this.http.get<string[]>( this.baseUrl+ 'Product/types' ).subscribe({
      next: response => this.types = response
    })
  }
}
