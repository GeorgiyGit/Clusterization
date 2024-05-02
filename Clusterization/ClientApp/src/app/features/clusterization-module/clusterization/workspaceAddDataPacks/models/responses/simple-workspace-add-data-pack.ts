import { ISimpleCustomer } from "src/app/features/admin-panel-module/users/models/responses/simple-customer";

export interface ISimpleWorkspaceAddDataPack{
    id:number,
    dataObjectsCount:number,
    owner:ISimpleCustomer,

    creationTime:Date,
    isDeleted:boolean,

    workspaceChangingType:string
}