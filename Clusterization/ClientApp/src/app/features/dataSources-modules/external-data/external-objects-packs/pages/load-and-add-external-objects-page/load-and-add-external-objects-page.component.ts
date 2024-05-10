import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ILoadExternalObjectsRequest } from '../../models/requests/load-external-objects-request';
import { ExternalObjectsPacksService } from '../../services/external-objects-packs.service';

@Component({
  selector: 'app-load-and-add-external-objects-page',
  templateUrl: './load-and-add-external-objects-page.component.html',
  styleUrl: './load-and-add-external-objects-page.component.scss',
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
export class LoadAndAddExternalObjectsPageComponent implements OnInit {
  animationState: string = 'in';
  workspaceId: number;
  selectedFile: File | null = null;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private packsService: ExternalObjectsPacksService,
    private toaster: MyToastrService) { }
  ngOnInit(): void {
    this.animationState = 'in';

    this.workspaceId = this.route.snapshot.params['workspaceId'];
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isLoading: boolean;
  load() {
    if (this.selectedFile == null) return;

    let model: ILoadExternalObjectsRequest = {
      workspaceId: this.workspaceId,
      file: this.selectedFile,
      changingType: 'OnlyOwner',
      visibleType: 'OnlyOwner',
      title: 'Workspace number ' + this.workspaceId + ' external data pack',
      description: 'Workspace number ' + this.workspaceId + ' external data pack'
    }

    this.isLoading=true;
    this.packsService.loadAndAdd(model).subscribe(res => {
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
