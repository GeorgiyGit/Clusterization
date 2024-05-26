import { Component, Input } from '@angular/core';
import { ISelectAction } from '../../models/select-action';
import { MyToastrService } from '../../services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

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
  notAuthorizedErrorStr=$localize`Ви не авторизовані!`;
  selectOption(event:MouseEvent,action:ISelectAction){
    event.stopPropagation();

    if(action.isForAuthorized && !this.accountService.isAuthenticated()){
      if(!this.accountService.isAuthenticated()){
        this.toastr.error(this.notAuthorizedErrorStr);
        return;
      }
    }

    action.action();

    this.close();
  }
}
