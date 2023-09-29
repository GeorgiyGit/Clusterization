export interface IAddCommentsToWorkspaceByChannelRequest{
    workspaceId:number,
    maxCount:number,
    channelId:string,
    dateFrom:Date | undefined,
    DateTo:Date | undefined,
    isVideoDateCount:boolean
}