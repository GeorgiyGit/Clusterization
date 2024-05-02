export interface IAddYoutubeCommentsToWorkspaceByVideosRequest{
    workspaceId:number,
    maxCountInVideo:number,
    videoIds:string[],
    dateFrom:Date | undefined,
    DateTo:Date | undefined
}