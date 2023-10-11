import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";

export interface IClusterizationProfile{
    id:number,

    algorithmId:number,
    algorithmType:ISimpleAlgorithmType,

    dimensionTypeId:number,

    clustersCount:number,
    workspaceId:number,
    isCalculated:boolean,
    isFullyCalculated:boolean,

    minTileLevel:number,
    maxTileLevel:number
}