import { IAbstractAlgorithm } from "../../../abstractAlgorithm/models/abstractAlgorithm";

export interface IAddDBSCANAlgorithm extends IAbstractAlgorithm{
    epsilon:number,
    minimumPointsPerCluster:number
}