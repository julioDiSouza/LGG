import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
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
  validando = true;
  confirmado = false;

  constructor(
    private apiService: ApiService,
    private activatedRoute: ActivatedRoute,
    @Inject(PLATFORM_ID) private platformId: object
  ) {}

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    this.key = this.activatedRoute.snapshot.queryParamMap.get('key') ?? '';
    this.verificarCliente();
  }

  private exibirConfirmacao(): void {
    this.validando = false;
    this.confirmado = true;

    if (typeof window === 'undefined') {
      return;
    }

    const validandoElement = document.getElementById('validando');
    const confirmadoElement = document.getElementById('confirmado');

    if (validandoElement) {
      validandoElement.style.display = 'none';
    }

    if (confirmadoElement) {
      confirmadoElement.style.display = 'block';
    }

    confetti?.({ particleCount: 150, spread: 100, origin: { y: 0.5 } });
    window.setTimeout(() => {
      confetti?.({ particleCount: 100, spread: 120, origin: { y: 0.6 } });
    }, 300);
  }

  private exibirFalha(): void {
    this.validando = false;
    this.confirmado = false;

    if (typeof window === 'undefined') {
      return;
    }

    const validandoElement = document.getElementById('validando');
    const confirmadoElement = document.getElementById('confirmado');
    const falhouElement = document.getElementById('falhou');

    if (validandoElement) {
      validandoElement.style.display = 'none';
    }

    if (confirmadoElement) {
      confirmadoElement.style.display = 'none';
    }

    if (falhouElement) {
      falhouElement.style.display = 'block';
    }
  }

  verificarCliente() {
    console.log('[Confirmacao] chave recebida:', this.key);

    if (!this.key) {
      this.exibirFalha();

      if (typeof window !== 'undefined' && window.alert) {
        window.alert('Chave de confirmação ausente na URL.');
      }
      return;
    }

    this.apiService.confirmarEmail(this.key).subscribe({
      next: (res) => {
        setTimeout(() => {
          this.exibirConfirmacao();
        }, 3000);
      },
      error: (err) => {
        this.exibirFalha();
      }
    });
  }
}
