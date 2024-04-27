import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { IYoutubeVideoFilter } from '../../models/youtube-video-filter';

@Component({
  selector: 'app-youtube-videos-search-filter',
  templateUrl: './youtube-videos-search-filter.component.html',
  styleUrls: ['./youtube-videos-search-filter.component.scss']
})
export class YoutubeVideosSearchFilterComponent {

  @Input() filter:IYoutubeVideoFilter={
    filterStr:"",
    filterType:"ByTimeDesc"
  };

  @Output() sendEvent=new EventEmitter<IYoutubeVideoFilter>();

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
      value:'ByCommentsDesc',
      description:$localize`Спочатку більше коментарів`
    },
    {
      value:'ByCommentsInc',
      description:$localize`Спочатку менше коментарів`
    },
    {
      value:'ByViewDesc',
      description:$localize`Спочатку більше переглядів`
    },
    {
      value:'ByViewInc',
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
