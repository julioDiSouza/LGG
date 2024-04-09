import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EncryptionService {
  private secretKey = environment.cryptoKey;
  private key = CryptoJS.enc.Utf8.parse(this.secretKey);
  private iv = CryptoJS.enc.Utf8.parse(this.secretKey);

  constructor() {}

  encryptUsingAES256(text: any): any {
    var encrypted = CryptoJS.AES.encrypt(
      CryptoJS.enc.Utf8.parse(text),
      this.key,
      {
        keySize: 128 / 8,
        iv: this.iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7,
      }
    );

    return encrypted.toString();
  }

  decryptUsingAES256(decString: any) {
    var decrypted = CryptoJS.AES.decrypt(decString, this.key, {
      keySize: 128 / 8,
      iv: this.iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });
    return decrypted.toString(CryptoJS.enc.Utf8);
  }

  getTempToken() {
    const localDate = new Date(new Date().getTime() + (-3 * 60 * 60 * 1000));
    let dataAtual = localDate.toISOString().slice(0, 19).replace('T', ' ');
    return this.encryptUsingAES256(dataAtual);
  }

}
