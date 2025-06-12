import { Injectable } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  private _loading = new BehaviorSubject<boolean>(false);
  loading$ = this._loading.asObservable();

  private requestCount = 0;

  show() {
    this.requestCount++;
    if (this.requestCount === 1) {
      this._loading.next(true);
    }
  }

  hide() {
    if (this.requestCount > 0) this.requestCount--;
    if (this.requestCount === 0) {
      this._loading.next(false);
    }
  }
}
