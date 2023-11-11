import { Component, OnInit } from '@angular/core';
import { ICommentLoadOptions } from '../../models/comment-load-options';
import { YoutubeCommentsService } from '../../services/youtube-comments.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-youtube-load-comments-by-channel-page',
  templateUrl: './youtube-load-comments-by-channel-page.component.html',
  styleUrls: ['./youtube-load-comments-by-channel-page.component.scss'],
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
export class YoutubeLoadCommentsByChannelPageComponent implements OnInit{
  animationState:string='in';
  channelId:string | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxLoad:[]
  });

  get formValue() {
    return this.optionsForm.value as ICommentLoadOptions;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private commentsService:YoutubeCommentsService,
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

  isVideoDateCount:boolean=true;

  isLoading:boolean;
  load(){
    let options = this.formValue;

    options.isVideoDateCount=true;

    if(this.channelId!=null){
      options.parentId=this.channelId;
    }

    if(options.maxLoad<=0){
      this.toaster.error('Максимальна кількість дорівнює нулю');
      return;
    }

    this.commentsService.loadFromChannel(options).subscribe(res=>{
      this.toaster.success('Завдання створено');
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