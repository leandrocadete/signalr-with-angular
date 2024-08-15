import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppSignalrService {



  private hubConnection: signalR.HubConnection;
  private userHubConnection: signalR.HubConnection;

  constructor() {

    const api_url = `${environment.host_webapi}:${environment.apiport}`;

    this.userHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${api_url}/hubs/userhub`, { withCredentials: false })
      .build();
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${api_url}/hubs/realtimehub`, { withCredentials: false }) // SignalR hub URL
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
    });
  }

  newWindowLoadedOnClient(): void {
    this.hubConnection.invoke("NewWindowLoader");
  }

  // Subscription to group ()
  subscribeToGroup = (email: any) => {
    return new Promise((resolve, reject) => {
      if (this.userHubConnection.state !== signalR.HubConnectionState.Connected) {
        this.userHubConnection.start().then((v) => {
          console.info("Subscribe - connected o userHub!");
          this.userHubConnection.invoke("Subscribe", email)
            .then(r => resolve(r))
            .catch(r => reject(false));

        }).catch(r => reject(false));
      } else {
        this.userHubConnection.invoke("Subscribe", email)
          .then(r => resolve(r))
          .catch(r => reject(false));
      }
    });
  }
  unsubscribeFromGroup = (email: any) => {
    return new Promise((resolve, reject) => {
      if (this.userHubConnection.state !== signalR.HubConnectionState.Connected) {
        this.userHubConnection.start().then((v) => {
          console.info("Unsubscribe - connected o userHub!");
          this.userHubConnection.invoke("Unsubscribe", email)
            .then(r => resolve(true))
            .catch(r => reject(false));

        }).catch(r => reject(false));
      } else {
        this.userHubConnection.invoke("Unsubscribe", email)
          .then(r => resolve(true))
          .catch(r => reject(false));
      }
    });
  }

  fromGroupAdm(): Observable<string> {
    return new Observable<string>((observe) => {
      this.userHubConnection.on("fromGroupAdm", (message: string) => {
        observe.next(message);        
      });
    });
  }


}
