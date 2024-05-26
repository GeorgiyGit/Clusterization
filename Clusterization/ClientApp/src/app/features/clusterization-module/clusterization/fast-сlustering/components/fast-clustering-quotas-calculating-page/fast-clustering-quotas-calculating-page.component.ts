import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FastClusteringService } from '../../services/fast-clustering.service';
import { Router } from '@angular/router';
import { IFastClusteringInitialRequest } from '../../models/requests/fast-clustering-initial-request';
import { IFastClusteringProcessRequest } from '../../models/requests/fast-clustering-process-request';
import { IQuotasCalculation } from 'src/app/features/shared-module/quotas/models/responses/quotas-calculation';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IFullFastClustering } from '../../models/requests/full-fast-clustering-request';

@Component({
  selector: 'app-fast-clustering-quotas-calculating-page',
  templateUrl: './fast-clustering-quotas-calculating-page.component.html',
  styleUrl: './fast-clustering-quotas-calculating-page.component.scss',
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
export class FastClusteringQuotasCalculatingPageComponent implements OnInit {
  animationState: string = 'in';

  @Output() confirmEvent = new EventEmitter<boolean>();

  @Input() mainText:string;
  @Input() text:string;

  @Input() request:IFullFastClustering;
  
  quotasCalculation:IQuotasCalculation[]=[];
  constructor(private fastClusteringService:FastClusteringService,
    private router:Router,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.loadQuotas();
  }

  isLoading:boolean;
  loadQuotas(){
    this.isLoading=true;
    this.fastClusteringService.calculateFullFastClusteringQuotas(this.request).subscribe(res=>{
      this.quotasCalculation=res;
      this.isLoading=false;
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    })
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
