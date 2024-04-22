import { IPageParameters } from "src/app/core/models/page-parameters";

export interface IGetWorkspaceAddDataPacksRequest{
    workspaceId:number,
    pageParameters:IPageParameters
}