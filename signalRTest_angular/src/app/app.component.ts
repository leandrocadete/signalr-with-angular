import { Component } from '@angular/core';
import { AppSignalrService } from './app-signalr.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'signalR-test';
  receivedMessage: string = "";
  totalView: number = 0;
  formMessage: FormGroup = new FormGroup({
    msg: new FormControl('')
  });


  constructor(private signalRService: AppSignalrService){}


  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        //this.receivedMessage = this.title = message;
        this.formMessage.controls['msg'].setValue(message);
      });

      this.signalRService.updateTotalViews().subscribe((views) => this.totalView = views);
    });    
  }

  sendMessage(message: string): void {
    this.signalRService.sendMessage(message);
  }


  toggleLabel() {
    const msg = this.formMessage.controls['msg'].value;
    // if(this.title.charAt(0) == 's')
    //   this.title = this.title.toUpperCase();
    // else 
    //   this.title = this.title.toLowerCase();

    this.sendMessage(msg);
    this.signalRService.newWindowLoadedOnClient();
  }
}
