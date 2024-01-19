export interface HrRcrtUserEducationListItemDTO {
    educationId: number;
    schoolName: string;
    yearsCompleted: number | null;
    graduated: string;
    graduatedYear: number | null;
    degreeId: number | null;
    major: string;
    gpa: number | null;
    countryId: number | null;
    comments: string;
    countryName?: string,
    degree?: string
}

export const createBlankHrEduc = ()=>{

    const educ : HrRcrtUserEducationListItemDTO = {
        educationId: 0,
        schoolName: "",
        yearsCompleted: null,
        graduated: "",
        graduatedYear: null,
        degreeId: null,
        major: "",
        gpa: 0,
        countryId: null,
        comments: "",
        
    }

    return educ
}