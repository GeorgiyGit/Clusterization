import { ISimpleCustomer } from "src/app/features/admin-panel/users/models/responses/simple-customer";
import { IEmbeddingLoadingState } from "src/app/features/embedding-loading-states/models/responses/embedding-loading-state";

export interface IFullWorkspaceAddDataPack{
    id:number,
    dataObjectsCount:number,
    owner:ISimpleCustomer,

    embeddingLoadingStates:IEmbeddingLoadingState[],

    creationTime:Date,
    isDeleted:boolean
}