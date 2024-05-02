import { IDisplayedPoint } from "./displayed-points";

export interface IClusterizationTile{
    id:number,
    
    x:number,
    y:number,
    z:number,

    tileLength:number,

    points:IDisplayedPoint[]
}