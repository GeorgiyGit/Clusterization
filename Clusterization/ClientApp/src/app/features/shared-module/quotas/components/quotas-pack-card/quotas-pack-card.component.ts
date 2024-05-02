import { Component, EventEmitter, Input, Output, QueryList } from '@angular/core';
import { IQuotasPack } from '../../models/responses/quotas-pack';

@Component({
  selector: 'app-quotas-pack-card',
  templateUrl: './quotas-pack-card.component.html',
  styleUrl: './quotas-pack-card.component.scss'
})
export class QuotasPackCardComponent {
  @Input() quotasPack: IQuotasPack;

  @Output() selectEvent=new EventEmitter<IQuotasPack>();
  @Output() unselectEvent=new EventEmitter<IQuotasPack>();

  select(){
    this.selectEvent.emit(this.quotasPack);
  }
}
