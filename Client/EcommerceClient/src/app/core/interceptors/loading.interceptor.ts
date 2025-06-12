import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../services/busy.service';


export const loadingInterceptor: HttpInterceptorFn = (req, next) => {

  const busyService = inject( BusyService);
  
  busyService.show();

  return next(req).pipe(
    delay(500),
    finalize(() => busyService.hide() )
  )
};
