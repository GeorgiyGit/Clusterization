import { Directive, EventEmitter, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appLongPress]'
})
export class LongPressDirective {
  @Output() longPress = new EventEmitter();

  private timeout: any;

  @HostListener('mousedown', ['$event'])
  onMouseDown(event: MouseEvent) {
    this.timeout = setTimeout(() => {
      this.longPress.emit(event);
    }, 1000);
  }

  @HostListener('mouseup')
  onMouseUp() {
    clearTimeout(this.timeout);
  }

  @HostListener('mouseleave')
  onMouseLeave() {
    clearTimeout(this.timeout);
  }
}
