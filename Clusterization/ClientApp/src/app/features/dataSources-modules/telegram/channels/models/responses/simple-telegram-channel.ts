export interface ISimpleTelegramChannel{
    id:number,
    username:string,
    isActive:boolean,
    
    photoId:number,
    
    title:string,
    participantsCount:number,

    telegramMessagesCount:number,
    telegramRepliesCount:number,
    
    loadedDate:Date,
    date:Date,

    isLoaded:boolean,

    isSelectAvailable:boolean,
    isSelected:boolean
}