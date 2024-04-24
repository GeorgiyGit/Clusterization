import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IAddClusterizationWorkspace } from '../../models/requests/addClusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';
@Component({
  selector: 'app-add-workspace-page',
  templateUrl: './add-workspace-page.component.html',
  styleUrls: ['./add-workspace-page.component.scss'],
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
export class AddWorkspacePageComponent implements OnInit {
  animationState: string = 'in';

  workspaceForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(3000)]],
  });

  typeId: string;
  visibleType:string='AllCustomers';
  changingType:string='AllCustomers';

  get formValue() {
    return this.workspaceForm.value as IAddClusterizationWorkspace;
  }

  get title() { return (this.workspaceForm.get('title')!); }
  get description() { return this.workspaceForm.get('description')!; }

  constructor(private fb: FormBuilder,
    private workspaceService: ClusterizationWorkspaceService,
    private toaster: MyToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.animationState = 'in';
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    if (this.typeId == undefined) {
      this.toaster.error($localize`Тип не вибраний`);
      return;
    }

    model.typeId = this.typeId;
    model.visibleType=this.visibleType;
    model.changingType=this.changingType;

    this.isLoading = true;
    this.workspaceService.add(model).subscribe(res => {
      this.toaster.success($localize`Робочий простір добавлено`);
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }

  changeTypeId(id: string) {
    this.typeId = id;
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
