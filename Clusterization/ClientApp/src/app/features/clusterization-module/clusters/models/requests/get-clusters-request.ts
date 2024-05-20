import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetClustersRequest{
    profileId:number,
    pageParameters:IPageParameters
}