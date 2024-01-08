import { IAbstractAlgorithm } from "../../../abstractAlgorithm/models/abstractAlgorithm";

export interface IAddSpectralClusteringAlgorithm extends IAbstractAlgorithm{
    numClusters:number,
    gamma:number
}