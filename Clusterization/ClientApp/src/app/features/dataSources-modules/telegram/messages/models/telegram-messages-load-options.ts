export interface ITelegramMessagesLoadOptions{
    parentId:string | undefined,
    dateTo:Date | undefined,
    maxLoad:number,
    minCommentCount:number | undefined,
    minViewCount:number | undefined
}