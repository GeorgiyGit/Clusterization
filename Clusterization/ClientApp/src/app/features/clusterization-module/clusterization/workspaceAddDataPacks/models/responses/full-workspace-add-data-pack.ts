import { ISimpleCustomer } from "src/app/features/admin-panel-module/users/models/responses/simple-customer";
import { IEmbeddingLoadingState } from "src/app/features/clusterization-module/embedding-loading-states/models/responses/embedding-loading-state";

export interface IFullWorkspaceAddDataPack{
    id:number,
    dataObjectsCount:number,
    owner:ISimpleCustomer,

    embeddingLoadingStates:IEmbeddingLoadingState[],

    creationTime:Date,
    isDeleted:boolean
}