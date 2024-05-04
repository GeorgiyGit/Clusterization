export interface ISimpleTelegramMessage{
    id:number,
    date:Date,
    message:string,
    postAuthor:string,
    views:number,

    loadedDate:Date,
    telegramRepliesCount:number,

    isLoaded:boolean,

    isSelectAvailable:boolean,
    isSelected:boolean
}