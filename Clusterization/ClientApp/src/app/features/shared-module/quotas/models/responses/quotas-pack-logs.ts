import { IQuotasPack } from "./quotas-pack";

export interface IQuotasPackLogs{
    id:number,
    pack:IQuotasPack,
    customerId:string,
    creationTime:Date
}