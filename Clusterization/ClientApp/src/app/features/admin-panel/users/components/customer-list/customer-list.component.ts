import { Component, Input } from '@angular/core';
import { ISimpleCustomer } from '../../models/responses/simple-customer';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss'
})
export class CustomerListComponent {
  @Input() customers: ISimpleCustomer[] = [];
}
