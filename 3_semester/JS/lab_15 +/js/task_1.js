document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('validateForm');

    initFieldFocusEffects();
    
    form.addEventListener('submit', async function(event) {
        event.preventDefault();
        clearErrors();
        
        const validationResults = await Promise.all([
            validateSurname(),
            validateName(),
            validateEmail(),
            validatePhone(),
            validateCity(),
            validateUniversity(),
            validateCourse(),
            validateAbout()
        ]);
        
        const allValid = validationResults.every(result => result === true);
        
        if (allValid) {
            showSuccessNotification();
        } else {
            showErrorNotification();
        }
    });
    
    function showNotification(type, title, message, confirmCallback = null) {
        return new Promise((resolve) => {
            const container = document.getElementById('notificationContainer');
            const notification = document.createElement('div');
            notification.className = `notification ${type}`;
            
            let buttonsHTML = '';
            if (type === 'confirm') {
                buttonsHTML = `
                    <div class="notification-buttons">
                        <button class="notification-btn cancel" onclick="closeNotification(this, false)">Отмена</button>
                        <button class="notification-btn confirm" onclick="closeNotification(this, true)">Подтвердить</button>
                    </div>
                `;
            } else if (type === 'success') {
                buttonsHTML = `
                    <div class="notification-buttons">
                        <button class="notification-btn success" onclick="closeNotification(this, true)">OK</button>
                    </div>
                `;
            } else if (type === 'error') {
                buttonsHTML = `
                    <div class="notification-buttons">
                        <button class="notification-btn cancel" onclick="closeNotification(this, true)">Понятно</button>
                    </div>
                `;
            }
            
            notification.innerHTML = `
                <div class="notification-title">${title}</div>
                <div class="notification-message">${message}</div>
                ${buttonsHTML}
            `;
            
            container.appendChild(notification);
            
            setTimeout(() => {
                notification.classList.add('show');
            }, 100);
            
            notification._resolve = resolve;
        });
    }
    
    function showConfirm(title, message) {
        return showNotification('confirm', title, message);
    }
    
    function showSuccessNotification() {
        showNotification('success', 'Успех!', 'Форма заполнена корректно. Отправить данные?')
            .then((confirmed) => {
                if (confirmed) {
                    form.style.opacity = '0.7';
                    form.style.transform = 'scale(0.98)';
                    setTimeout(() => {
                        form.submit();
                    }, 300);
                }
            });
    }
    
    function showErrorNotification() {
        showNotification('error', 'Ошибка!', 'Пожалуйста, исправьте ошибки в форме перед отправкой.');
    }
    
    function clearErrors() {
        const errorMessages = document.querySelectorAll('.error-message');
        errorMessages.forEach(error => {
            error.textContent = '';
            error.style.display = 'none';
        });
        
        const inputs = document.querySelectorAll('input, textarea, select');
        inputs.forEach(input => {
            input.style.borderColor = '';
        });
        
        const containers = document.querySelectorAll('.checkContainer, .radioContainer');
        containers.forEach(container => {
            container.style.borderColor = '';
        });
    }
    
    function showError(fieldId, message) {
        const errorElement = document.getElementById(fieldId + '-error');
        const fieldElement = document.getElementById(fieldId);
        
        errorElement.textContent = message;
        errorElement.style.display = 'block';
        fieldElement.style.borderColor = 'red';
    }
    
    async function validateSurname() {
        const surname = document.getElementById('surname').value.trim();
        
        if (!surname) {
            showError('surname', 'Фамилия обязательна для заполнения');
            return false;
        }
        
        if (surname.length > 20) {
            showError('surname', 'Фамилия не должна превышать 20 символов');
            return false;
        }
        
        const nameRegex = /^[a-zA-Zа-яА-ЯёЁ]+$/;
        if (!nameRegex.test(surname)) {
            showError('surname', 'Фамилия должна содержать только символы русского и английского алфавита');
            return false;
        }
        
        return true;
    }
    
    async function validateName() {
        const name = document.getElementById('name').value.trim();
        
        if (!name) {
            showError('name', 'Имя обязательно для заполнения');
            return false;
        }
        
        if (name.length > 20) {
            showError('name', 'Имя не должно превышать 20 символов');
            return false;
        }
        
        const nameRegex = /^[a-zA-Zа-яА-ЯёЁ]+$/;
        if (!nameRegex.test(name)) {
            showError('name', 'Имя должно содержать только символы русского и английского алфавита');
            return false;
        }
        
        return true;
    }
    
    async function validateEmail() {
        const email = document.getElementById('email').value.trim();
        
        if (!email) {
            showError('email', 'E-mail обязателен для заполнения');
            return false;
        }
        
        if (/\s/.test(email)) {
            showError('email', 'E-mail не должен содержать пробелы');
            return false;
        }
        
        const emailRegex = /^[^\s@]+@[a-zA-Z]{2,6}\.[a-zA-Z]{2,3}$/;
        if (!emailRegex.test(email)) {
            showError('email', 'Неверный формат E-mail. Допустимый формат: @xxxxx.xxx');
            return false;
        }
        
        return true;
    }
    
    async function validatePhone() {
        const phone = document.getElementById('phone').value.trim();
        
        if (!phone) {
            showError('phone', 'Телефон обязателен для заполнения');
            return false;
        }
        
        const phoneCharRegex = /^[0-9()\-]+$/;
        if (!phoneCharRegex.test(phone)) {
            showError('phone', 'Телефон должен содержать только цифры и символы "(", ")", "-"');
            return false;
        }
        
        const phoneFormatRegex = /^\(0\d{2}\)\d{3}-\d{2}-\d{2}$/;
        if (!phoneFormatRegex.test(phone)) {
            showError('phone', 'Неверный формат телефона. Допустимый формат: (0xx)xxx-xx-xx');
            return false;
        }
        
        return true;
    }
    
    async function validateCity() {
        const city = document.getElementById('city').value;
        
        if (!city) {
            showError('city', 'Город обязателен для выбора');
            return false;
        }
        
        if (city !== 'Minsk') {
            try {
                const confirmed = await showConfirm(
                    'Подтверждение выбора города',
                    'Вы уверены, что выбрали правильный город? Рекомендуется выбрать "Минск".'
                );
                if (!confirmed) {
                    document.getElementById('city').style.borderColor = 'red';
                    return false;
                }
            } catch {
                return false;
            }
        }
        
        return true;
    }
    
    async function validateUniversity() {
        const university = document.getElementById('university');
        
        if (!university.checked) {
            try {
                const confirmed = await showConfirm(
                    'Подтверждение',
                    'Вы уверены, что не учитесь в БГТУ? Рекомендуется отметить этот пункт.'
                );
                if (!confirmed) {
                    university.parentElement.style.borderColor = 'red';
                    return false;
                }
            } catch {
                return false;
            }
        }
        
        return true;
    }
    
    async function validateCourse() {
        const courseRadios = document.getElementsByName('course');
        let courseSelected = false;
        let selectedValue = '';
        
        for (let radio of courseRadios) {
            if (radio.checked) {
                courseSelected = true;
                selectedValue = radio.value;
                break;
            }
        }
        
        if (!courseSelected) {
            showError('course', 'Выберите курс обучения');
            return false;
        }
        
        if (selectedValue !== '3') {
            try {
                const confirmed = await showConfirm(
                    'Подтверждение выбора курса',
                    `Вы уверены, что учитесь на ${selectedValue} курсе? Рекомендуется выбрать "3" курс.`
                );
                if (!confirmed) {
                    document.querySelector('.radioContainer').style.borderColor = 'red';
                    return false;
                }
            } catch {
                return false;
            }
        }
        
        return true;
    }
    
    async function validateAbout() {
        const about = document.getElementById('about').value.trim();
        
        if (!about) {
            showError('about', 'Поле "Расскажите о себе" обязательно для заполнения');
            return false;
        }
        
        if (about.length > 250) {
            showError('about', 'Поле "Расскажите о себе" не должно превышать 250 символов');
            return false;
        }
        
        return true;
    }
    
    window.closeNotification = function(button, result) {
        const notification = button.closest('.notification');
        notification.classList.remove('show');
        notification.classList.add('hide');
        
        setTimeout(() => {
            if (notification.parentNode) {
                notification.parentNode.removeChild(notification);
            }
        }, 400);
        
        if (notification._resolve) {
            notification._resolve(result);
        }
    };
});

function initFieldFocusEffects() {
    const form = document.getElementById('validateForm');
    const focusableElements = form.querySelectorAll('input, select, textarea');
    
    focusableElements.forEach(element => {
        element.addEventListener('focus', function() {
            form.classList.add('form-focused');
            
            this.classList.add('field-focused');
            
            if (this.type === 'checkbox' || this.type === 'radio') {
                const container = this.closest('.checkContainer, .radioContainer');
                if (container) {
                    container.style.borderColor = '#667eea';
                    container.style.background = '#f0f4ff';
                    container.style.transform = 'translateY(-2px)';
                }
            }
        });
        
        element.addEventListener('blur', function() {
            const hasActiveElement = form.querySelector('input:focus, select:focus, textarea:focus');
            if (!hasActiveElement) {
                form.classList.remove('form-focused');
            }
            
            this.classList.remove('field-focused');
            
            if (this.type === 'checkbox' || this.type === 'radio') {
                const container = this.closest('.checkContainer, .radioContainer');
                if (container && !container.querySelector('input:focus')) {
                    container.style.borderColor = '#e8eeff';
                    container.style.background = '#f8faff';
                    container.style.transform = 'translateY(0)';
                }
            }
        });
    });
    
    const labels = form.querySelectorAll('label');
    labels.forEach(label => {
        label.addEventListener('click', function() {
            const forAttr = this.getAttribute('for');
            if (forAttr) {
                const targetElement = document.getElementById(forAttr);
                if (targetElement) {
                    setTimeout(() => {
                        targetElement.focus();
                    }, 10);
                }
            }
        });
    });
}
