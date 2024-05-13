import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-visible-types-select-option',
  templateUrl: './visible-types-select-option.component.html',
  styleUrl: './visible-types-select-option.component.scss'
})
export class VisibleTypesSelectOptionComponent implements OnInit, OnChanges {
  @Output() sendEvent = new EventEmitter<string>();

  tooltipForIcon: string = $localize`Область видимості`;
  @Input() isNullAvailable: boolean;
  @Input() selectedType: string;

  selectedOption: IOptionForSelectInput;

  options: IOptionForSelectInput[] = [
    {
      value: 'AllCustomers',
      description: $localize`Можуть бачити всі`//Changeable by everyone
    },
    {
      value: 'OnlyOwner',
      description: $localize`Може бачити тільки власник`//Changeable Only By Owner
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
