import { Component, OnInit } from '@angular/core';
import { IGetTelegramChannelsRequest } from '../../models/requests/get-telegram-channels-request';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';
import { TelegramChannelsService } from '../../services/telegram-channels.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ITelegramChannelFilter } from '../../models/telegram-channel-filter';

@Component({
  selector: 'app-telegram-channel-list-page',
  templateUrl: './telegram-channel-list-page.component.html',
  styleUrl: './telegram-channel-list-page.component.scss'
})
export class TelegramChannelListPageComponent implements OnInit{
  request:IGetTelegramChannelsRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }

  channels:ISimpleTelegramChannel[]=[];

  constructor(private channelService:TelegramChannelsService,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter:ITelegramChannelFilter){
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
