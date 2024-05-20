import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetClusterDataRequest{
    clusterId:number,
    pageParameters:IPageParameters
}