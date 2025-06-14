import { APP_INITIALIZER, ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from '../core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { InitService } from './core/services/init.service';
import { lastValueFrom } from 'rxjs';

function initializeApp( initializeService: InitService ) {
  return () => lastValueFrom(initializeService.init()).finally(() =>{
    if (typeof document !== 'undefined')
    {
    const splash =  document.getElementById('init-splash');
    if( splash) {
      splash.remove();
    }
  }
  })
}
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
              provideClientHydration(), 
              provideAnimationsAsync(),
              provideHttpClient(withFetch(),withInterceptors([errorInterceptor, loadingInterceptor])),
            {
              provide: APP_INITIALIZER,
              useFactory: initializeApp,
              multi: true,
              deps: [InitService]
            }]
};
