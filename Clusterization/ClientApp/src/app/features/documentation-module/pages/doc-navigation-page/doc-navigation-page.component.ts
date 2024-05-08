import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doc-navigation-page',
  templateUrl: './doc-navigation-page.component.html',
  styleUrl: './doc-navigation-page.component.scss',
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
export class DocNavigationPageComponent implements OnInit {
  states: SearchStates = new SearchStates();

  ngOnInit(): void {
    this.states.SearchFor['data_sources'] = false;

    this.states.SearchFor['data_sources/youtube'] = false;
    this.states.SearchFor['data_sources/youtube/channels'] = false;
    this.states.SearchFor['data_sources/youtube/videos'] = false;
    this.states.SearchFor['data_sources/youtube/comments'] = false;

    this.states.SearchFor['data_sources/telegram'] = false;
    this.states.SearchFor['data_sources/telegram/channels'] = false;
    this.states.SearchFor['data_sources/telegram/messages'] = false;
    this.states.SearchFor['data_sources/telegram/replies'] = false;

    this.states.SearchFor['algorithms'] = false;
    this.states.SearchFor['workspaces'] = false;
    this.states.SearchFor['profiles'] = false;

    this.states.SearchFor['embeddings'] = false;
    this.states.SearchFor['points-map'] = false;

    this.states.SearchFor['quotas'] = false;
  }
  toggleSelect(key: string) {
    this.states.SearchFor[key] = !this.states.SearchFor[key];
  }

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
export class SearchStates {
  SearchFor: Dictionary<boolean> = {};
}
interface Dictionary<T> {
  [Key: string]: T;
}