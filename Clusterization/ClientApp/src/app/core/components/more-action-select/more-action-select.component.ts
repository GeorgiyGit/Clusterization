import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';
import { Router } from '@angular/router';
import { ISelectAction } from '../../models/select-action';

@Component({
  selector: 'app-more-action-select',
  templateUrl: './more-action-select.component.html',
  styleUrls: ['./more-action-select.component.scss']
})
export class MoreActionSelectComponent{
  @Input() actions:ISelectAction[]=[];
  constructor(private router:Router){}

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

    action.action();

    this.close();
  }
}
