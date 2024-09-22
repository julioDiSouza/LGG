import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { EncryptionService } from '../../services/encryption.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl: string = environment.apiServer + '/api';

  constructor(
    private http: HttpClient,
    private encryption: EncryptionService
  ) { }
  
  encurtarUrl(model:any, logado:boolean){
    return logado ? this.encurtarUrlLogado(model) : this.encurtarUrlPublico(model);
  }
  
  encurtarUrlPublico(model:any){
    return this.http.post<any>(`${this.baseUrl}/encurta-url/?temp_token=${encodeURIComponent(this.encryption.getTempToken())}`, model);
  }

  encurtarUrlLogado(model:any){
    return this.http.post<any>(`${this.baseUrl}/encurta-url/logado`, model);
  }

  ping(){
    return this.http.get<any>(`${this.baseUrl}/teste/ping`);
  }

  autenticado(){
    return this.http.get<any>(`${this.baseUrl}/teste/autenticacao`);
  }

  autenticadoAdmin(){
    return this.http.get<any>(`${this.baseUrl}/teste/autenticacao-admin`);
  }

  obterLinksCurtos(pagina:any, quantidade:any){
    return this.http.get<any>(`${this.baseUrl}/urls/${pagina}/${quantidade}`);
  }
  
  apagarLink(id:any){
    return this.http.delete<any>(`${this.baseUrl}/url/${id}`);
  }
}
