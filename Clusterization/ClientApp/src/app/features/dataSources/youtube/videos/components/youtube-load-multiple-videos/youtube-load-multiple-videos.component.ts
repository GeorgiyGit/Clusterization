import { Component, OnInit, ViewChild } from '@angular/core';
import { YoutubeVideoService } from '../../services/youtube-video.service';
import { ActivatedRoute, Router } from '@angular/router';
import { YoutubeVideoListComponent } from '../youtube-video-list/youtube-video-list.component';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';
import { QuotasCalculationList } from 'src/app/features/quotas/static/quotas-calculation-list';

@Component({
  selector: 'app-youtube-load-multiple-videos',
  templateUrl: './youtube-load-multiple-videos.component.html',
  styleUrls: ['./youtube-load-multiple-videos.component.scss']
})
export class YoutubeLoadMultipleVideosComponent implements OnInit{
  value: string = '';
  nextPageToken: string | undefined;

  channelId:string | undefined;

  videos: ISimpleYoutubeVideo[] = [];
  selectedVideos: ISimpleYoutubeVideo[] = [];

  filterType:string='Date';
  options:IOptionForSelectInput[]=[
    {
      value:'Date',
      description:$localize`Спочатку новіші`
    },
    {
      value:'Rating',
      description:$localize`Спочатку популярніші`
    }
  ]
  
  quotasCount:number;
  constructor(private toastrService: MyToastrService,
    private videoService: YoutubeVideoService,
    private router: Router,
    private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.channelId = this.route.snapshot.params['id'];

    this.quotasCount = QuotasCalculationList.youtubeVideo;
    
    if(this.channelId!=null)this.loadFirst();
  }


  @ViewChild(YoutubeVideoListComponent) filterChild: YoutubeVideoListComponent;
  changeValue(event: any) {
    this.value = event.target.value;
    this.nextPageToken = undefined;
  }

  isLoading: boolean = false;
  loadFirst() {
    if(this.isLoading)return;

    this.nextPageToken=undefined;
    this.isLoading = true;
    this.videoService.getWithoutLoading(this.value, this.nextPageToken,this.channelId,this.filterType).subscribe(res => {
      this.selectedVideos=[];
      
      res.videos.forEach(elem=>{
        elem.isSelectAvailable=true;
      });

      this.videos = res.videos;
      this.nextPageToken = res.nextPageToken;
      this.isLoading = false;
    },
      error => {
        this.isLoading = false;
        this.toastrService.error(error.error.Message);
      });
  }

  isLoading2:boolean;
  loadMore() {
    if(this.isLoading2)return;
    if (this.nextPageToken == undefined) return;

    this.isLoading2 = true;
    this.videoService.getWithoutLoading(this.value, this.nextPageToken,this.channelId,this.filterType).subscribe(res => {
      res.videos.forEach(elem=>{
        elem.isSelectAvailable=true;
      });

      this.videos = this.videos.concat(res.videos);
      this.nextPageToken = res.nextPageToken;
      this.isLoading2 = false;
    },
      error => {
        this.isLoading2 = false;
        this.toastrService.error(error.error.Message);
      });
  }

  selectVideo(channel: ISimpleYoutubeVideo) {
    if (this.selectedVideos.find(c => c == channel) == null) this.selectedVideos.push(channel);
  }
  unselectVideo(channel: ISimpleYoutubeVideo) {
    this.selectedVideos = this.selectedVideos.filter(c => c != channel);
  }

  load() {
    if (this.selectedVideos == null || this.selectedVideos.length == 0) {
      this.toastrService.error($localize`Відео не вибрано`);
      return;
    }

    let ids: string[] = [];
    this.selectedVideos.forEach(elem => {
      ids.push(elem.id);
    });

    this.isLoading = true;
    this.videoService.loadManyByIds(ids).subscribe(res => {
      this.videos = [];
      this.selectedVideos = [];
      this.value = '';
      this.nextPageToken = undefined;

      this.toastrService.success($localize`Відео завантажено`);
      this.isLoading = false;
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading = false;
      this.toastrService.error(error.error.Message);
    });
  }

  filterTypeChanges(type:IOptionForSelectInput){
    if(type.value!=null)this.filterType=type.value;
    this.loadFirst();
  }
}
