import { Component, OnInit } from '@angular/core';
import { IChannelFilter } from '../../models/channel-filter';
import { ISimpleChannel } from '../../models/responses/simple-channel';
import { IGetChannelsRequest } from '../../models/requests/get-channels-request';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-youtube-channel-list-page',
  templateUrl: './youtube-channel-list-page.component.html',
  styleUrls: ['./youtube-channel-list-page.component.scss']
})
export class YoutubeChannelListPageComponent implements OnInit{
  request:IGetChannelsRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }

  channels:ISimpleChannel[]=[];

  constructor(private channelService:YoutubeChannelService,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter:IChannelFilter){
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
  loadMore(){
    if(this.isLoading)return;
    this.isLoading=true;
    this.channelService.getMany(this.request).subscribe(res=>{
      this.channels=this.channels.concat(res);
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
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
