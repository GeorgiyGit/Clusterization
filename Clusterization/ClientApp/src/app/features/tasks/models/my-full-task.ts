export interface IMyFullTask{
    id:number,

    title:string,
    description:string,

    startTime:Date,
    endTime:Date,

    percent:number,

    stateName:string,
    stateId:string,

    customerId:string
}