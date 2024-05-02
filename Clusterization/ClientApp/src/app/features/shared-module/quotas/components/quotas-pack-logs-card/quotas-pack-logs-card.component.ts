import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IQuotasPackLogs } from '../../models/responses/quotas-pack-logs';

@Component({
  selector: 'app-quotas-pack-logs-card',
  templateUrl: './quotas-pack-logs-card.component.html',
  styleUrl: './quotas-pack-logs-card.component.scss'
})
export class QuotasPackLogsCardComponent {
  @Input() logs: IQuotasPackLogs;
}
