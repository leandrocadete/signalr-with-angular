import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AcoesComponent } from './acoes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { AcoesRoutingModule } from './acoes-routing.module';



@NgModule({
  declarations: [
    AcoesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,    
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatIconModule,
    HttpClientModule,
    AcoesRoutingModule,
  ]
})
export class AcoesModule { }
