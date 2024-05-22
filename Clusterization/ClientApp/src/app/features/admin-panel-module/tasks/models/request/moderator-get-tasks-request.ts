import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IModeratorGetTasksRequest{
    taskStateId:string | undefined,
    pageParameters:IPageParameters
    customerId:string | undefined
}