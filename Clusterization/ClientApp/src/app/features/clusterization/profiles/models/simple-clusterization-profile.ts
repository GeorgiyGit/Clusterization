import { ISimpleAlgorithmType } from "../../algorithms/algorithmType/models/simpleAlgorithmType";

export interface ISimpleClusterizationProfile{
    id:number,
    dimensionType:number,
    algorithmType:ISimpleAlgorithmType
}