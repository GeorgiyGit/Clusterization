import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { IOptionForSelectInput } from '../../models/option-for-select-input';

@Component({
  selector: 'app-select-option-input',
  templateUrl: './select-option-input.component.html',
  styleUrls: ['./select-option-input.component.scss']
})
export class SelectOptionInputComponent implements OnInit, OnChanges {
  @Input() matTooltipForIcon: string;

  @Input() options: IOptionForSelectInput[] = [];
  @Input() selectedOption: IOptionForSelectInput;

  @Input() isActive: boolean = true;

  @Input() isMoreActive: boolean = false;
  @Input() isMoreLoading: boolean = false;

  @Input() isGetFirst:boolean=true;

  @Output() sendResultEvent = new EventEmitter<IOptionForSelectInput>();
  @Output() loadMoreEvent = new EventEmitter();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['options'] && !changes['options'].firstChange) {
      if (this.selectedOption == null) {
        this.selectedOption = this.options[0];

        if(this.isGetFirst && this.selectedOption!=null){
          this.sendResultEvent.emit(this.selectedOption);
        }
      }
    }
  }
  ngOnInit(): void {
    if (this.selectedOption == null) {
      this.selectedOption = this.options[0];

      if(this.isGetFirst && this.selectedOption!=null){
        this.sendResultEvent.emit(this.selectedOption);
      }
    }
  }

  isOpen: boolean;
  close() {
    this.isOpen = false;
  }
  toggleSelect(event: MouseEvent) {
    event.stopPropagation();

    if (!this.isActive && this.isOpen == false) {
      return;
    }
    this.isOpen = !this.isOpen;
  }
  toggleSelectWithoutEvent() {
    if (!this.isActive && this.isOpen == false) {
      return;
    }
    this.isOpen = !this.isOpen;
  }
  selectOption(event: MouseEvent, newOption: IOptionForSelectInput) {
    event.stopPropagation();

    this.selectedOption = newOption;

    this.close();

    this.sendResultEvent.emit(this.selectedOption);
  }

  loadMore(event: MouseEvent){
    event.stopPropagation();

    this.loadMoreEvent.emit();
  }
}
