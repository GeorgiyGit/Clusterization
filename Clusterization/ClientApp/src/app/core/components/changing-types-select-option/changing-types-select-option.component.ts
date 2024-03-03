import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-changing-types-select-option',
  templateUrl: './changing-types-select-option.component.html',
  styleUrl: './changing-types-select-option.component.scss'
})
export class ChangingTypesSelectOptionComponent {
  @Output() sendEvent = new EventEmitter<string>();
  tooltipForIcon:string=$localize`Область модифікації`;
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
