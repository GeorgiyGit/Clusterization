import { IQuotasPackItem } from "./quotas-pack-item";

export interface IQuotasPack {
    id: number,
    items: IQuotasPackItem[],
    isSelected:boolean
}