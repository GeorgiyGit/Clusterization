import { AfterViewInit, Component, ElementRef, EventEmitter, HostListener, Input, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-swipeable-card',
  templateUrl: './swipeable-card.component.html',
  styleUrl: './swipeable-card.component.scss'
})
export class SwipeableCardComponent {
  @ViewChild('card') cardRef: ElementRef<HTMLDivElement>; // Reference to the card element
  offsetX = 0;
  startX = 0;
  isDragging = false;
  swipeThreshold = 0.2; // Swipe threshold (20%)

  @Output() swipeLeft = new EventEmitter();
  @Output() swipeRight = new EventEmitter();

  @Input() isSwipingAllowed = false; // Set to true by default

  constructor() { }

  onTouchStart(event: MouseEvent | TouchEvent) {
    if (!this.isSwipingAllowed) return; // Check if swiping is allowed
    this.startX = this.getEventX(event);
    this.isDragging = true;
  }

  onTouchMove(event: MouseEvent | TouchEvent) {
    if (!this.isSwipingAllowed || !this.isDragging) return; // Check if swiping is allowed and dragging has started

    const currentX = this.getEventX(event);
    this.offsetX = currentX - this.startX;

    // Move the card visually
    this.cardRef.nativeElement.style.transform = `translateX(${this.offsetX}px)`;
  }

  onTouchEnd() {
    if (!this.isSwipingAllowed || !this.isDragging) return; // Check if swiping is allowed and dragging has started

    const swipeDistance = Math.abs(this.offsetX);

    // Emit swipeLeft or swipeRight events based on swipe direction and distance
    if (swipeDistance >= this.swipeThreshold) {
      if (this.offsetX > 0) {
        this.swipeRight.emit();
      } else {
        this.swipeLeft.emit();
      }
    }

    // Reset card position visually
    this.cardRef.nativeElement.style.transform = 'translateX(0)';
    this.offsetX = 0;
    this.isDragging = false;
  }

  private getEventX(event: MouseEvent | TouchEvent): number {
    return event instanceof MouseEvent ? event.clientX : event.touches[0].clientX;
  }
}
