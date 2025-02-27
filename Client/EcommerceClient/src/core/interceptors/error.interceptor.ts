import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackService } from '../services/snack.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
 
  const router = inject(Router);
  const snackBar = inject( SnackService)
  return next(req).pipe(
        catchError((err : HttpErrorResponse) => {
          console.log( 'Inside interceptor' + err);
                   if( err.status == 400 )
                   {
                     snackBar.openSnackBar( err.error.title || err.error)
                   }
                   if( err.status == 404)
                   {
                    router.navigateByUrl('not-found')
                   }
                   if( err.status == 500)
                   {
                    //console.log('inside interceptor:)
                    const navigationExtras: NavigationExtras = {
                      state: { dataId: err.error},
                    };
                    router.navigate(['server-error'], navigationExtras);
                    const extras: NavigationExtras = {state:{ dataId :err} };
                                 }
                   if( err.status ==  401)
                  {
                    snackBar.openSnackBar( err.error.title || err.error)
                  }

        return throwError(() => err)
        }))
      }
