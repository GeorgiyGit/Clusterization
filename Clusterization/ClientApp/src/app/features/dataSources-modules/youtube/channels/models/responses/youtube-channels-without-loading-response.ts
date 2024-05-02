import { ISimpleYoutubeChannel } from "./simple-youtube-channel";

export interface IYoutubeChannelsWithoutLoadingResponse{
    channels:ISimpleYoutubeChannel[],
    nextPageToken:string
}