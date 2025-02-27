import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [
    MatButton
  ],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.scss'
})
export class TestErrorsComponent {

  baseUrl: string = 'https://localhost:5001/';
  http? : HttpClient;

 constructor( http : HttpClient){
this.http = http;

 }

 get404Error()
 {
  const url= this.baseUrl+'notfound';
  console.log('Inside method'+ url);

   this.http?.get(url).subscribe( x => console.log( x ));
 }

 get400Error()
 {
   this.http?.get(this.baseUrl+'bad-request').subscribe( x => console.log( x ));
 }

 get401Error()
 {
   this.http?.get(this.baseUrl+'un-authorized').subscribe( x => console.log( x ));
 }

 get500Error()
 {
   this.http?.get(this.baseUrl+'internal-server-error').subscribe( x => console.log( x ));
 }
}
