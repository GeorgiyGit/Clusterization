import { Inject, Injectable, LOCALE_ID } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService,
    @Inject(LOCALE_ID) public locale: string) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.accountService.isAuthenticated()) {
      const authReq = req.clone({
        headers: new HttpHeaders({
          'Authorization': `Bearer ${this.accountService.getToken()}`,
          'Accept-Language':this.locale
        })
      });
      return next.handle(authReq);
    } else {
      const authReq = req.clone({
        headers: new HttpHeaders({
          'Accept-Language':this.locale
        })
      });
      return next.handle(authReq);
    }
  }
}

