import { IQuotasType } from "./quotas-type";

export interface IQuotasCalculation{
    type:IQuotasType,
    count:number
}