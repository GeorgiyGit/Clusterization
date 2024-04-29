import { Component, OnInit } from '@angular/core';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IYoutubeChannelFilter } from '../../models/youtube-channel-filter';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';
import { IGetYoutubeChannelsRequest } from '../../models/requests/get-youtube-channels-request';

@Component({
  selector: 'app-youtube-channel-list-page',
  templateUrl: './youtube-channel-list-page.component.html',
  styleUrls: ['./youtube-channel-list-page.component.scss']
})
export class YoutubeChannelListPageComponent implements OnInit{
  request:IGetYoutubeChannelsRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }

  channels:ISimpleYoutubeChannel[]=[];

  constructor(private channelService:YoutubeChannelService,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter:IYoutubeChannelFilter){
    this.request.filterStr=filter.filterStr;
    this.request.filterType=filter.filterType;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    if(this.isLoading)return;
    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.channelService.getMany(this.request).subscribe(res=>{
      this.channels=res;
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
    this.channelService.getMany(this.request).subscribe(res=>{
      this.channels=this.channels.concat(res);
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
}
