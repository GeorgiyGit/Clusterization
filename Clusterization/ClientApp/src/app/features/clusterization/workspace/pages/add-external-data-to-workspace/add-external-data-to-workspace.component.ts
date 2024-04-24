import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IAddCommentsToWorkspaceByChannelRequest } from '../../../../dataSources/youtube/youtube-data-objects/models/requests/addCommentsToWorkspaceByChannel';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { IAddExternalData } from '../../models/external-data/add-external-data';

@Component({
  selector: 'app-add-external-data-to-workspace',
  templateUrl: './add-external-data-to-workspace.component.html',
  styleUrls: ['./add-external-data-to-workspace.component.scss'],
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
export class AddExternalDataToWorkspaceComponent implements OnInit {
  animationState: string = 'in';
  workspaceId: number;
  selectedFile: File | null = null;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private workspaceService: ClusterizationWorkspaceService,
    private toaster: MyToastrService,
    private storageService: MyLocalStorageService) { }
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
    let model: IAddExternalData = {
      workspaceId: this.workspaceId,
      file: this.selectedFile
    };
    /*this.workspaceService.addExternalData(model).subscribe(res => {
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });*/
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }
}
