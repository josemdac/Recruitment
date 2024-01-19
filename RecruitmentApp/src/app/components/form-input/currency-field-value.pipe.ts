import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyFieldValue'
})
export class CurrencyFieldValuePipe implements PipeTransform {

  transform(value: any): any{
    let val = parseFloat(value+'')
    return val.toFixed(2) 
  }

}
