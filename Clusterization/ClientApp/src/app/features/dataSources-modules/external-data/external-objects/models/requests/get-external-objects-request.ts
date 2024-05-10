import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetExternalObjectsRequest{
    pageParameters:IPageParameters,
    packId:number
}