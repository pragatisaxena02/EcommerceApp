import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatBadgeModule} from '@angular/material/badge';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatMenu } from '@angular/material/menu';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIconModule, MatButtonModule, MatBadgeModule, RouterLink, RouterLinkActive, MatMenu, MatProgressSpinnerModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

public indeterminate: string =  "indeterminate";

  public constructor( public busyService: BusyService){
  

  }
  // public CheckBusy() : boolean
  // {
  //   console.log('busy is : '+this.busyService?.loading);
  //   return this.busyService.loading ?? false ;
  // }

}
