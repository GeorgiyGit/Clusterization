import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetYoutubeChannelsRequest{
    filterStr:string,
    filterType:string,
    pageParameters:IPageParameters
}