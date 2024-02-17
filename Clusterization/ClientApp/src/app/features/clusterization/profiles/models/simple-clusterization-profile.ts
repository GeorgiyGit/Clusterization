import { IDimensionalityReductionTechnique } from "src/app/features/dimensionalityReduction/dimensionalityReductionTechniques/models/dimensionalityReductionTechnique";
import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";

export interface ISimpleClusterizationProfile{
    id:number,
    dimensionCount:number,
    algorithmType:ISimpleAlgorithmType,
    dimensionalityReductionTechnique:IDimensionalityReductionTechnique,

    fullTitle:string,

    isElected:boolean,

    visibleType:string,
    changingType:string,
    ownerId:string
}