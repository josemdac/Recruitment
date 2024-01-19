export const parseCommentDesc = (txt:string)=>{
    return txt.split(' ').join('')
  }
  
  export const parseDbComment = (txt:string)=>{
  
  
    return txt.split(',').map(txt=>{ const [K,v] = txt.split('='); return { id: K.trim(), description: parseCommentDesc(v)} })
  }


  export function dataURLtoFile(dataurl:string, filename:string) {
 
    var arr = dataurl.split(','),
    //@ts-ignore
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), 
        n = bstr.length, 
        u8arr = new Uint8Array(n);
        
    while(n--){
        u8arr[n] = bstr.charCodeAt(n);
    }
    
    return new File([u8arr], filename, {type:mime});
  }

  export function toBase64(file: File) {
    return new Promise<string | ArrayBuffer>((resolve, reject) => {
      const reader = new FileReader();
  
      reader.readAsDataURL(file);
      //@ts-ignore
      reader.onload = () => resolve(reader.result);
      reader.onerror = error => reject(error);
    })
  }


  export const goToTop = ()=>{
    window.scrollTo({ top: 0})
  }


  export function changeFavicon(dataUri: string) {
  
    const link = document.createElement('link');
    const oldLinks = document.querySelectorAll('link[rel="icon"]');
    const oldLinks2 = document.querySelectorAll('link[rel="shortcut icon"]');
    oldLinks.forEach(e => {
      if(e && e.parentNode){
        e.parentNode.removeChild(e)
      }
    });

    oldLinks2.forEach(e => {
      if(e && e.parentNode){
        e.parentNode.removeChild(e)
      }
    });
    
    link.id = 'dynamic-favicon';
    link.rel = 'shortcut icon';
    link.type = 'image/png'
    link.href = dataUri;
    //console.log(link)
    document.head.appendChild(link);
  }