import { IEmbeddingModel } from "src/app/features/embedding-models/models/embedding-model";

export interface IEmbeddingLoadingState{
    id:number,
    isAllEmbeddingsLoaded:boolean,
    embeddingModel:IEmbeddingModel,

    profileId:number | undefined,
    addPackId:number | undefined
}