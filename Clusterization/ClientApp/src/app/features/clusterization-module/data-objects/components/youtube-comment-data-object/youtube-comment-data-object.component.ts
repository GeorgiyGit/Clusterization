import { Component, Input } from '@angular/core';
import { IYoutubeComment } from 'src/app/features/dataSources-modules/youtube/comments/models/youtube-comment';

@Component({
  selector: 'app-youtube-comment-data-object',
  templateUrl: './youtube-comment-data-object.component.html',
  styleUrl: './youtube-comment-data-object.component.scss'
})
export class YoutubeCommentDataObjectComponent {
  @Input() model: IYoutubeComment;
}
