import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

  export const matchpassword : ValidatorFn = (control: AbstractControl):ValidationErrors|null =>{

     let password = control.get('senha');
     let confirmpassword = control.get('reSenha');
     if(password && confirmpassword && password?.value != confirmpassword?.value){
        return {
            passwordmatcherror : true }
     }
    return null; 
   }