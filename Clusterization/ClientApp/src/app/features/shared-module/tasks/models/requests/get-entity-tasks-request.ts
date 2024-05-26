import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetEntityTasksRequest<T>{
    id:T,
    taskStateId:string | undefined,
    pageParameters:IPageParameters
}