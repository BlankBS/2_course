import React from "react";

function Button({title, onClick, disabled}){
    return (
        <button onClick={onClick} disabled={disabled}>
            {title}
        </button>
    );
}

export default Button;