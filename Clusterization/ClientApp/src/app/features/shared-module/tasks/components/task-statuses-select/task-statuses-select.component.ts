import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';

@Component({
  selector: 'app-task-statuses-select',
  templateUrl: './task-statuses-select.component.html',
  styleUrl: './task-statuses-select.component.scss'
})
export class TaskStatusesSelectComponent {
  @Output() sendEvent = new EventEmitter<string>();

  @Input() isActive:boolean=true;

  tooltip:string=$localize`Статус завдання`;

  options: IOptionForSelectInput[] = [
    {
      value: undefined,
      description: $localize`Всі`
    },
    {
      value: 'Error',
      description: $localize`Помилка`
    },
    {
      value: 'Wait',
      description: $localize`Чекають виконання`
    },
    {
      value: 'Process',
      description: $localize`Виконуються`
    },
    {
      value: 'Completed',
      description: $localize`Виконані`
    },
    {
      value: 'Stopped',
      description: $localize`Зупинені`
    },
  ];

  select(option: IOptionForSelectInput) {
    this.sendEvent.emit(option?.value);
  }
}
