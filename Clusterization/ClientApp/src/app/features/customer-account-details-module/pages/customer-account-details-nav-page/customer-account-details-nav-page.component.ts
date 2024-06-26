import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { SearchStates } from 'src/app/features/documentation-module/pages/doc-navigation-page/doc-navigation-page.component';

@Component({
  selector: 'app-customer-account-details-nav-page',
  templateUrl: './customer-account-details-nav-page.component.html',
  styleUrl: './customer-account-details-nav-page.component.scss',
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
export class CustomerAccountDetailsNavPageComponent implements OnInit{
  states: SearchStates = new SearchStates();
  
  isMenuOpen: boolean = true;
  isMenuOpenAnimation: boolean = true;
  
  ngOnInit(): void {
    this.states.SearchFor['data-sources'] = false;
  }

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

  toggleSelect(key: string) {
    this.states.SearchFor[key] = !this.states.SearchFor[key];
  }
}
