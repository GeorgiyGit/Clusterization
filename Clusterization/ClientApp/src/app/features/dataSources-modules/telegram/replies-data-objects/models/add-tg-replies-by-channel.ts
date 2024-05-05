export interface IAddTelegramRepliesToWorkspaceByChannelRequest{
    workspaceId:number,
    maxCount:number,
    channelId:number,

    dateFrom:Date | undefined,
    dateTo:Date | undefined,

    isMessageDateCount:boolean
}