import { Inject, LOCALE_ID, Pipe, PipeTransform } from '@angular/core';
import { differenceInMilliseconds, formatDistance } from 'date-fns';
@Pipe({
  name: 'timeDifference'
})
export class TimeDifferencePipe implements PipeTransform {

  transform(startDate:Date,endDate:Date): unknown {
    // Calculate time difference in milliseconds
    const difference = differenceInMilliseconds(startDate, endDate);

    // Format the time difference
    return formatDistance(startDate, endDate);
  }
}
