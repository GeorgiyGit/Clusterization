import { IDimensionalityReductionTechnique } from "src/app/features/dimensionalityReduction/dimensionalityReductionTechniques/models/dimensionalityReductionTechnique";
import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";
import { IEmbeddingModel } from "src/app/features/embedding-models/models/embedding-model";

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