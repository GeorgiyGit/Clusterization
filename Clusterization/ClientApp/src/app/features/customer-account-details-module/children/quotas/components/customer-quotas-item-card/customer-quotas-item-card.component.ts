import { Component, Input } from '@angular/core';
import { ICustomerQuotas } from 'src/app/features/shared-module/quotas/models/responses/customer-quotas';

@Component({
  selector: 'app-customer-quotas-item-card',
  templateUrl: './customer-quotas-item-card.component.html',
  styleUrl: './customer-quotas-item-card.component.scss'
})
export class CustomerQuotasItemCardComponent {
  @Input() item:ICustomerQuotas;
}
