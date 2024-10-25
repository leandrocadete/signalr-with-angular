import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AcoesService {
  api_url: string;

  constructor() {
    this.api_url = `${environment.host_webapi}:${environment.apiport}/api`;
   }

   public getAcoes() {
    let token = localStorage.getItem('token')?.toString();
    return fetch(`${this.api_url}/acoes/ListAcoes01`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `${token}` 
       }
    });
   }
}
