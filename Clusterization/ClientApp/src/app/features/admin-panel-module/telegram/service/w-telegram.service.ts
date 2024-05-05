import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class WTelegramService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "WTelegram/";
  }

  getStatus(): Observable<string> {
    return this.http.get<string>(this.controllerUrl + "status/");
  }

  submitCode(code:string): Observable<string> {
    return this.http.post<string>(this.controllerUrl + "config_verification_code/",{
      code:code
    });
  }
}
