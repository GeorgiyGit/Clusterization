export interface IAddTelegramRepliesToWorkspaceByMessagesRequest{
    workspaceId:number,
    maxCountInMessage:number,

    dateFrom:Date | undefined,
    dateTo:Date | undefined,

    messageIds:number[]
}