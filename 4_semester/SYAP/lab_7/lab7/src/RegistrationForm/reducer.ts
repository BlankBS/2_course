import type { IFormState, TFormAction } from '../RegistrationForm/types';

export const initialState: IFormState = {
    currentStep: 1,
    formData: {
        email: '', password: '', username: '', city: '', occupation: '', agree: false 
    },
    errors: {},
    isSubmitting: false,
};

export function registrationReducer(state: IFormState, action: TFormAction): IFormState {
    switch (action.type) {
        case 'UPDATE_FIELD':
            return {
                ...state,
                formData: { ...state.formData, [action.field]: action.value },
                // Очищаем ошибку поля, когда пользователь начал в него что-то вводить
                errors: { ...state.errors, [action.field]: undefined }
            };
        case 'SET_ERROR':
            return {
                ...state,
                errors: { ...state.errors, [action.field]: action.message }
            };
        case 'NEXT_STEP':
            return { ...state, currentStep: state.currentStep + 1 };
        case 'PREV_STEP':
            return { ...state, currentStep: state.currentStep - 1 };
        case 'SUBMIT_START':
            return { ...state, isSubmitting: true };
        case 'SUBMIT_SUCCESS':
            return { ...state, isSubmitting: false };
        default:
            return state;
    }
}