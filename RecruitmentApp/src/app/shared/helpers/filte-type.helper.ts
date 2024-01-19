import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import MediaTypesDict from './mime_application';
import ImageTypesDict from './mime_image';
import { Exts } from './extToMime';



export class RSMimeType {

   public static Application = MediaTypesDict;
   public static Image = ImageTypesDict;
   public static AnyImage = 'image/*';
   public static AnyApplication = 'application/*';

   static getApps() {
      return MediaTypesDict;
   }



}


export function getMimeType(filename:string){
   let ext = filename?filename.split('.').pop():undefined;

   if(ext){
      //@ts-ignore
      return Exts["."+ext.toLowerCase()];
   }

   return ""

}



