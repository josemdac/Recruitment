export interface HrRcrtApplicantDTO {
    
    [k: string]: any;
    applicantId: number|null;
    requestId: number;
    requestDate: string| Date;
    userName: string;
    firstName: string;
    lastName1: string;
    lastName2: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zipcode: string;
    mondayAm: string;
    mondayPm: string;
    tuesdayAm: string;
    tuesdayPm: string;
    wednesdayAm: string;
    wednesdayPm: string;
    thursdayAm: string;
    thursdayPm: string;
    fridayAm: string;
    fridayPm: string;
    saturdayAm: string;
    saturdayPm: string;
    sundayAm: string;
    sundayPm: string;
    telephone: string;
    ofccpDisability: string;
    ofccpGender: string;
    ofccpVeteran: string;
    ofccpEthnicity: string;
    applicantStatus: number | null;
    mobileTelephone: string;
    socialNetworkAddress1: string;
    socialNetworkAddress2: string;
    socialNetworkAddress3: string;
}

export const createBlankApplicant = ()=>{
    const ap:HrRcrtApplicantDTO = {
        applicantId: null,
        requestId: 0,
        requestDate: new Date,
        userName: "",
        firstName: "",
        lastName1: "",
        lastName2: "",
        address1: "",
        address2: "",
        city: "",
        state: "PR",
        zipcode: "",
        telephone: "",
        ofccpDisability: "N",
        ofccpGender: "N",
        ofccpVeteran: "N",
        ofccpEthnicity: "N",
        applicantStatus: null,
        mobileTelephone: "",
        socialNetworkAddress1: "",
        socialNetworkAddress2: "",
        socialNetworkAddress3: "",
        address1Street: "",
        address2Street: "",
        cityStreet: "",
        stateStreet: "PR",
        zipcodePh: "",
        mondayAm: "",
        mondayPm: "",
        tuesdayAm: "",
        tuesdayPm: "",
        wednesdayAm: "",
        wednesdayPm: "",
        thursdayAm: "",
        thursdayPm: "",
        fridayAm: "",
        fridayPm: "",
        saturdayAm: "",
        saturdayPm: "",
        sundayAm: "",
        sundayPm: ""
    }

    return ap
}

export interface HrRcrtApplicantListItem{
    applicantId?: number,
    requestDate: string | Date,
    applicationStatus?: {
        statusId: number, 
        description: string
    },
    position?: {
        positionDescription: string,
        requestId: string,
        requestStatus?: HrRcrtRequestStatusMaster,
    }

}


export interface HrRcrtRequestStatusMaster {
    typeCode: string;
    spanishDescription: string;
    englishDescription: string;
}