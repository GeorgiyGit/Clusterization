import { ICluster } from "./cluster";
import { IClusterDataObject } from "./cluster-data-object";

export interface IClusterData{
    cluster:ICluster | undefined,
    dataObject:IClusterDataObject | undefined
}