import { IMyTask } from "./my-task";

export interface IMySubTask extends IMyTask{
    position:number,
    groupTaskId:string
}