import React from 'react';  



const Button = ({type = 'button', onClick, children, bgColor, color}) => {
    return (
        <button type={type} 
        onClick={onClick} 
        style={{ 
            backgroundColor: bgColor, 
            color: color, 
            border: '1px solid black',
            borderRadius: '25px',
        }}
        >
            {children}
        </button>
    );
};

export default Button;