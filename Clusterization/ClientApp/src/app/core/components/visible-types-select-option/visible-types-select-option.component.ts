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
      description:$localize`Можуть змінювати всі`//Changeable by everyone
    },
    {
      value:'OnlyOwner',
      description:$localize`Може змінювати тільки власник`//Changeable Only By Owner
    }
  ];

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
