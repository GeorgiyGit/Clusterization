import { IBaseSignalRResponse } from "../../../signalR/models/base-signalR-response";

export interface IChangeTaskStateEvent extends IBaseSignalRResponse{
    taskId:string,
    groupTaskId:string | undefined,

    stateName:string,
    stateId:string
}