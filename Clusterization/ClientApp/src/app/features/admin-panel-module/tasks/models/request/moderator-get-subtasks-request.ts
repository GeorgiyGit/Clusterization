import { IModeratorGetTasksRequest } from "./moderator-get-tasks-request";

export interface IModeratorGetSubTasksRequest extends IModeratorGetTasksRequest{
    groupTaskId:string
}