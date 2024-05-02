import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetClusterizationProfilesRequest{
    workspaceId:number,
    pageParameters:IPageParameters,
    algorithmTypeId:string | undefined,
    dimensionCount:number | undefined,
    embeddingModelId:string | undefined
}