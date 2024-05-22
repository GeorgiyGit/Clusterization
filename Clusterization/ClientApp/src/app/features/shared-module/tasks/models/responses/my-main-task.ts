import { IMyTask } from "./my-task";
import { IMySimpleSubTask } from "./my-simple-sub-task";

export interface IMyMainTask extends IMyTask {
    isGroupTask: boolean,
    subTasksCount: number,
    subTasks: IMySimpleSubTask[]
}