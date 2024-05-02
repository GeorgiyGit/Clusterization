import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { YoutubeVideoService } from '../../services/youtube-video.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ActivatedRoute } from '@angular/router';
import { IGetYoutubeVideosRequest } from '../../models/requests/get-youtube-videos-request';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';
import { IYoutubeVideoFilter } from '../../models/youtube-video-filter';

@Component({
  selector: 'app-youtube-video-list-page',
  templateUrl: './youtube-video-list-page.component.html',
  styleUrls: ['./youtube-video-list-page.component.scss']
})
export class YoutubeVideoListPageComponent implements OnInit{
  @Input() isSelectAvailable:boolean=false;
  @Input() isSelectOnlyLoaded:boolean=false;
  @Output() selectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();
  @Output() unselectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();

  request:IGetYoutubeVideosRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    channelId:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }
  @Input() channelId:string;
  videos:ISimpleYoutubeVideo[]=[];

  isEmbedded:boolean;

  constructor(private videoService:YoutubeVideoService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    if(this.channelId==null){
      this.channelId=this.route.snapshot.params['id'] as string;
    }

    if(this.channelId!=null){
      this.request.channelId=this.channelId;
      this.isEmbedded=true;
    }
    
    this.loadFirst();
  }


  changeFilter(filter:IYoutubeVideoFilter){
    this.request.filterStr=filter.filterStr;
    this.request.filterType=filter.filterType;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    if(this.isLoading)return;

    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.videoService.getMany(this.request).subscribe(res=>{
      if(this.isSelectAvailable){
        res.forEach(elem=>{
          elem.isSelectAvailable=true
        });
      }

      this.videos=res;
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoading2:boolean;
  loadMore(){
    if(this.isLoading2)return;
    this.isLoading2=true;
    this.videoService.getMany(this.request).subscribe(res=>{
      if(this.isSelectAvailable){
        res.forEach(elem=>{
          elem.isSelectAvailable=true
        });
      }

      this.videos=this.videos.concat(res);
      this.isLoading2=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading2=false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable:boolean;
  addMore(){
    if(this.isLoadMoreAvailable){
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }

  selectVideo(video:ISimpleYoutubeVideo){
    this.selectVideoEvent.emit(video);
  }
  unselectVideo(video:ISimpleYoutubeVideo){
    this.unselectVideoEvent.emit(video);
  }
}
