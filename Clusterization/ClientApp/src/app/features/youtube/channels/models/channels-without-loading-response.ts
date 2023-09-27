import { ISimpleChannel } from "./simple-channel";

export interface IChannelsWithoutLoadingResponse{
    channels:ISimpleChannel[],
    nextPageToken:string
}