import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetExternalObjectsPacksRequest{
    pageParameters:IPageParameters,
    filterStr:string
}