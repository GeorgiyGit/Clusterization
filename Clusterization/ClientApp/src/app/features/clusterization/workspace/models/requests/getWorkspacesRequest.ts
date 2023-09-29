import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetWorkspacesRequest{
    typeId:string | undefined,
    pageParameters:IPageParameters
}