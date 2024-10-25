import { Component, OnInit } from '@angular/core';
import { AcoesService } from './service/acoes.service';
import { MatTableDataSource } from '@angular/material/table';

interface acao {
  name: string;
  value: string;
}

@Component({
  selector: 'app-acoes',
  templateUrl: './acoes.component.html',
  styleUrls: ['./acoes.component.scss']
})


export class AcoesComponent implements OnInit {
  dataSource = MatTableDataSource<acao>;
  displayedColumns = ["name"];
  arr: acao[] = [];
  constructor(private acoesService: AcoesService) { }

  ngOnInit(): void {

    this.acoesService.getAcoes().then((resp) => {
      console.log("Acoes %o", resp)
      if (resp.ok) {
        resp.json().then(v => {
          for (let i = 0, len = v.length; i < len; i++) {
            const a = { name: v[i], value: v[i]};
            this.arr.push(a);
          }  
          let vetArr = new MatTableDataSource(this.arr);

        });
      }
    });
  }

}
