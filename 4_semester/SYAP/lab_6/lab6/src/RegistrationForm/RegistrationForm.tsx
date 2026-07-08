import React, { useReducer } from 'react';
import { RegistrationSchema, type IFormData } from './schema';
import { registrationReducer, initialState } from './reducer';

export const RegistrationForm = () => {
    const [state, dispatch] = useReducer(registrationReducer, initialState);

    const validateStep = () => {
        let fieldsToValidate: (keyof IFormData)[] = [];
        if (state.currentStep === 1) fieldsToValidate = ['email', 'password'];
        if (state.currentStep === 2) fieldsToValidate = ['username', 'city'];
        if (state.currentStep === 3) fieldsToValidate = ['occupation', 'agree'];

        // Выбираем из общей схемы только те поля, которые на текущем шаге
        const stepSchema = RegistrationSchema.pick(
            Object.fromEntries(fieldsToValidate.map(f => [f, true])) as any
        );

        const result = stepSchema.safeParse(state.formData);

        if (!result.success) {
            result.error.issues.forEach(issue => {
                dispatch({ 
                    type: 'SET_ERROR', 
                    field: issue.path[0] as keyof IFormData, 
                    message: issue.message 
                });
            });
            return false;
        }
        return true;
    };

    const handleNext = () => {
        if (validateStep()) dispatch({ type: 'NEXT_STEP' });
    };

    const handleSubmit = async () => {
        if (!validateStep()) return;
        dispatch({ type: 'SUBMIT_START' });
        
        await new Promise(res => setTimeout(res, 2000));
        
        console.log("Данные успешно отправлены:", state.formData);
        dispatch({ type: 'SUBMIT_SUCCESS' });
        alert("Регистрация завершена!");
    };

    return (
        <div style={{ padding: '20px', border: '1px solid #ccc', borderRadius: '8px', maxWidth: '400px' }}>
            <h2>Шаг {state.currentStep} из 3</h2>

            {state.currentStep === 1 && (
                <div>
                    <input placeholder="Email" value={state.formData.email} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'email', value: e.target.value})} />
                    <p style={{color: 'red'}}>{state.errors.email}</p>
                    <input type="password" placeholder="Пароль" value={state.formData.password} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'password', value: e.target.value})} />
                    <p style={{color: 'red'}}>{state.errors.password}</p>
                </div>
            )}

            {state.currentStep === 2 && (
                <div>
                    <input placeholder="Имя" value={state.formData.username} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'username', value: e.target.value})} />
                    <p style={{color: 'red'}}>{state.errors.username}</p>
                    <input placeholder="Город" value={state.formData.city} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'city', value: e.target.value})} />
                    <p style={{color: 'red'}}>{state.errors.city}</p>
                </div>
            )}

            {state.currentStep === 3 && (
                <div>
                    <select value={state.formData.occupation} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'occupation', value: e.target.value})}>
                        <option value="">Выберите профессию</option>
                        <option value="Dev">Разработчик</option>
                        <option value="Designer">Дизайнер</option>
                    </select>
                    <p style={{color: 'red'}}>{state.errors.occupation}</p>
                    <label>
                        <input type="checkbox" checked={state.formData.agree} onChange={e => dispatch({type: 'UPDATE_FIELD', field: 'agree', value: e.target.checked})} /> Согласен с правилами
                    </label>
                    <p style={{color: 'red'}}>{state.errors.agree}</p>
                </div>
            )}

            <div style={{ marginTop: '20px' }}>
                {state.currentStep > 1 && <button onClick={() => dispatch({type: 'PREV_STEP'})}>Назад</button>}
                {state.currentStep < 3 
                    ? <button onClick={handleNext}>Далее</button> 
                    : <button disabled={state.isSubmitting} onClick={handleSubmit}>{state.isSubmitting ? 'Загрузка...' : 'Зарегистрироваться'}</button>
                }
            </div>
        </div>
    );
};