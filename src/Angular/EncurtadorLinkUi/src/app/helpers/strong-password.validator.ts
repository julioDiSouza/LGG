import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { StrongPasswordRegx } from "./strong.password.regx";

  export const strongPassword : ValidatorFn = (control: AbstractControl):ValidationErrors|null =>{

     let password = control.get('senha');
     
     if(!StrongPasswordRegx.test(password?.value)){
        return { validPassWordError: true };
     }
    return null; 
   }