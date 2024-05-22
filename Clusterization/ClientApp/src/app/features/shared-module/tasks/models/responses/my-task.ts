export interface IMyTask{
    id:string,
    title:string,

    startTime:Date,
    endTime:Date,

    percent:number,
    isPercents:boolean,
    
    stateName:string,
    stateId:string
}