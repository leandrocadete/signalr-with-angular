import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AppSignalrService } from 'src/app/app-signalr.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title = 'signalR-test';
  receivedMessage: string = "";
  totalView: number = 0;
  formMessage: FormGroup = new FormGroup({
    ctrlMsg: new FormControl('')
  });
  
  constructor(private signalRService: AppSignalrService){}


  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        //this.receivedMessage = this.title = message;
        this.formMessage.controls['ctrlMsg'].setValue(message);
      });

      this.signalRService.updateTotalViews().subscribe((views) => this.totalView = views);
    });    
  }

  sendMessage(message: string): void {
    this.signalRService.sendMessage(message);
  }


  toggleLabel() {
    const msg = this.formMessage.controls['ctrlMsg'].value;
    // if(this.title.charAt(0) == 's')
    //   this.title = this.title.toUpperCase();
    // else 
    //   this.title = this.title.toLowerCase();

    this.sendMessage(msg);
    this.signalRService.newWindowLoadedOnClient();
  }
}
