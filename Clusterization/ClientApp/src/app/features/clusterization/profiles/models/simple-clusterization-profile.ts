import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";

export interface ISimpleClusterizationProfile{
    id:number,
    dimensionCount:number,
    algorithmType:ISimpleAlgorithmType
}