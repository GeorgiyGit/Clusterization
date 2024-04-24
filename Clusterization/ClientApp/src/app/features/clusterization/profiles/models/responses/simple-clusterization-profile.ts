import { IDimensionalityReductionTechnique } from "src/app/features/dimensionalityReduction/dimensionalityReductionTechniques/models/dimensionalityReductionTechnique";
import { ISimpleAlgorithmType } from "../../../algorithms/algorithmType/models/simpleAlgorithmType";
import { IEmbeddingModel } from "src/app/features/embedding-models/models/embedding-model";

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