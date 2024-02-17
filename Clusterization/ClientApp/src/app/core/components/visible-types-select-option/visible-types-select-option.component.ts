import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-visible-types-select-option',
  templateUrl: './visible-types-select-option.component.html',
  styleUrl: './visible-types-select-option.component.scss'
})
export class VisibleTypesSelectOptionComponent {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  options: IOptionForSelectInput[] = [
    {
      value:'AllCustomers',
      description:'Visible For All'
    },
    {
      value:'OnlyOwner',
      description:'Visible Only For Owner'
    }
  ];

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
