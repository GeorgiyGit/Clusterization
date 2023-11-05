import { IDimensionalityReductionTechnique } from "src/app/features/dimensionalityReduction/dimensionalityReductionTechniques/models/dimensionalityReductionTechnique";
import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";

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
    dimensionalityReductionTechnique:IDimensionalityReductionTechnique
}