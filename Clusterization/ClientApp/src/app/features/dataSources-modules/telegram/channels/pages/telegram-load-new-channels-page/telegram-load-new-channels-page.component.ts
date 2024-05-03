import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-telegram-load-new-channels-page',
  templateUrl: './telegram-load-new-channels-page.component.html',
  styleUrl: './telegram-load-new-channels-page.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translateY(0)' })),
      state('hidden', style({ transform: 'translateY(100%)' })),
      transition('void => in', [
        style({ transform: 'translateY(100%)' }),
        animate('300ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in => hidden', animate('300ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ])
  ]
})
export class TelegramLoadNewChannelsPageComponent implements OnInit{
  animationState:string='in';

  constructor(private router:Router,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    this.animationState='in';
  }

  isLoadById:boolean=true;
  loadById(){
    this.isLoadById=true;
  }
  loadByName(){
    this.isLoadById=false;
  }

  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

}
