import { ITelegramReply } from 'src/app/features/dataSources-modules/telegram/replies/models/telegram-reply';
import { ISimpleExternalObject } from './../../../dataSources-modules/external-data/external-objects/models/responses/simple-external-object';
import { IFullTelegramMessage } from 'src/app/features/dataSources-modules/telegram/messages/models/responses/full-telegram-message';
import { IYoutubeComment } from 'src/app/features/dataSources-modules/youtube/comments/models/youtube-comment';

export interface IFullDataObject{
    id:number,
    text:string,
    typeId:string,

    externalObject:ISimpleExternalObject,
    telegramReply:ITelegramReply,
    telegramMessage:IFullTelegramMessage,
    youtubeComment:IYoutubeComment,
}