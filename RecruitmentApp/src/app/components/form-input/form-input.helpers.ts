import { IFormInput } from "./form-input.model"

export const  validateFields = (fields:IFormInput[], model: any)=>{
    model['invalidFields'] = []
    const valid = fields.map(f=>{

      let isValid = true;
      let reason = ''
      if(f.required){
             isValid = isValid && !!model[f.name]
             if(!isValid){
               reason = 'Requried'
             }
      }

      if(f.maxlength && isText(f)){
          isValid = isValid && !model[f.name] || model[f.name] && (model[f.name]+'').length <= f.maxlength
          if(!isValid){
            reason = 'MaxLength'
          }
      }

      if(f.maxlength && f.type == 'textarea'){
        isValid =isValid && !model[f.name] || model[f.name] && (model[f.name]+'').length <= f.maxlength
        if(!isValid){
          reason = 'MaxLength'
        }
      }

      if(f.type == 'email'){
          isValid = isValid && validateEmail(f, model)
          if(!isValid){
            reason = 'Email'
          }
      }


      if(f.type == 'file' && f.fileRestrictions && f.fileRestrictions?.maxFileSize){
        let fileVal: File = model[f.name]
        //console.log(fileVal?.size, f.fileRestrictions.maxFileSize)
        isValid = isValid && ( !fileVal || (fileVal && fileVal.size <= f.fileRestrictions.maxFileSize))
        if(!isValid){
          reason = 'Invalid File'
        }
        
      }
      if(!isValid){
          model['invalidFields'].push(f.name)
          f.valid = false

          console.log('reason', reason)
      }

      
      
      return isValid
    })
    //console.log(valid)
    return !valid.some(v=>!v)
  }


const isText = (f:IFormInput)=>{
    return !f.type && (f.type != 'dropdown') && (f.type != 'textarea') && (f.type != 'checkbox')
}

export const validateEmail = (field: IFormInput, model:any)=>{
    let reg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
    return reg.test(model[field.name])
}