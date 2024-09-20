import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const urlValidator : ValidatorFn = (control: AbstractControl):ValidationErrors|null =>{
  const urlPattern = /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/;
  let url = control.get('url');
  let absolute = false;

  try {
    new URL(url?.value);
    absolute = true;
  } catch {
    absolute = false;
  }

  if(!urlPattern.test(url?.value) || !absolute){
     return { urlValidatorError: true };
  }
  
 return null; 
}


