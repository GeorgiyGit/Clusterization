import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddQuotasToCustomer } from '../models/requests/add-quotas-to-customer';
import { ICustomerQuotas } from '../models/responses/customer-quoatas';

@Injectable({
  providedIn: 'root'
})
export class CustomerQuotasService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "CustomerQuotas/";
  }

  addQuotasToCustomer(request: IAddQuotasToCustomer): Observable<any> {
    return this.http.post(this.controllerUrl + "add_to_customer", request);
  }
  getAll(): Observable<ICustomerQuotas[]> {
    return this.http.get<ICustomerQuotas[]>(this.controllerUrl + "get_all");
  }
}
