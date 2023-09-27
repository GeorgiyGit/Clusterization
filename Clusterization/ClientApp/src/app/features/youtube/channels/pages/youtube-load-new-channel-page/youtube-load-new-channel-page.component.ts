import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-youtube-load-new-channel-page',
  templateUrl: './youtube-load-new-channel-page.component.html',
  styleUrls: ['./youtube-load-new-channel-page.component.scss'],
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
export class YoutubeLoadNewChannelPageComponent implements OnInit{
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
