import { Component, Inject } from '@angular/core';
import { ShopService } from '../../../../core/services/shop.service';
import {MatDivider} from '@angular/material/divider';
import {MatListOption, MatSelectionList} from '@angular/material/list';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-filters-dialog',
  standalone: true,
  imports: [
     MatDivider,
     MatSelectionList,
     MatListOption,
    FormsModule
    ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {

  selectedBrands : string[] = [];
  selectedTypes : string[] = [];

  constructor(public shopService: ShopService, private dialogRef: MatDialogRef<FiltersDialogComponent>,  @Inject(MAT_DIALOG_DATA) public data : any) {
  
  this.selectedBrands = this.data.selectedBrands;
  this.selectedTypes = this.data.selectedTypes;  
  }
  applyFilters()
  {
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes 
    })
  }
}
