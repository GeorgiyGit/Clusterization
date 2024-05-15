import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetYoutubeVideosRequest } from 'src/app/features/dataSources-modules/youtube/videos/models/requests/get-youtube-videos-request';
import { ISimpleYoutubeVideo } from 'src/app/features/dataSources-modules/youtube/videos/models/responses/simple-youtube-video';
import { IYoutubeVideoFilter } from 'src/app/features/dataSources-modules/youtube/videos/models/youtube-video-filter';
import { YoutubeVideoService } from 'src/app/features/dataSources-modules/youtube/videos/services/youtube-video.service';

@Component({
  selector: 'app-customer-youtube-videos-loaded-list-page',
  templateUrl: './customer-youtube-videos-loaded-list-page.component.html',
  styleUrl: './customer-youtube-videos-loaded-list-page.component.scss'
})
export class CustomerYoutubeVideosLoadedListPageComponent implements OnInit{
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
  videos:ISimpleYoutubeVideo[]=[];

  constructor(private videoService:YoutubeVideoService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
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
    this.videoService.getCustomerMany(this.request).subscribe(res=>{
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
    this.videoService.getCustomerMany(this.request).subscribe(res=>{
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
