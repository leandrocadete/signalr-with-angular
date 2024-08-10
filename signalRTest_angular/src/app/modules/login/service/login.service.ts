import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class LoginService {
  api_url: string;

  constructor(
    
  ) { 
    this.api_url = `${environment.host_webapi}:${environment.apiport}/api`;
    
  }

  /**
   * Login method using fetch
   *  */ 
  public login (username: string, password: string) {


    const u = {
      name: "",
      email: username,
      pwd: password,
      group: "",
      id: 0
    }
    return fetch(`${this.api_url}/Login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'// cors
        
      },
      body: JSON.stringify(u)
    });

    // return fetch(`${this.api_url}/Login`, {
    //   method: 'GET',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   }
    
    // },);
  }

  // public login(userName: string, password: string) {
  //    const u = { email: userName, pwd: password}
  //    return this.http.post(`${this.api_url}/login`, u);
  // }
}
