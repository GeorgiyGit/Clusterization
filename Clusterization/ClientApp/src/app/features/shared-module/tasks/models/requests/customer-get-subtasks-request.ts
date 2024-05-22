import { IPageParameters } from "src/app/core/models/page-parameters";
import { ICustomerGetTasksRequest } from "./customer-get-tasks-request";

export interface ICustomerGetSubTasksRequest extends ICustomerGetTasksRequest{
    groupTaskId:string
}