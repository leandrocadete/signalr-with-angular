import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginService: any;
  formLogin: FormGroup = new FormGroup(
   {
    ctrlUsername: new FormControl(null, [Validators.required]),
    ctrlPassword: new FormControl(null, [Validators.required])
   } 
  );

  constructor(private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  login(): void {
    //this.loginService.login(this.username, this.password);
    try {
      
      console.log("TODO: Login %o", this.formLogin.value);
      this.loginService.login();
    } catch (ex) {
      this.snackBar.openFromComponent(PizzaPartyAnnotatedComponent, 
      { duration: 30000, horizontalPosition: 'end', verticalPosition: 'top', panelClass: 'green' });

    }
  }

  logout(): void {
    console.log("TODO: Logout");
  }
}


@Component({
  selector: 'snack-bar-annotated-component-example-snack',
  template: ` 
<div style="display: flex"> 
  <span style="margin: auto auto auto 0" class="example-pizza-party" matSnackBarLabel>
    Custom Message
  </span>
  <span style="margin: auto 5px auto 0" matSnackBarActions>
    <button mat-icon-button  matSnackBarAction (click)="snackBarRef.dismissWithAction()">
      <mat-icon>check</mat-icon>
    </button>
  </span>
</div>
  `,
  
  styleUrls: ['./login.component.scss'],
  standalone: true,  
  imports: [MatIconModule]
})
export class PizzaPartyAnnotatedComponent {
  snackBarRef = inject(MatSnackBarRef);
}