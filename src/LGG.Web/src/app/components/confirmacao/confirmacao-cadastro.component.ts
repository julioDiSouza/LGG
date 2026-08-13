import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-confirmacao-cadastro',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirmacao-cadastro.component.html',
  styleUrls: ['./confirmacao-cadastro.component.css']
})
export class ConfirmacaoCadastroComponent implements OnInit {
  key = '';

  constructor(
    private apiService: ApiService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.key = params['key'];
      this.verificarCliente();
    });
  }

  verificarCliente() {
    console.log('[Confirmacao] chave recebida:', this.key);

    if (!this.key) {
      console.warn('[Confirmacao] query param "key" ausente — abortando confirmação.');
      if (typeof window !== 'undefined' && window.alert) {
        window.alert('Chave de confirmação ausente na URL.');
      }
      return;
    }

    const reqUrl = `${this.apiService['baseUrl']}/Email/cadastro_confirmacao?key=${encodeURIComponent(this.key)}`;
    console.log('[Confirmacao] fazendo requisição para', reqUrl);

    this.apiService.confirmarEmail(this.key).subscribe({
      next: (res) => {
        console.log('[Confirmacao] resposta sucesso:', res);
        if (typeof window !== 'undefined' && window.alert) {
          window.alert('Cadastro confirmado com sucesso! Você será redirecionado para a página inicial.');
        }
      },
      error: (err) => {
        console.error('[Confirmacao] resposta erro:', err);
        if (typeof window !== 'undefined' && window.alert) {
          window.alert('Ocorreu um erro ao confirmar o cadastro. Por favor, tente novamente.');
        }
      }
    });
  }
}
