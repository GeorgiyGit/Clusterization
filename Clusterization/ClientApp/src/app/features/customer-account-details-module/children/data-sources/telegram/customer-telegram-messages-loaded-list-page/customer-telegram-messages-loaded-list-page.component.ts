import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetTelegramMessagesRequest } from 'src/app/features/dataSources-modules/telegram/messages/models/requests/get-telegram-messages-request';
import { ISimpleTelegramMessage } from 'src/app/features/dataSources-modules/telegram/messages/models/responses/simple-telegram-message';
import { ITelegramMessagesFilter } from 'src/app/features/dataSources-modules/telegram/messages/models/telegram-messages-filter';
import { TelegramMessagesService } from 'src/app/features/dataSources-modules/telegram/messages/services/telegram-messages.service';

@Component({
  selector: 'app-customer-telegram-messages-loaded-list-page',
  templateUrl: './customer-telegram-messages-loaded-list-page.component.html',
  styleUrl: './customer-telegram-messages-loaded-list-page.component.scss'
})
export class CustomerTelegramMessagesLoadedListPageComponent implements OnInit{
  @Input() isSelectAvailable:boolean=false;
  @Input() isSelectOnlyLoaded:boolean=false;
  @Output() selectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();
  @Output() unselectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();

  request:IGetTelegramMessagesRequest={
    filterStr:'',
    filterType:'ByTimeDesc',
    channelId:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }
  messages:ISimpleTelegramMessage[]=[];

  constructor(private messagesService:TelegramMessagesService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter:ITelegramMessagesFilter){
    this.request.filterStr=filter.filterStr;
    this.request.filterType=filter.filterType;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    if(this.isLoading)return;

    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.messagesService.getCustomerMany(this.request).subscribe(res=>{
      if(this.isSelectAvailable){
        res.forEach(elem=>{
          elem.isSelectAvailable=true
        });
      }

      this.messages=res;
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
    this.messagesService.getCustomerMany(this.request).subscribe(res=>{
      if(this.isSelectAvailable){
        res.forEach(elem=>{
          elem.isSelectAvailable=true
        });
      }

      this.messages=this.messages.concat(res);
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

  selectMessage(message:ISimpleTelegramMessage){
    this.selectMessageEvent.emit(message);
  }
  unselectMessage(message:ISimpleTelegramMessage){
    this.unselectMessageEvent.emit(message);
  }
}
