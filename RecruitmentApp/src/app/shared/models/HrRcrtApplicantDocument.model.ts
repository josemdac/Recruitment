export interface HrRcrtApplicantDocumentListItemDTO {
    documentId: number;
    
    status: string;
    documentType: string;
    document?: any
    hasFile?: boolean
}


export const createBlankApplicantDoc = ()=>{
    const doc: HrRcrtApplicantDocumentListItemDTO = {
        documentId: 0,
        
        status: "",
        documentType: ""
    }

    return doc;
}