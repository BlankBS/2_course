import React, { type InputHTMLAttributes } from 'react';
import cn from 'classnames';
import styles from './Input.module.css';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
    label: string;
    error?: string;
    isFullWidth?: boolean;
}

export const Input: React.FC<InputProps> = ({
    label,
    error,
    isFullWidth = false,
    className,
    ...props
}) => {
    return (
        <div className={cn(styles.wrapper, { [styles.fullWidth]: isFullWidth })}>
            <label className={styles.label}>{label}</label>
            <input
                className={cn(styles.input, { [styles.errorInput]: !!error }, className)}
                {...props} // Здесь передаются value и onChange для контролируемости
            />
            {error && <span className={styles.errorMessage}>{error}</span>}
        </div>
    );
};