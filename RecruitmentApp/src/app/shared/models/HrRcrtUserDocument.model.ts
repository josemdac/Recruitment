export interface HrRcrtUserDocumentListItemDTO {
    documentId: number;
    documentTitle: string;
    documentName: string;
    documentType: string;
    documentFormat: string;
    document?:any;
    status: string;
}

export const createBlankUserDoc = ()=>{
    const doc:HrRcrtUserDocumentListItemDTO = {
        documentId: 0,
        documentTitle: "",
        documentName: "",
        documentType: "",
        documentFormat: "",
        status: ""
    }

    return doc
}