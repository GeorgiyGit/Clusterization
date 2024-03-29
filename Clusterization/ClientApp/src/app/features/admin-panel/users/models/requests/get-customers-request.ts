import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetCustomersRequest{
    filterStr:string | undefined,
    pageParameters:IPageParameters
}