import { SortDescriptor } from "@progress/kendo-data-query";

export interface HrRcrtPositionRequest {
    requestId: number;
    positionDescription: string;
    requestDate: string| Date| null;
   // postingType: string;
    companyProfile: HrRcrtPositionProfileTypeSimple;
    educationProfile: HrRcrtPositionProfileTypeSimple;
    expertiseProfile: HrRcrtPositionProfileTypeSimple;
    jobLevelProfile: HrRcrtPositionProfileTypeSimple;
    jobTypeProfile: HrRcrtPositionProfileTypeSimple;
    locationProfile: HrRcrtPositionProfileTypeSimple;
    languageProfile: HrRcrtPositionProfileTypeSimple;
    experienceProfile: HrRcrtPositionProfileTypeSimple;
    salaryProfile: HrRcrtPositionProfileTypeSimple;
    // internalPostingStart: string | null;
    // internalPostingEnd: string | null;
    // externalPostingStart: string | null;
    // externalPostingEnd: string | null;
    positionDetailsEnglish: string;
    positionDetailsSpanish: string;
    jobTypeEnglish: string,
    jobTypeSpanish: string,
    jobTypeProfileType: number,
    // businessUnitId: number | null;
    requestStatusId: number | null;
    positionsNeeded: number | null;
    
    requestStatus: HrRcrtRequestStatusMaster;
}



export interface HrRcrtPositionProfileTypeSimple {
    profileType: string;
    spanishDescription: string;
    englishDescription: string;
}
export interface HrRcrtRequestStatusMaster {
    statusId: number;
    companyId: number;
    typeCode: string;
    spanishDescription: string;
    englishDescription: string;
}


export interface HrRcrtPositionRequestListStateDTO {
    keyWord: string;
    location: string;
    skip?: number;
    take?: number;
    lang: string;
    jobTypes?: string[];
    sort?: SortDescriptor[];
}