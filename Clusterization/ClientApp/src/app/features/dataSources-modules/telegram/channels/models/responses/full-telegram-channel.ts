export interface IFullTelegramChannel{
    id:number,
    isActive:boolean,
    
    photoId:number,
    
    title:string,
    participantsCount:number,

    telegramMessagesCount:number,
    telegramRepliesCount:number,
    
    loadedDate:Date,
    date:Date,

    isLoaded:boolean,

    about:string,
    username:string,

    isSelectAvailable:boolean,
    isSelected:boolean
}