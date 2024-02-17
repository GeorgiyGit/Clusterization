export interface IClusterizationWorkspace{
    id:number,
    title:string,
    description:string,
    creationTime:Date,
    typeId:string,
    typeName:string,
    entitiesCount:number,
    profilesCount:number,
    isAllDataEmbedded:boolean,
    visibleType:string,
    changingType:string,
    ownerId:string
}