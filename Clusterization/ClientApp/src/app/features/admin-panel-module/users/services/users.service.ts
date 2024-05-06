import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { IGetCustomersRequest } from '../models/requests/get-customers-request';
import { ISimpleCustomer } from '../models/responses/simple-customer';
import { ICustomerPersonalInfo } from 'src/app/features/customer-account-details-module/children/personal-information/models/customer-personal-info';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "Users/";
  }

  getCustomers(request: IGetCustomersRequest): Observable<ISimpleCustomer[]> {
    return this.http.post<ISimpleCustomer[]>(this.controllerUrl + "get_customers/", request);
  }

  customerGetPersonalInfo(): Observable<ICustomerPersonalInfo> {
    return this.http.get<ICustomerPersonalInfo>(this.controllerUrl + "customer_get_personal_info/");
  }
}
