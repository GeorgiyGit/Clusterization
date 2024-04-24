import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IAddCommentsToWorkspaceByVideosRequest } from '../../models/requests/addCommentsToWorkspaceByVideos';
import { YoutubeDataObjectsService } from '../../services/youtube-data-objects.service';
import { ISimpleVideo } from '../../../videos/models/responses/simple-video';

@Component({
  selector: 'app-add-youtube-videos-comments-to-workspace',
  templateUrl: './add-youtube-videos-comments-to-workspace.component.html',
  styleUrl: './add-youtube-videos-comments-to-workspace.component.scss',
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
export class AddYoutubeVideosCommentsToWorkspaceComponent implements OnInit{
  animationState:string='in';
  channelId:string;

  videos:ISimpleVideo[]=[];

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxCountInVideo:[]
  });

  get formValue() {
    return this.optionsForm.value as IAddCommentsToWorkspaceByVideosRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxCountInVideo() { return this.optionsForm.get('maxCountInVideo')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private youtubeDataObjectsService:YoutubeDataObjectsService,
    private toaster:MyToastrService,
    private storageService:MyLocalStorageService){}
  ngOnInit(): void {
    this.animationState='in';

    let channelId=this.route.snapshot.params['channelId'];

    if(channelId!=null){
      this.channelId=channelId;
    }
  }

  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isLoading:boolean;
  load(){
    let options = this.formValue;

    if(options.maxCountInVideo<=0){
      this.toaster.error($localize`Максимальна кількість дорівнює нулю`);
      return;
    }

    let workspaceId =this.storageService.getSelectedWorkspace();

    if(workspaceId==null){
      this.toaster.error($localize`Робочий простір не вибрано`);
      return;
    }

    if(this.videos.length==0){
      this.toaster.error($localize`Жодного відео не вибрано`);
      return;
    }

    let ids:string[]=[];

    this.videos.forEach(video=>{
      ids.push(video.id);
    })

    options.videoIds=ids;
    options.workspaceId=workspaceId;

    this.youtubeDataObjectsService.addCommentsByVideos(options).subscribe(res=>{
      this.toaster.success($localize`Коментарі додано`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }

  selectVideo(video:ISimpleVideo){
    this.videos.push(video);
  }
  unselectVideo(video:ISimpleVideo){
    this.videos=this.videos.filter(e=>e!=video);
  }
}
