export interface HrRcrtUserEmploymentHistListItemDTO {
    employmentId: number;
    positionTitle: string;
    companyName: string;
    supervisorName: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zipcode: string;
    telephone: string;
    jobDescription: string;
    startDate: Date | string | null;
    endDate: Date | string | null;
    startSalary: number | null;
    endSalary: number | null;
    currentJob: string;
    terminationReason: string;
    comments: string;
    dateRange?: any
}

export const createBlankEmpHist = ()=>{
    const hist: HrRcrtUserEmploymentHistListItemDTO = {
        employmentId: 0,
        positionTitle: "",
        companyName: "",
        supervisorName: "",
        address1: "",
        address2: "",
        city: "",
        state: "PR",
        zipcode: "",
        telephone: "",
        jobDescription: "",
        startDate: new Date(),
        endDate: null,
        dateRange: { start: new Date(), end: null},
        startSalary: 0,
        endSalary: 0,
        currentJob: "",
        terminationReason: "",
        comments: ""
    }

    return hist
}