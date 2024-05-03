import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ITelegramChannelFilter } from '../../models/telegram-channel-filter';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-telegram-channels-search-filter',
  templateUrl: './telegram-channels-search-filter.component.html',
  styleUrl: './telegram-channels-search-filter.component.scss'
})
export class TelegramChannelsSearchFilterComponent {
  @Input() filter:ITelegramChannelFilter={
    filterStr:"",
    filterType:"ByTimeDesc"
  };

  @Output() sendEvent=new EventEmitter<ITelegramChannelFilter>();

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
      value:'ByParticipantsDesc',
      description:$localize`Спочатку більше учасників`
    },
    {
      value:'ByParticipantsInc',
      description:$localize`Спочатку менше учасників`
    },
    {
      value:'ByLoadedMessageCountDesc',
      description:$localize`Спочатку більше завантажених повідомлень`
    },
    {
      value:'ByLoadedMessageCountInc',
      description:$localize`Спочатку менше завантажених повідомлень`
    },
  ]


  searchStrChanges(str:string){
    this.filter.filterStr=str;

    this.sendEvent.emit(this.filter);
  }

  filterTypeChanges(type:IOptionForSelectInput){
    if(type.value!=null) this.filter.filterType=type.value;
    this.sendEvent.emit(this.filter);
  }
}
