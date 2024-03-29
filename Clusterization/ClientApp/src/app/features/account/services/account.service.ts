import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { environment } from 'src/environments/environment';
import { ILogIn } from '../models/log-in';
import { ISignUp } from '../models/sign-up';
import { ILoginResponse } from '../models/login-response';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  controllerUrl: string;
  rolesKey: 'user-roles'

  constructor(private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private myLocalStorage:MyLocalStorageService,
    private toastr: MyToastrService) {

    this.controllerUrl = environment.apiUrl + "account/";
  }

  logIn(model: ILogIn): Observable<ILoginResponse> {
    return this.http.post<ILoginResponse>(this.controllerUrl + 'log_in', model);
  }
  signUp(model: ISignUp): Observable<any> {
    return this.http.post<ILoginResponse>(this.controllerUrl + 'sign_up', model);
  }

  logout(): void {
    this.removeToken();
  }

  saveToken(token: string): void {
    this.myLocalStorage.setUserToken(token);
  }
  // getCurrentUserEmail(): string | null {
  //   return localStorage.getItem(this.userKey);
  // }
  getToken(): string | null {;
    return this.myLocalStorage.getUserToken();
  }
  getHttpOptions(): any {
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.getToken()
      })
    };
  }
  isAuthenticated(): boolean {
    return this.myLocalStorage.getUserToken() != null;
  }
  removeToken(): void {
    this.myLocalStorage.removeUserToken();
  }
  private getRoles(): string {
    const token = this.getToken();
    if (token == null) return '';
    const decodedToken = this.jwtHelper.decodeToken(token);

    return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
  }
  getUserId(): string {
    const token = this.getToken();
    if (token == null) return '';
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken['userId']
  }
  public getName():string{
    const token = this.getToken();
    if (token == null) return '';

    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
  }

  isUserAdmin(): boolean {
    return this.getRoles().includes('Admin');
  }
  isUserModerator(): boolean {
    return this.getRoles().includes('Moderator');
  }
}
