export interface ISimpleVideo{
    id:string,

    title:string,
    description:string,

    publishedAtDateTimeOffset:Date,
    publishedAtRaw:string,

    videoImageUrl:string,

    commentCount:number,
    likeCount:number,
    viewCount:number,

    isLoaded:boolean,

    loadedDate:Date,

    loadedCommentCount:number,

    isSelectAvailable:boolean,
    isSelected:boolean
}