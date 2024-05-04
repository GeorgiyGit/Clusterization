import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';
import { ITelegramMessagesFilter } from '../../models/telegram-messages-filter';
import { IGetTelegramMessagesRequest } from '../../models/requests/get-telegram-messages-request';
import { TelegramMessagesService } from '../../services/telegram-messages.service';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-telegram-message-list-page',
  templateUrl: './telegram-message-list-page.component.html',
  styleUrl: './telegram-message-list-page.component.scss'
})
export class TelegramMessageListPageComponent implements OnInit{
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
  @Input() channelId:number;
  messages:ISimpleTelegramMessage[]=[];

  isEmbedded:boolean;

  constructor(private messagesService:TelegramMessagesService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    if(this.channelId==null){
      this.channelId=this.route.snapshot.params['id'] as number;
    }

    if(this.channelId!=null){
      this.request.channelId=this.channelId;
      this.isEmbedded=true;
    }
    
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
    this.messagesService.getMany(this.request).subscribe(res=>{
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
    this.messagesService.getMany(this.request).subscribe(res=>{
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
