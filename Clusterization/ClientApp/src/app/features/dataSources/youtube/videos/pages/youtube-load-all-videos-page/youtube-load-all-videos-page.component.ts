import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IYoutubeVideoLoadOptions } from '../../models/youtube-video-load-options';
import { YoutubeVideoService } from '../../services/youtube-video.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-youtube-load-all-videos-page',
  templateUrl: './youtube-load-all-videos-page.component.html',
  styleUrls: ['./youtube-load-all-videos-page.component.scss'],
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
export class YoutubeLoadAllVideosPageComponent implements OnInit{
  animationState:string='in';
  channelId:string | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxLoad:[],
    minCommentCount:[],
    minViewCount:[]
  });

  get formValue() {
    return this.optionsForm.value as IYoutubeVideoLoadOptions;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }
  get minCommentCount() { return this.optionsForm.get('minCommentCount')!; }
  get minViewCount() { return this.optionsForm.get('minViewCount')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private videoService:YoutubeVideoService,
    private toaster:MyToastrService){}
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

  isLoading:boolean;
  load(){
    let options = this.formValue;

    if(this.channelId!=null){
      options.parentId=this.channelId;
    }

    if(options.maxLoad<=0){
      this.toaster.error($localize`Кількість завантажень дорівнює нулю`);
      return;
    }

    this.videoService.loadByChannel(options).subscribe(res=>{
      this.toaster.success($localize`Задачу створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }
}
