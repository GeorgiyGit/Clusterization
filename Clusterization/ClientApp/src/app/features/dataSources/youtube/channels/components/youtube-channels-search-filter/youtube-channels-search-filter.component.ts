import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IChannelFilter } from '../../models/channel-filter';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-youtube-channels-search-filter',
  templateUrl: './youtube-channels-search-filter.component.html',
  styleUrls: ['./youtube-channels-search-filter.component.scss']
})
export class YoutubeChannelsSearchFilterComponent {

  @Input() filter:IChannelFilter={
    filterStr:"",
    filterType:"ByTimeDesc"
  };

  @Output() sendEvent=new EventEmitter<IChannelFilter>();

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
      value:'BySubscribersDesc',
      description:$localize`Спочатку більше підписників`
    },
    {
      value:'BySubscribersInc',
      description:$localize`Спочатку менше підписників`
    },
    {
      value:'ByVideoCountDesc',
      description:$localize`Спочатку більше відео`
    },
    {
      value:'ByVideoCountInc',
      description:$localize`Спочатку менше відео`
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
