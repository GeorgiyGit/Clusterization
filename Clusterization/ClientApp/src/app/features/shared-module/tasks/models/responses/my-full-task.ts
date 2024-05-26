import {IMyTask} from "./my-task";
export interface IMyFullTask extends IMyTask{
    description:string,
    customerId:string,
    
    fastClusteringWorkflowId:number | undefined,

    clusterizationProfileId:number | undefined,
    workspaceId:number | undefined,

    youtubeChannelId:number | undefined,
    youtubeVideoId:number | undefined,

    telegramChannelId:number | undefined,
    telegramMessageId:number | undefined,
    externalObjectsPackId:number | undefined
}