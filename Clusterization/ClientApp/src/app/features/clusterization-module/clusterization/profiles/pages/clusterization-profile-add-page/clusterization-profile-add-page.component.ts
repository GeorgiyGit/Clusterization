import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IAddClusterizationProfile } from '../../models/requests/add-clusterization-profile';

@Component({
  selector: 'app-clusterization-profile-add-page',
  templateUrl: './clusterization-profile-add-page.component.html',
  styleUrls: ['./clusterization-profile-add-page.component.scss'],
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
export class ClusterizationProfileAddPageComponent implements OnInit {
  animationState: string = 'in';

  algorithmTypeId: string;
  algorithmId: number;

  dimensionTypeId: number;

  DRTechniqueId: string;

  workspaceId: number;
  embeddingModelId:string;

  isActive: boolean;
  isAlgorithmsSelectActive:boolean;

  constructor(private profilesService: ClusterizationProfilesService,
    private toaster: MyToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.animationState = 'in';

    this.workspaceId = this.route.snapshot.params['workspaceId'];
  }

  visibleType:string='AllCustomers';
  changingType:string='AllCustomers';

  isLoading: boolean;
  submit() {
    if (this.isActive == false) return;

    if (this.algorithmId == undefined) {
      this.toaster.error($localize`Тип не вибраний`);
      return;
    }

    if (this.dimensionTypeId == undefined) {
      this.toaster.error($localize`Розмірність не вибрана`);
      return;
    }

    var model: IAddClusterizationProfile = {
      algorithmId: this.algorithmId,
      workspaceId: this.workspaceId,
      dimensionCount: this.dimensionTypeId,
      DRTechniqueId:this.DRTechniqueId,
      visibleType:this.visibleType,
      changingType:this.changingType,
      embeddingModelId:this.embeddingModelId
    };

    this.isLoading = true;
    this.profilesService.add(model).subscribe(res => {
      this.toaster.success($localize`Профіль додано`);
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }

  changeAlgorithmType(id: string) {
    if(id==undefined)this.isAlgorithmsSelectActive=false;
    else this.isAlgorithmsSelectActive=true;
    this.algorithmTypeId = id;
    this.isActive = false;
  }

  changeAlgorithm(id: string) {
    this.algorithmId = parseInt(id);

    this.isActiveChange();
  }

  changeDimensionType(id: number) {
    this.dimensionTypeId = id;

    this.isActiveChange();
  }
  changeDRTechniqueId(id: string) {
    this.DRTechniqueId = id;
    this.isActiveChange();
  }

  isActiveChange() {
    if (this.dimensionTypeId != undefined &&
      this.algorithmId != undefined &&
      this.DRTechniqueId != undefined) {
      this.isActive = true;
    }
    else {
      this.isActive = false;
    }
  }


  changeEmbeddingModel(id:string){
    this.embeddingModelId=id;
  }

  changeVisibleType(type:string){
    this.visibleType=type;
  }
  changeChangingType(type:string){
    this.changingType=type;
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
}
