import { Component, Input, OnInit } from '@angular/core';
import { IFormInput } from 'src/app/components/form-input/form-input.model';
import { HrRcrtRequestQuestChoice, HrRcrtRequestQuestion, HrRcrtRequestQuestnAnswer } from 'src/app/shared/models/HrRcrtRequestQuestion.model';

@Component({
  selector: 'app-ap-proc-question',
  templateUrl: './ap-proc-question.component.html',
  styleUrls: ['./ap-proc-question.component.scss']
})
export class ApProcQuestionComponent implements OnInit {

  @Input() question?: HrRcrtRequestQuestion
  constructor() { }

  ngOnInit(): void {
    this.fields = this.getFields()
  }

  fields: IFormInput[] = []
  get textField(){
    return this.fields[0]
  }
  get boolField(){
    return this.fields[1]
  }
  get sChoiceField(){
    return this.fields[2]
  }
  get mChoiceField(){
    return this.fields[3]
  }
  getMChoiceField(choice:HrRcrtRequestQuestChoice):IFormInput{
    if(this.mChoiceField){
      return { ...this.mChoiceField, label: choice.choice, name: choice.choiceId +'' }
    }

    return { label: choice.choice, 
      name: (choice.choiceId+''), type: 'checkbox', col: 12 }
  }

  getMChoiceValue(choice:HrRcrtRequestQuestChoice){
    let checked = false
    if(this.question?.answers && typeof this.question.answers == 'object'){
      checked = this.question.answers.some(a=>a.answer == choice.choice)
    }
    return checked?'Y':'N'
  }

  getFields():IFormInput[]{
    return [
      { name: 'text', col: 12, type: 'text', maxlength: 4000, label: this.question?.question},
      { name: 'bool', col: 12, type: 'switch', label: this.question?.question, checkBoxValues: [
        { trueValue: 'Y'}, { falseValue: 'N'}
      ] },
      { name: 'single-choice', col: 12, type: 'radio', label: this.question?.question, radioOptions: [
        ...this.question?(this.question.choices.map(
          c=>({ value: c.choice, label: c.choice})
        )):[]
      ]},
      { name: '', col: 12, type: 'checkbox', label: '', checkBoxValues: [
        { trueValue: 'Y'}, {falseValue: 'N'}
      ] }
    ]
  }

  changeHandler(value:any, field:IFormInput){
    if(this.question){
      if(this.question.questionType != 'M'){
        this.question.answers = value
      }else{
        let answers:HrRcrtRequestQuestnAnswer[] = [
          { answer: value, answerId: 0 }
        ]
        if(this.question.answers && 
          typeof this.question.answers == 'object'){
          answers = [ ...this.question.answers, ...answers]
        }
        this.question.answers = answers
      }
    }
  }

}
