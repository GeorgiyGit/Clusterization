import { ITelegramReaction } from "../../reactions/models/telegram-reaction";

export interface ITelegramReply{
    id:number,
    date:Date,
    editDate:Date,
    groupedId:number,
    message:string,
    reactions:ITelegramReaction[],
    loadedDate:Date
}