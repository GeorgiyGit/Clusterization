export interface IYoutubeVideoLoadOptions{
    parentId:string | undefined,
    dateFrom:Date | undefined,
    dateTo:Date | undefined,
    maxLoad:number,
    minCommentCount:number | undefined,
    minViewCount:number | undefined
}