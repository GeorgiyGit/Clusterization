import { IDisplayedPoint } from "./displayed-points";

export interface IClusterizationTile{
    id:number,
    
    x:number,
    y:number,
    z:number,

    length:number,

    points:IDisplayedPoint[]
}