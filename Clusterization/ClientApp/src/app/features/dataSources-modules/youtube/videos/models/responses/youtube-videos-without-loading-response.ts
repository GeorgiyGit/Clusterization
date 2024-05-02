import { ISimpleYoutubeVideo } from "./simple-youtube-video";

export interface IYoutubeVideosWithoutLoadingResponse{
    videos:ISimpleYoutubeVideo[],
    nextPageToken:string
}