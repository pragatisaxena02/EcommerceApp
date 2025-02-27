import { Routes } from '@angular/router';
import { HomeComponent } from '../features/home/home.component';
import { ShopComponent } from '../features/shop/shop.component';
import { ProductDetailsComponent } from '../features/shop/product-details/product-details.component';
import { TestErrorsComponent } from '../features/test-errors/test-errors.component';
import { NotFoundComponent } from '../shared/component/not-found/not-found.component';
import { ServerErrorComponent } from '../shared/component/server-error/server-error.component';

export const routes: Routes = [
{path:'', component: HomeComponent},
{path:'shop', component: ShopComponent},
{path:'shop/:id', component: ProductDetailsComponent},
{path:'test-error', component: TestErrorsComponent},
{path:'not-found', component: NotFoundComponent},
{path:'server-error', component: ServerErrorComponent},
{path:'**', redirectTo: 'not-found', pathMatch:'full'}
]

