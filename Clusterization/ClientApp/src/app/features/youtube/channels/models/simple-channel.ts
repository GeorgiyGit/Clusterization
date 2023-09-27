export interface ISimpleChannel{
    id:string,
    title:string,
    
    publishedAtDateTimeOffset:Date,
    publishedAtRaw:string,

    subscriberCount:number,
    videoCount:number,
    viewCount:number,

    loadedVideoCount:number,
    loadedCommentCount:number,

    isLoaded:boolean,

    loadedDate:Date,
    
    channelImageUrl:string,

    isSelectAvailable:boolean,
    isSelected:boolean
}