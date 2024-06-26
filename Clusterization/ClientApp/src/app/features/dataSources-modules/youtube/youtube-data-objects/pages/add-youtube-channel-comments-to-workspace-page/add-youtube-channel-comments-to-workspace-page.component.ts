import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { YoutubeDataObjectsService } from '../../services/youtube-data-objects.service';
import { IAddYoutubeCommentsToWorkspaceByChannelRequest } from '../../models/requests/addYoutubeCommentsToWorkspaceByChannel';

@Component({
  selector: 'app-add-youtube-channel-comments-to-workspace-page',
  templateUrl: './add-youtube-channel-comments-to-workspace-page.component.html',
  styleUrl: './add-youtube-channel-comments-to-workspace-page.component.scss',
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
export class AddYoutubeChannelCommentsToWorkspacePageComponent implements OnInit{
  animationState:string='in';
  channelId:string | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxCount:[]
  });

  get formValue() {
    return this.optionsForm.value as IAddYoutubeCommentsToWorkspaceByChannelRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxCount() { return this.optionsForm.get('maxCount')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private youtubeDataObjectsService:YoutubeDataObjectsService,
    private toaster:MyToastrService,
    private storageService:MyLocalStorageService){}
  ngOnInit(): void {
    this.animationState='in';

    this.channelId=this.route.snapshot.params['channelId'];
  }

  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isVideoDateCount:boolean=true;

  isLoading:boolean;
  load(){
    let options = this.formValue;

    options.isVideoDateCount=true;

    if(this.channelId!=null){
      options.channelId=this.channelId;
    }

    if(options.maxCount<=0){
      this.toaster.error($localize`Максимальна кількість дорівнює нулю`);
      return;
    }

    let workspaceId =this.storageService.getSelectedWorkspace();

    if(workspaceId==null){
      this.toaster.error($localize`Робочий простір не вибрано`);
      return;
    }
    options.workspaceId=workspaceId;

    this.youtubeDataObjectsService.addCommentsByChannel(options).subscribe(res=>{
      this.toaster.success($localize`Коментарі додано`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }


  selectVideoDate(event:any){
    this.isVideoDateCount=true;
  }
  
  selectYoutubeDate(event:any){
    this.isVideoDateCount=false;
  }
}
