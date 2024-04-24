import { Component, OnInit } from '@angular/core';
import { ICommentLoadOptions } from '../../models/comment-load-options';
import { FormBuilder, FormGroup } from '@angular/forms';
import { YoutubeCommentsService } from '../../services/youtube-comments.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-youtube-load-all-comments-page',
  templateUrl: './youtube-load-all-comments-page.component.html',
  styleUrls: ['./youtube-load-all-comments-page.component.scss']
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
    return this.optionsForm.value as ICommentLoadOptions;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private commentService:YoutubeCommentsService,
    private toaster:MyToastrService){}
  ngOnInit(): void {
    this.animationState='in';

    this.videoId=this.route.snapshot.params['videoId'];
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
