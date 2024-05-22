import {IMyTask} from "./my-task";
export interface IMyFullTask extends IMyTask{
    description:string,
    customerId:string
}