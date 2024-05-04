import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ITelegramMessagesFilter } from '../../models/telegram-messages-filter';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-telegram-messages-search-filter',
  templateUrl: './telegram-messages-search-filter.component.html',
  styleUrl: './telegram-messages-search-filter.component.scss'
})
export class TelegramMessagesSearchFilterComponent {
  @Input() filter:ITelegramMessagesFilter={
    filterStr:"",
    filterType:"ByTimeDesc"
  };

  @Output() sendEvent=new EventEmitter<ITelegramMessagesFilter>();

  options:IOptionForSelectInput[]=[
    {
      value:'ByTimeDesc',
      description:$localize`Спочатку новіші`
    },
    {
      value:'ByTimeInc',
      description:$localize`Спочатку старіші`
    },
    {
      value:'ByRepliesDesc',
      description:$localize`Спочатку більше відповідей`
    },
    {
      value:'ByRepliesInc',
      description:$localize`Спочатку менше відповідей`
    },
    {
      value:'ByViewsDesc',
      description:$localize`Спочатку більше переглядів`
    },
    {
      value:'ByViewsInc',
      description:$localize`Спочатку менше переглядів`
    },
  ]


  searchStrChanges(str:string){
    this.filter.filterStr=str;

    this.sendEvent.emit(this.filter);
  }

  filterTypeChanges(type:IOptionForSelectInput){
    if(type.value!=null)this.filter.filterType=type.value;
    this.sendEvent.emit(this.filter);
  }
}
