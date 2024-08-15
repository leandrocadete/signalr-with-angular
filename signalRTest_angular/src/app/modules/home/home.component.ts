import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
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

  constructor(private signalRService: AppSignalrService, private snackbar: MatSnackBar) { }


  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        //this.receivedMessage = this.title = message;
        this.formMessage.controls['ctrlMsg'].setValue(message);
      });

      this.signalRService.updateTotalViews().subscribe((views) => this.totalView = views);

      const email = localStorage.getItem("email");

      this.signalRService.subscribeToGroup(email).then(s => {
        console.info("Ok subscription... ");
        this.signalRService.fromGroupAdm()
          .subscribe({
            next: message =>
              this.snackbar.open(message, "Ok", { duration: 30000, horizontalPosition: "right", verticalPosition: "top" }),
            error: err => console.error(err)
          });
      });

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
