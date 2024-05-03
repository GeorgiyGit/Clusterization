import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetTelegramChannelsRequest{
    filterStr:string,
    filterType:string,
    pageParameters:IPageParameters
}