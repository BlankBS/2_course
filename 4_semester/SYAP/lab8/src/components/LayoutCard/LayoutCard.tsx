import React from 'react';
import type {ReactNode} from 'react';
import styles from './LayoutCard.module.css';

interface LayoutCardProps {
    title: ReactNode;
    children: ReactNode;
    footer?: ReactNode;
}

export const LayoutCard: React.FC<LayoutCardProps> = ({ title, children, footer }) => {
    return (
        <div className={styles.card}>
            <header className={styles.header}>{title}</header>
            <main className={styles.content}>{children}</main>
            {footer && <footer className={styles.footer}>{footer}</footer>}
        </div>
    );
};