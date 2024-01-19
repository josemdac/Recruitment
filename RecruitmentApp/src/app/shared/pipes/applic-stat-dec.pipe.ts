import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'applicStatDec'
})
export class ApplicStatDecPipe implements PipeTransform {

  transform(value?: { description:string }): string {
    if(value){
      return value.description
    }

    return ''
  }

}
