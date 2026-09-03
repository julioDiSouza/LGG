import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';

declare const confetti: ((options: {
  particleCount: number;
  spread: number;
  origin: { y: number };
}) => void) | undefined;

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
    this.iniciarConfirmacaoVisual();

    this.activatedRoute.queryParams.subscribe((params) => {
      this.key = params['key'];
      this.verificarCliente();
    });
  }

  private iniciarConfirmacaoVisual(): void {
    if (typeof window === 'undefined') {
      return;
    }

    window.setTimeout(() => {
      document.getElementById('validando')?.setAttribute('style', 'display: none');
      document.getElementById('confirmado')?.setAttribute('style', 'display: block');
      document.getElementById('orientacao')?.setAttribute('style', 'display: block');

      confetti?.({ particleCount: 150, spread: 100, origin: { y: 0.5 } });
      window.setTimeout(() => {
        confetti?.({ particleCount: 100, spread: 120, origin: { y: 0.6 } });
      }, 300);
    }, 3000);
  }

  verificarCliente() {
    console.log('[Confirmacao] chave recebida:', this.key);

    if (!this.key) {
      if (typeof window !== 'undefined' && window.alert) {
        window.alert('Chave de confirmação ausente na URL.');
      }
      return;
    }

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
