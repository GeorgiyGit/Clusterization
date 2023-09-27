import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetChannelsRequest{
    filterStr:string,
    filterType:string,
    pageParameters:IPageParameters
}