import type { IFormData } from '../RegistrationForm/schema';

export interface IFormState {
    currentStep: number;
    formData: IFormData;
    errors: Partial<Record<keyof IFormData, string>>; // Ошибки (ключ - имя поля, значение - текст)
    isSubmitting: boolean;
}

export type TFormAction =
    | { type: 'UPDATE_FIELD'; field: keyof IFormData; value: any }
    | { type: 'SET_ERROR'; field: keyof IFormData; message: string | undefined }
    | { type: 'NEXT_STEP' }
    | { type: 'PREV_STEP' }
    | { type: 'SUBMIT_START' }
    | { type: 'SUBMIT_SUCCESS' };