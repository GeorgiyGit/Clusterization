import { Component, Input } from '@angular/core';
import { IQuotasCalculation } from '../../models/responses/quotas-calculation';

@Component({
  selector: 'app-quotas-calculation-list',
  templateUrl: './quotas-calculation-list.component.html',
  styleUrl: './quotas-calculation-list.component.scss'
})
export class QuotasCalculationListComponent {
  @Input() quotascalculationList:IQuotasCalculation[]=[];
}
