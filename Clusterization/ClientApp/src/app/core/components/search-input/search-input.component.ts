import { Component, EventEmitter, Input, Output } from '@angular/core';
@Component({
  selector: 'app-search-input',
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.scss']
})
export class SearchInputComponent {
  @Input() value: string = '';
  @Output() sendEvent = new EventEmitter<string>();

  changeValue(event:any){
    this.value=event.target.value;
  }

  sendText(){
    this.sendEvent.emit(this.value);
  }
}
