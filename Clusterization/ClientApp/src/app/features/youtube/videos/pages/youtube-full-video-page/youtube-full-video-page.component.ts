import { Component, OnInit } from '@angular/core';
import { YoutubeVideoService } from '../../services/youtube-video.service';
import { ISimpleVideo } from '../../models/simple-video';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
@Component({
  selector: 'app-youtube-full-video-page',
  templateUrl: './youtube-full-video-page.component.html',
  styleUrls: ['./youtube-full-video-page.component.scss']
})
export class YoutubeFullVideoPageComponent implements OnInit {
  video: ISimpleVideo;


  isLoading: boolean;
  constructor(private videoService: YoutubeVideoService,
    private route: ActivatedRoute,
    private router:Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.videoService.getById(id).subscribe(res => {
      this.video = res;
      
      //this.video.videoImageUrl=this.video.channelImageUrl.replace('s88','s240');

      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + 'скопійовано!!!');

  }

  navigateToLoadByVideo(){
    this.router.navigate([{outlets: {overflow: 'load-comments-by-video/'+this.video.id}}]);
  }
}
