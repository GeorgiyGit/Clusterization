import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-page',
  templateUrl: './confirm-page.component.html',
  styleUrl: './confirm-page.component.scss'
})
export class ConfirmPageComponent {
  @Output() confirmEvent = new EventEmitter<boolean>();

  @Input() mainText:string;
  @Input() text:string;
}
