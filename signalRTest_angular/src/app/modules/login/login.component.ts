import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { CSnackBar } from './CSnackBar';
import { LoginService } from './service/login.service';
import { Route, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formLogin: FormGroup = new FormGroup(
    {
      ctrlUsername: new FormControl(null, [Validators.required]),
      ctrlPassword: new FormControl(null, [Validators.required])
    }
  );
  cfg: MatSnackBarConfig = {
    duration: 120000,
    horizontalPosition: 'end',
    verticalPosition: 'top',
    panelClass: 'custom-snackbar'
  };

  constructor(private snackBar: MatSnackBar,
    private loginService: LoginService,
    private route: Router
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    try {

      if (this.formLogin.valid) {
        //this.cfg.data = "Usuário ou senha inválido!" ;
        this.loginService
          .login(this.formLogin.value.ctrlUsername, this.formLogin.value.ctrlPassword)
          .then((r) => {
            //if(r)
            console.info(r);
            r.json().then(v => {
              console.info("Json %o", v);
              if (v.success) {
              const token = v.value;
              
              this.storeToken(token);
                this.snackBar
                .open("Login successfull", "OK", { duration: 3000, verticalPosition: "top", horizontalPosition: "right" })
                .afterDismissed()
                .subscribe(() => {
                  // TODO: redirect to home
                  this.route.navigate(['/home']);
                });
              }
            });
          }).catch((err) => {
            console.error(err);
          });
      } else {
        this.snackBar.open("Usuário ou senha invalido.", "Ok", this.cfg);
      }
    } catch (ex) {
      this.snackBar.open(`Erro. ${ex}`, "Ok");
    }
  }
  /**
   * Store jwt token
   *  
   * */ 
  storeToken(token: any) {
    localStorage.setItem('token', token);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.snackBar.open("Logout realizado com sucesso.", "Ok", this.cfg);
  }
}