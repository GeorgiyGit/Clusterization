import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-panel-navigation-page',
  templateUrl: './admin-panel-navigation-page.component.html',
  styleUrl: './admin-panel-navigation-page.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translate3d(0, 0, 0)' })),
      state('hidden', style({ transform: 'translate3d(calc(-100% + 45px), 0, 0)' })),
      transition('void => in', [
        style({ transform: 'translate3d(calc(-100% + 45px), 0, 0)' }),
        animate('500ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in <=> hidden', animate('500ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ])
  ]
})
export class AdminPanelNavigationPageComponent {
  isMenuOpen: boolean = true;
  isMenuOpenAnimation: boolean = true;
  toggleMenu() {
    if (this.isMenuOpen == true) {
      this.isMenuOpenAnimation = false;

      setTimeout(() => {
        this.isMenuOpen = false;
      }, 500)
      return;
    }
    else{
      this.isMenuOpen = true;
      this.isMenuOpenAnimation = true;
    }
  }
}
