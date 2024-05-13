import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-changing-types-select-option',
  templateUrl: './changing-types-select-option.component.html',
  styleUrl: './changing-types-select-option.component.scss'
})
export class ChangingTypesSelectOptionComponent implements OnInit, OnChanges{
  @Output() sendEvent = new EventEmitter<string>();
  tooltipForIcon:string=$localize`Область модифікації`;
  @Input() isNullAvailable:boolean;
  @Input() selectedType: string;

  selectedOption: IOptionForSelectInput;

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

  ngOnInit(): void {
    this.selectedOption = this.options[0];
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedType'] && !changes['selectedType'].firstChange) {
      if (this.selectedType != null) {
        let option = this.options.find(e => e.value == this.selectedType);
        if (option != null) this.selectedOption = option;
      }
    }
  }

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option.value);
  }
}
