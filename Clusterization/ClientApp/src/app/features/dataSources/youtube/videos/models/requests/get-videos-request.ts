import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetVideosRequest{
    filterStr:string,
    filterType:string,
    channelId:string | undefined,
    pageParameters:IPageParameters
}