import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-select-option-input',
  templateUrl: './select-option-input.component.html',
  styleUrls: ['./select-option-input.component.scss']
})
export class SelectOptionInputComponent implements OnInit,OnChanges{
  @Input() options:IOptionForSelectInput[]=[];
  @Input() selectedOption:IOptionForSelectInput;

  @Output() sendResultEvent=new EventEmitter<IOptionForSelectInput>();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['options'] && !changes['options'].firstChange) {
      if(this.selectedOption==null){
        this.selectedOption=this.options[0];
      }
    }
  }
  ngOnInit(): void {
    if(this.selectedOption==null){
      this.selectedOption=this.options[0];
    }
  }

  isOpen:boolean;
  close(){
    this.isOpen=false;
  }
  toggleSelect(event:MouseEvent){
    event.stopPropagation();

    this.isOpen=!this.isOpen;
  }
  selectOption(event:MouseEvent,newOption:IOptionForSelectInput){
    event.stopPropagation();

    this.selectedOption=newOption;

    this.close();

    this.sendResultEvent.emit(this.selectedOption);
  }
}
