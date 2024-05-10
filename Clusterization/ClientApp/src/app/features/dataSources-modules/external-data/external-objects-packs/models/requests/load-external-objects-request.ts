export interface ILoadExternalObjectsRequest{
    file:File,
    workspaceId:number | undefined,

    visibleType:string,
    changingType:string,
    title:string,
    description:string
}