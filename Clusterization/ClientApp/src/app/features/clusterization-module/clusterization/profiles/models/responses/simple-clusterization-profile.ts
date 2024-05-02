import { IEmbeddingModel } from "src/app/features/clusterization-module/embedding-models/models/embedding-model";
import { ISimpleAlgorithmType } from "../../../algorithms/algorithmType/models/simpleAlgorithmType";
import { IDimensionalityReductionTechnique } from "src/app/features/clusterization-module/dimensionalityReduction/DR-techniques/models/dimensionalityReductionTechnique";

export interface ISimpleClusterizationProfile{
    id:number,
    dimensionCount:number,
    algorithmType:ISimpleAlgorithmType,
    drTechnique:IDimensionalityReductionTechnique,

    fullTitle:string,

    isElected:boolean,

    visibleType:string,
    changingType:string,
    ownerId:string,

    embeddingModel:IEmbeddingModel
}