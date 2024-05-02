import { IAbstractAlgorithm } from "../../../abstractAlgorithm/models/abstractAlgorithm";

export interface IAddGaussianMixtureAlgorithm extends IAbstractAlgorithm{
    numberOfComponents:number,
}