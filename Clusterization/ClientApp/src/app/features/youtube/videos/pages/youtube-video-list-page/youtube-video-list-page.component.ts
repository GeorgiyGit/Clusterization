import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ISimpleVideo } from '../../models/simple-video';
import { YoutubeVideoService } from '../../services/youtube-video.service';
import { IVideoFilter } from '../../models/video-filter';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetVideosRequest } from '../../models/get-videos-request';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-youtube-video-list-page',
  templateUrl: './youtube-video-list-page.component.html',
  styleUrls: ['./youtube-video-list-page.component.scss']
})
export class YoutubeVideoListPageComponent implements OnInit{
  @Input() isSelectAvailable:boolean=false;
  @Input() isSelectOnlyLoaded:boolean=false;
  @Output() selectVideoEvent=new EventEmitter<ISimpleVideo>();
  @Output() unselectVideoEvent=new EventEmitter<ISimpleVideo>();

  request:IGetVideosRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    channelId:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }
  @Input() channelId:string;
  videos:ISimpleVideo[]=[];

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


  changeFilter(filter:IVideoFilter){
    this.request.filterStr=filter.filterStr;
    this.request.filterType=filter.filterType;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    this.request.pageParameters.pageNumber=1;

    console.log(this.request);
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

  selectVideo(video:ISimpleVideo){
    this.selectVideoEvent.emit(video);
  }
  unselectVideo(video:ISimpleVideo){
    this.unselectVideoEvent.emit(video);
  }
}
