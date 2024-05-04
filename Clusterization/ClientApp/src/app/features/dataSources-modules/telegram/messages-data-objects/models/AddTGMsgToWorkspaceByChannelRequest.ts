export interface IAddTelegramMessagesToWorkspaceByChannelRequest{
    workspaceId:number,
    maxCount:number,
    channelId:number,

    dateFrom:Date | undefined,
    dateTo:Date | undefined,
}