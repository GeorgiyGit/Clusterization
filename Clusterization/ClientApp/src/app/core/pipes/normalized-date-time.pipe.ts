import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'normalizedDateTime'
})
export class NormalizedDateTimePipe implements PipeTransform {
  transform(date:Date): string {
    date = new Date(Date.UTC(
      Number(date.toString().substring(0, 4)),     // Year
      Number(date.toString().substring(5, 7)) - 1, // Month (0-11)
      Number(date.toString().substring(8, 10)),    // Day
      Number(date.toString().substring(11, 13)),   // Hours
      Number(date.toString().substring(14, 16)),   // Minutes
      Number(date.toString().substring(17, 19))    // Seconds
    ));

    return date.toLocaleDateString('uk-UA',{ dateStyle: 'long' });
  }

}
