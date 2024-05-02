import { IPageParameters } from "src/app/core/models/page-parameters";

export interface ICustomerGetTasksRequest{
    taskStateId:string | undefined,
    pageParameters:IPageParameters
}