import { ISimpleVideo } from "./simple-video";

export interface IVideosWithoutLoadingResponse{
    videos:ISimpleVideo[],
    nextPageToken:string
}