import { Component, Input, OnInit } from '@angular/core';
import { IQuotasLogs } from '../../models/responses/quotas-logs';
import { ToastrService } from 'ngx-toastr';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-quotas-logs-card',
  templateUrl: './quotas-logs-card.component.html',
  styleUrl: './quotas-logs-card.component.scss'
})
export class QuotasLogsCardComponent{
  @Input() logs: IQuotasLogs;


  constructor(private clipboard: Clipboard,
    private toastr: ToastrService) { }


  copyToClipboard(text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }
}
