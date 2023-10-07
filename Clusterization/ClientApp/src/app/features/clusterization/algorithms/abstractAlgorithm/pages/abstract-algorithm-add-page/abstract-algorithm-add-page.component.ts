import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-abstract-algorithm-add-page',
  templateUrl: './abstract-algorithm-add-page.component.html',
  styleUrls: ['./abstract-algorithm-add-page.component.scss'],
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
export class AbstractAlgorithmAddPageComponent implements OnInit{
  animationState: string = 'in';

  typeId:string;

  ngOnInit(): void {
    this.animationState = 'in';
  }

  constructor(private router:Router){}

  selectType(typeId:string){
    this.typeId=typeId;
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
