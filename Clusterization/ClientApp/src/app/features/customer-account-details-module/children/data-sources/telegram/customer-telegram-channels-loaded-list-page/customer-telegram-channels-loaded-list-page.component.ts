import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetTelegramChannelsRequest } from 'src/app/features/dataSources-modules/telegram/channels/models/requests/get-telegram-channels-request';
import { ISimpleTelegramChannel } from 'src/app/features/dataSources-modules/telegram/channels/models/responses/simple-telegram-channel';
import { ITelegramChannelFilter } from 'src/app/features/dataSources-modules/telegram/channels/models/telegram-channel-filter';
import { TelegramChannelsService } from 'src/app/features/dataSources-modules/telegram/channels/services/telegram-channels.service';

@Component({
  selector: 'app-customer-telegram-channels-loaded-list-page',
  templateUrl: './customer-telegram-channels-loaded-list-page.component.html',
  styleUrl: './customer-telegram-channels-loaded-list-page.component.scss'
})
export class CustomerTelegramChannelsLoadedListPageComponent implements OnInit{
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
    this.channelService.getCustomerMany(this.request).subscribe(res=>{
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
    this.channelService.getCustomerMany(this.request).subscribe(res=>{
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
