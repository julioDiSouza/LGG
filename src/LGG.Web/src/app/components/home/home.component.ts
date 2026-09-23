import { Component } from '@angular/core';

interface RankedVideo {
  rank: number;
  title: string;
  channel: string;
  likes: string;
  views: string;
  thumbnail: string;
  url: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  readonly rankedVideos: RankedVideo[] = [
    {
      rank: 1,
      title: 'O futuro da tecnologia já começou',
      channel: '@canaltech',
      likes: '18,4 mil',
      views: '245 mil',
      thumbnail: 'https://i.ytimg.com/vi/aircAruvnKk/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=aircAruvnKk'
    },
    {
      rank: 2,
      title: 'Como criar hábitos que realmente funcionam',
      channel: '@vidaorganizada',
      likes: '15,2 mil',
      views: '198 mil',
      thumbnail: 'https://i.ytimg.com/vi/75d_29QWELk/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=75d_29QWELk'
    },
    {
      rank: 3,
      title: 'A história por trás das grandes ideias',
      channel: '@documentarios',
      likes: '12,8 mil',
      views: '176 mil',
      thumbnail: 'https://i.ytimg.com/vi/UF8uR6Z6KLc/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=UF8uR6Z6KLc'
    },
    {
      rank: 4,
      title: 'Aprenda algo novo em poucos minutos',
      channel: '@manualdomundo',
      likes: '10,6 mil',
      views: '143 mil',
      thumbnail: 'https://i.ytimg.com/vi/ScMzIvxBSi4/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=ScMzIvxBSi4'
    },
    {
      rank: 5,
      title: 'Música para acompanhar seu dia',
      channel: '@somdacidade',
      likes: '9,4 mil',
      views: '121 mil',
      thumbnail: 'https://i.ytimg.com/vi/5qap5aO4i9A/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=5qap5aO4i9A'
    },
    {
      rank: 6,
      title: 'As perguntas que todo mundo deveria fazer',
      channel: '@paposinceros',
      likes: '8,7 mil',
      views: '109 mil',
      thumbnail: 'https://i.ytimg.com/vi/2Vv-BfVoq4g/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=2Vv-BfVoq4g'
    },
    {
      rank: 7,
      title: 'O que ninguém te contou sobre criatividade',
      channel: '@ideiasemmovimento',
      likes: '7,9 mil',
      views: '96 mil',
      thumbnail: 'https://i.ytimg.com/vi/9bZkp7q19f0/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=9bZkp7q19f0'
    },
    {
      rank: 8,
      title: 'Descubra novas formas de aprender',
      channel: '@aprendafacil',
      likes: '6,8 mil',
      views: '84 mil',
      thumbnail: 'https://i.ytimg.com/vi/kJQP7kiw5Fk/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=kJQP7kiw5Fk'
    },
    {
      rank: 9,
      title: 'Uma conversa sobre escolhas e caminhos',
      channel: '@pontodevista',
      likes: '5,9 mil',
      views: '72 mil',
      thumbnail: 'https://i.ytimg.com/vi/fJ9rUzIMcZQ/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=fJ9rUzIMcZQ'
    },
    {
      rank: 10,
      title: 'Pequenas mudanças, grandes resultados',
      channel: '@rotinapratica',
      likes: '5,2 mil',
      views: '65 mil',
      thumbnail: 'https://i.ytimg.com/vi/L_jWHffIx5E/hqdefault.jpg',
      url: 'https://www.youtube.com/watch?v=L_jWHffIx5E'
    }
  ];
}
