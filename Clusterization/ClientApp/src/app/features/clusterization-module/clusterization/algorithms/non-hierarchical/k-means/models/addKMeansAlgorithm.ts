import { IAbstractAlgorithm } from "../../../abstractAlgorithm/models/abstractAlgorithm";

export interface IAddKMeansAlgorithm extends IAbstractAlgorithm{
    numClusters:number,
    seed:number
}