import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';
import { Router } from '@angular/router';
import { ISelectAction } from '../../models/select-action';
import { AccountService } from 'src/app/features/account/services/account.service';
import { MyToastrService } from '../../services/my-toastr.service';

@Component({
  selector: 'app-more-action-select',
  templateUrl: './more-action-select.component.html',
  styleUrls: ['./more-action-select.component.scss']
})
export class MoreActionSelectComponent{
  @Input() actions:ISelectAction[]=[];
  constructor(private accountService:AccountService,
    private toastr:MyToastrService){}

  isOpen:boolean;
  close(){
    this.isOpen=false;
  }
  toggleSelect(event:MouseEvent){
    event.stopPropagation();

    this.isOpen=!this.isOpen;
  }
  selectOption(event:MouseEvent,action:ISelectAction){
    event.stopPropagation();

    if(action.isForAuthorized && !this.accountService.isAuthenticated()){
      if(!this.accountService.isAuthenticated()){
        this.toastr.error('You are not authorized!');
      }
    }

    action.action();

    this.close();
  }
}
