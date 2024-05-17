export interface ICluster{
    id:number,
    color:string,
    parentClusterId:number | undefined,
    name:string | undefined,
    childElementsCount:number 
}