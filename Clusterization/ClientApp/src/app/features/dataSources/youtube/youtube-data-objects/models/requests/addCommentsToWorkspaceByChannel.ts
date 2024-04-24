export interface IAddCommentsToWorkspaceByChannelRequest{
    workspaceId:number,
    maxCount:number,
    channelId:string,
    dateFrom:Date | undefined,
    dateTo:Date | undefined,
    isVideoDateCount:boolean
}