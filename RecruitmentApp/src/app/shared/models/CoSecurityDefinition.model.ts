export interface CoSecurityDefinitionDTO {
    passwordLength: number ;
    changeEveryDays: number;
    canRepeatePassword: string;
    minLowerCharacters: number ;
    minUpperCharacters: number ;
    minNumericCharacters: number;
    minSymbolCharacters: number ;
    reqUpperLowerCharacters: string;
    passwordCanBeTheUsername: string;
}