import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRHubService {
  private hubConnection: HubConnection;
  private messageIdSubject: Subject<number> = new Subject<number>();

  serverUrl: string;
  constructor() {
    this.serverUrl = environment.apiUrlWithoutApi + "signalR_hub";
  }

  startConnection(groupName: string) {

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.serverUrl}`)
      .build();

    this.hubConnection.on('Receive', (arg:any) => {
      this.handleMessage(arg);
    });


    this.hubConnection.start().then(() => {
      this.hubConnection.invoke("Enter", groupName)
        .catch((err) => console.error('Error sending message:', err));
    })
  }

  isConnected():boolean{
    return this.hubConnection!=null;
  }
  stopConnection() {
    this.hubConnection.stop()
      .then(() => console.log('SignalR connection stopped.'))
      .catch((err) => console.error('Error stopping SignalR connection:', err));
  }

  private handleMessage(arg: any) {
    this.messageIdSubject.next(arg);
  }

  getNewMessage(): Observable<any> {
    return this.messageIdSubject.asObservable();
  }
}
