export interface IMyTask{
    id:string,
    title:string,

    startTime:Date,
    endTime:Date,

    percent:number,
    
    stateName:string,
    stateId:string
}