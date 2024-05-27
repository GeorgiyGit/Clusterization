import { IBaseSignalRResponse } from "../../../signalR/models/base-signalR-response";

export interface IChangeTaskPercentsEvent extends IBaseSignalRResponse{
    taskId:string,
    groupTaskId:string | undefined,
    
    percents:number
}