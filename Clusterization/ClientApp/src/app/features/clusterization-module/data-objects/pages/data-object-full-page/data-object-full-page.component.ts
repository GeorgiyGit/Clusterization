import { Component, OnInit } from '@angular/core';
import { IFullDataObject } from '../../models/full-data-object';
import { MyDataObjectsService } from '../../services/my-data-objects.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-data-object-full-page',
  templateUrl: './data-object-full-page.component.html',
  styleUrl: './data-object-full-page.component.scss',
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
export class DataObjectFullPageComponent implements OnInit{
  animationState:string='in';


  dataObject:IFullDataObject;

  constructor(private router:Router,
    private toaster:MyToastrService,
    private route:ActivatedRoute,
    private dataObjectsService:MyDataObjectsService){}
  ngOnInit(): void {
    this.animationState='in';

    let taskId=this.route.snapshot.params['id'];

    this.dataObjectsService.getFullByDisplayedPointId(taskId).subscribe(res=>{
      this.dataObject=res;
    },error=>{
      this.toaster.error(error.error.Message);
    })
  }


  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
