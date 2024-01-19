import {
    SelectEvent,
    RemoveEvent,
    FileRestrictions,
  } from "@progress/kendo-angular-upload";

export interface IFormInput{
    name: string, 
    type: string,
    label?: string,
    valid?: boolean,
    col?: any,
    min?: number, 
    max?: number, 
    maxlength?: number,
    source?: string|{ value: any, text: string}[],
    checkBoxValues?: [ {trueValue: any}, { falseValue: any}],
    radioOptions?: { value: any, label: string }[]
    noInlineCheckbox?: boolean,
    required?:boolean,
    fileRestrictions?: FileRestrictions,
    fileNameField?: string,
    startName?: string,
    endName?:string
    noSort?: boolean
    
}