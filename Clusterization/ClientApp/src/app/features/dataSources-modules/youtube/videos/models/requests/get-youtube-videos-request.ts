import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetYoutubeVideosRequest{
    filterStr:string,
    filterType:string,
    channelId:string | undefined,
    pageParameters:IPageParameters
}