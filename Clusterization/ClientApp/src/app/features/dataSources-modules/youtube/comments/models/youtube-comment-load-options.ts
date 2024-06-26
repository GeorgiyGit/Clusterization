export interface IYoutubeCommentLoadOptions{
    parentId:string | undefined,
    dateFrom:Date | undefined,
    dateTo:Date | undefined,
    maxLoad:number,
    isVideoDateCount:boolean | undefined,
    maxLoadForOneVideo:number
}