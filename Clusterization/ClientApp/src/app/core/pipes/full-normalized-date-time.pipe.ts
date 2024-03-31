import { Inject, LOCALE_ID, Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fullNormalizedDateTime'
})
export class FullNormalizedDateTimePipe implements PipeTransform {

  constructor(@Inject(LOCALE_ID) public activeLocale: string){
  }

  transform(date:Date): string {
    date = new Date(Date.UTC(
      Number(date.toString().substring(0, 4)),     // Year
      Number(date.toString().substring(5, 7)) - 1, // Month (0-11)
      Number(date.toString().substring(8, 10)),    // Day
      Number(date.toString().substring(11, 13)),   // Hours
      Number(date.toString().substring(14, 16)),   // Minutes
      Number(date.toString().substring(17, 19))    // Seconds
    ));

    return date.toLocaleDateString(this.activeLocale,{ dateStyle: 'long' });
  }
}
