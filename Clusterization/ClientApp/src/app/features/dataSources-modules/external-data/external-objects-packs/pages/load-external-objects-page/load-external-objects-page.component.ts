import { Component, OnInit } from '@angular/core';
import { ExternalObjectsPacksService } from '../../services/external-objects-packs.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ILoadExternalObjectsRequest } from '../../models/requests/load-external-objects-request';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-load-external-objects-page',
  templateUrl: './load-external-objects-page.component.html',
  styleUrl: './load-external-objects-page.component.scss',
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
export class LoadExternalObjectsPageComponent implements OnInit {
  animationState: string = 'in';
  workspaceId: number;
  selectedFile: File | null = null;

  packForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', [Validators.maxLength(3000)]],
  });

  visibleType:string='AllCustomers';
  changingType:string='AllCustomers';

  get formValue() {
    return this.packForm.value as ILoadExternalObjectsRequest;
  }

  get title() { return (this.packForm.get('title')!); }
  get description() { return this.packForm.get('description')!; }

  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private packsService: ExternalObjectsPacksService,
    private toaster: MyToastrService) { }
  ngOnInit(): void {
    this.animationState = 'in';
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }
  changeVisibleType(type:string){
    this.visibleType=type;
  }
  changeChangingType(type:string){
    this.changingType=type;
  }

  isLoading: boolean;
  load() {
    if (this.selectedFile == null) return;

    var model = this.formValue;

    model.file=this.selectedFile;
    model.changingType=this.changingType;
    model.visibleType=this.visibleType;
    model.workspaceId=-1;

    this.isLoading=true;
    this.packsService.load(model).subscribe(res => {
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }
}
