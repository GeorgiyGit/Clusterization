import { IQuotasType } from "./quotas-type";

export interface IQuotasLogs{
    id:string,
    type:IQuotasType,
    customerId:string,
    count:number,
    isPlus:boolean,
    creationTime:Date
}