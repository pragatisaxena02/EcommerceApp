import { Component } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [MatCard],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.scss'
})
export class ServerErrorComponent {
public error: any;

constructor( public router : Router ){
const navigate = router.getCurrentNavigation();
 this.error = navigate?.extras.state?.['dataId'];
 console.log( 'inside component :' + this.error);
}

}
