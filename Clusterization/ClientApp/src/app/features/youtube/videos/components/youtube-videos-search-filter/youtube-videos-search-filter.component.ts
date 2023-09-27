import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { IVideoFilter } from '../../models/video-filter';

@Component({
  selector: 'app-youtube-videos-search-filter',
  templateUrl: './youtube-videos-search-filter.component.html',
  styleUrls: ['./youtube-videos-search-filter.component.scss']
})
export class YoutubeVideosSearchFilterComponent {

  @Input() filter:IVideoFilter={
    filterStr:"",
    filterType:"ByTimeDesc"
  };

  @Output() sendEvent=new EventEmitter<IVideoFilter>();

  options:IOptionForSelectInput[]=[
    {
      value:'ByTimeDesc',
      description:'Спочатку новіші'
    },
    {
      value:'ByTimeInc',
      description:'Спочатку старіші'
    },
    {
      value:'ByCommentsDesc',
      description:'Спочатку більше коментарів'
    },
    {
      value:'ByCommentsInc',
      description:'Спочатку менше коментарів'
    },
    {
      value:'ByViewDesc',
      description:'Спочатку більше переглядів'
    },
    {
      value:'ByViewInc',
      description:'Спочатку менше переглядів'
    },
  ]


  searchStrChanges(str:string){
    this.filter.filterStr=str;

    this.sendEvent.emit(this.filter);
  }

  filterTypeChanges(type:IOptionForSelectInput){
    this.filter.filterType=type.value;
    this.sendEvent.emit(this.filter);
  }
}
