import { ITelegramReaction } from "../../../reactions/models/telegram-reaction";

export interface IFullTelegramMessage{
    id:number,

    date:Date,
    editDate:Date,

    message:string,
    postAuthor:string,

    views:number,

    loadedDate:Date,
    telegramRepliesCount:number,

    reactions:ITelegramReaction[]

}