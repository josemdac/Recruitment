export interface HrRcrtRequestQuestion{
    question: string,
    questionType: 'S'|'M'|'T'|'B'
    questionId: number;
    choices: HrRcrtRequestQuestChoice[]
    answers: HrRcrtRequestQuestnAnswer[]|string
}

export interface HrRcrtRequestQuestChoice{
    choice:string, choiceId: number
}

export interface HrRcrtRequestQuestnAnswer{
    answer:string, answerId: number
}