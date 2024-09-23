import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ApiService } from 'src/app/services/api/api.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { MatDialog } from '@angular/material/dialog';
import { ModalConfirmacaoComponent } from '../modal-confirmacao/modal-confirmacao.component';

export interface UserData {
  UrlCurta: string;
  UrlLonga: string;
}

@Component({
  selector: 'app-my',
  templateUrl: './my.component.html',
  styleUrls: ['./my.component.css']
})
export class MyComponent implements OnInit {
  displayedColumns: string[] = [ 'UrlCurta', 'UrlLonga', 'excluir' ];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  paginaAtual: number = 1;
  quantidade: number = 5;
  total: number = 0;

  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private toast: NgToastService,
    public dialog: MatDialog
  ) { }

  ngOnInit() { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.obterLinks(this.paginaAtual, this.quantidade);
  }

  obterLinks(pagina:number, quantidade:number) {
    this.apiService.obterLinksCurtos(pagina, quantidade)
    .subscribe({
      next:(res)=>{
        this.dataSource = res.colecao;
        this.total = res.total;
      },
      error:(err)=>{
        this.toast.error({
          detail: 'Algo deu errado',
          summary: 'Por favor tente novamente mais tarde.',
          duration: 5000
        });
      }
    });
  }  

  mudarPagina(event: PageEvent) {
    this.paginaAtual = event.pageIndex+1;
    this.quantidade = event.pageSize;
    this.total = event.length;

    this.obterLinks(this.paginaAtual, this.quantidade);
  }

  apagarConfirmacao(id: any) {
    const dialogRef = this.dialog.open(ModalConfirmacaoComponent, {

      width: '300px',
      data: { mensagem: `Quer mesmo excluir este link?` },
      position: { top: '10%'} 
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result === true) {
        this.apagar(id);
      }
    });
  }

  apagar(id: any) {
    this.apiService.apagarLink(id)
    .subscribe({
      next:(res)=>{
        this.obterLinks(this.paginaAtual, this.quantidade);
      },
      error:(err)=>{
        this.toast.error({
          detail: 'Algo deu errado',
          summary: 'Por favor tente novamente mais tarde.',
          duration: 5000
        });
      }
    });
  }
}
