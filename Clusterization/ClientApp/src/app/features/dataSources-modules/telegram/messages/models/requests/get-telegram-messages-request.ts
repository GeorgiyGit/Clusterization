import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetTelegramMessagesRequest{
    filterStr:string,
    filterType:string,
    channelId:number | undefined,
    pageParameters:IPageParameters
}