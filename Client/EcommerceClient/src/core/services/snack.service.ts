import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackService {

  constructor(  private snackBar : MatSnackBar){}

  public openSnackBar( errMessage: string)
  {
    this.snackBar.open(errMessage, 'close',
      {
        panelClass: ['snack-error']
      }
    );
  }
}
