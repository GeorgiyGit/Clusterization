import { animate, state, style, transition, trigger } from '@angular/animations';
import { IUpdateClusterizationWorkspace } from './../../models/requests/updateClusterizationWorkspace';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';

@Component({
  selector: 'app-update-workspace-page',
  templateUrl: './update-workspace-page.component.html',
  styleUrl: './update-workspace-page.component.scss',
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
export class UpdateWorkspacePageComponent implements OnInit {
  animationState: string = 'in';
  id:number;


  workspaceForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(3000)]],
  });

  visibleType:string='AllCustomers';
  changingType:string='AllCustomers';

  get formValue() {
    return this.workspaceForm.value as IUpdateClusterizationWorkspace;
  }

  get title() { return (this.workspaceForm.get('title')!); }
  get description() { return this.workspaceForm.get('description')!; }

  constructor(private fb: FormBuilder,
    private workspaceService: ClusterizationWorkspaceService,
    private toaster: MyToastrService,
    private router: Router,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.animationState = 'in';
    this.id = this.route.snapshot.params['id'];

    this.workspaceService.getSimpleById(this.id).subscribe(res=>{
      this.workspaceForm=this.fb.group({
        title: [res.title, [Validators.required, Validators.maxLength(100)]],
        description: [res.description, [Validators.maxLength(3000)]],
      });
      console.log(res);
      this.visibleType=res.visibleType;
      this.changingType=res.changingType;
    },error=>{
      this.toaster.error(error.error.Message);
    })
  }

  isLoading: boolean;
  submit() {
    let model = this.formValue;

    model.visibleType=this.visibleType;
    model.changingType=this.changingType;
    model.id=this.id;

    this.isLoading = true;
    this.workspaceService.update(model).subscribe(res => {
      this.toaster.success($localize`Робочий простір оновлено`);
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
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

