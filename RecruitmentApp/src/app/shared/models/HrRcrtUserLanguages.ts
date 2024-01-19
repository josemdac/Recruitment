export interface HrRcrtUserLanguageListItemDTO {
    recordId: number;
    languageId: number;
    speakingProficiency: string;
    readingProficiency: string;
    writingProficiency: string;
}

export const createBlankHrLang = ()=>{
    const l:HrRcrtUserLanguageListItemDTO = {
        recordId: 0,
        languageId: 0,
        speakingProficiency: "",
        readingProficiency: "",
        writingProficiency: ""
    }

    return l;
}