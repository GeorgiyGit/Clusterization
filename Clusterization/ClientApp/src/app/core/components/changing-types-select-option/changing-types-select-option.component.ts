import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-changing-types-select-option',
  templateUrl: './changing-types-select-option.component.html',
  styleUrl: './changing-types-select-option.component.scss'
})
export class ChangingTypesSelectOptionComponent {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isNullAvailable:boolean;

  options: IOptionForSelectInput[] = [
    {
      value:'AllCustomers',
      description:'Changeable by everyone'
    },
    {
      value:'OnlyOwner',
      description:'Changeable Only By Owner'
    }
  ];

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
