import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { YoutubeCommentsService } from '../../services/youtube-comments.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IYoutubeCommentLoadOptions } from '../../models/youtube-comment-load-options';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-youtube-load-all-comments-page',
  templateUrl: './youtube-load-all-comments-page.component.html',
  styleUrls: ['./youtube-load-all-comments-page.component.scss'],
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
export class YoutubeLoadAllCommentsPageComponent implements OnInit{
  animationState:string='in';
  videoId:string | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxLoad:[],
  });

  get formValue() {
    return this.optionsForm.value as IYoutubeCommentLoadOptions;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }

  quotasCount:number;
  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private commentService:YoutubeCommentsService,
    private toaster:MyToastrService){}
  ngOnInit(): void {
    this.animationState='in';

    this.videoId=this.route.snapshot.params['videoId'];

    this.quotasCount = QuotasCalculationList.youtubComment;
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

    if(this.videoId!=null){
      options.parentId=this.videoId;
    }

    if(options.maxLoad<=0){
      this.toaster.error($localize`Кількість завантажень дорівнює нулю`);
      return;
    }

    this.commentService.loadFromVideo(options).subscribe(res=>{
      this.toaster.success($localize`Задачу створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }
}
