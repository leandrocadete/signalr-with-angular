import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppSignalrService {

  

  hubConnection: signalR.HubConnection;

  constructor() {
    
    const api_url = `${environment.host_webapi}:${environment.apiport}`;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${api_url}/hubs/realtimehub`, signalR.HttpTransportType.WebSockets) // SignalR hub URL
      .build();
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveMessage(): Observable<string> {
    return new Observable<string>((observer) => {
      
      this.hubConnection.on('ReceiveMessage', (message: string) => {
        observer.next(message);
      });
    });
  }

  sendMessage(message: string): void {
    this.hubConnection.invoke('SendMessage', message);
  }


  updateTotalViews(): Observable<number> {

    return new Observable<number>(observe => {

      this.hubConnection.on('updateTotalViews', (totalviews: number) => {
        observe.next(totalviews);
      });
    })

  }

  newWindowLoadedOnClient(): void {
    this.hubConnection.invoke("NewWindowLoader");
  }

}
