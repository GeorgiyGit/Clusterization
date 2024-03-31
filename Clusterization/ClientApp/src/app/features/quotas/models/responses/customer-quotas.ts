import { IQuotasType } from "./quotas-type"

export interface ICustomerQuotas{
    id:number,
    
    type:IQuotasType,

    expiredCount:number,
    availableCount:number
}