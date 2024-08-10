import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarRef } from '@angular/material/snack-bar';



@Component({
  selector: 'snack-bar-annotated-component-example-snack',
  template: ` 
<div style="display: flex"> 
  <span style="margin: auto auto auto 0" class="example-pizza-party" matSnackBarLabel>
    Content: {{ text }} 
  </span>
  <span style="margin: auto 5px auto 0" matSnackBarActions>
    <button mat-icon-button color="secondary"  matSnackBarAction (click)="snackBarRef.dismissWithAction()">
      <mat-icon>close</mat-icon>
    </button>
  </span>
</div>
  `,

  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [MatIconModule, MatButtonModule]
})
export class CSnackBar {
  
  text: any;
  snackBarRef = inject(MatSnackBarRef);
}
