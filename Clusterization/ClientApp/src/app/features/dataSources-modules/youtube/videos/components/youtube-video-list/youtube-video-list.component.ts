import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';

@Component({
  selector: 'app-youtube-video-list',
  templateUrl: './youtube-video-list.component.html',
  styleUrls: ['./youtube-video-list.component.scss']
})
export class YoutubeVideoListComponent implements OnInit{
  @Input() videos:ISimpleYoutubeVideo[]=[];
  @Input() isSelectOnlyLoaded:boolean;
  @Output() selectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();
  @Output() unselectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();

  ngOnInit(): void {
    this.phoneCalculate(window.innerWidth);
  }

  toggleSelection(value:boolean){
    this.videos.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectVideo(video:ISimpleYoutubeVideo){
    this.selectVideoEvent.emit(video);
  }
  unselectVideo(video:ISimpleYoutubeVideo){
    this.unselectVideoEvent.emit(video);
  }

  @HostListener('window:resize', ['$event'])
  onResize(event:any) {
    this.phoneCalculate(window.innerWidth);
  }

  isPhone:boolean;
  phoneCalculate(width: number) {
    if (width < 750) 
    {
      this.isPhone = true;
    }
    else 
    {
      this.isPhone=false;
    }
  }
}
