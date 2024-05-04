export interface ILoadTelegramRepliesByChannelRequest{
    channelId:number,
    dateFrom:Date | undefined,
    dateTo:Date | undefined,

    maxLoad:number,
    maxLoadForOneMessage:number
}