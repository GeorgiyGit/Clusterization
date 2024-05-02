import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetQuotasLogsRequest{
    typeId:string | undefined,
    pageParameters:IPageParameters
}