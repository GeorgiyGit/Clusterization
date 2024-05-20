export interface IYoutubeComment{
    id:string,
    totalReplyCount:number,
    
    authorDisplayName:string,
    authorProfileImageUrl:string,
    
    likeCount:number,

    publishedAtDateTimeOffset:Date,
    publishedAtRaw:string,

    textDisplay:string,
    textOriginal:string,

    channelId:string,
    videoId:string
}