import { IPageParameters } from "src/app/core/models/page-parameters";

export interface ICustomerGetClusterizationProfilesRequest{
    pageParameters:IPageParameters,
    algorithmTypeId:string | undefined,
    dimensionCount:number | undefined
}