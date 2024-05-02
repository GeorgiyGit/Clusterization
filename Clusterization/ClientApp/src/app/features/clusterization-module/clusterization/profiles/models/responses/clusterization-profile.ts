import { IEmbeddingModel } from "src/app/features/clusterization-module/embedding-models/models/embedding-model";
import { ISimpleAlgorithmType } from "../../../algorithms/algorithmType/models/simpleAlgorithmType";
import { IDimensionalityReductionTechnique } from "src/app/features/clusterization-module/dimensionalityReduction/DR-techniques/models/dimensionalityReductionTechnique";

export interface IClusterizationProfile{
    id:number,

    algorithmId:number,
    algorithmType:ISimpleAlgorithmType,

    dimensionCount:number,

    clustersCount:number,
    workspaceId:number,
    isCalculated:boolean,

    minTileLevel:number,
    maxTileLevel:number,
    drTechnique:IDimensionalityReductionTechnique,

    isElected:boolean,

    visibleType:string,
    changingType:string,
    ownerId:string,
    isAllEmbeddingsLoaded:boolean,
    embeddingModel:IEmbeddingModel
}