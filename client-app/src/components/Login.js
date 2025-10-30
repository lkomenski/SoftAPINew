import React, { useState } from 'react';
import { validateEmail, validatePassword } from '../scripts';

function Login() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');

    async function handleSubmit(e) {
        console.log('e from button', e);
        // Prevent default form submission behavior
        e.preventDefault();
        console.log({ email: validateEmail(email), password: validatePassword(password) });

        // Clear message after submission
        setMessage('Login form submitted!');

        // Validate email and password
        if (!validateEmail(email)) {
            setMessage('Invalid email format.');
            return;
        }
        if (!validatePassword(password)) {
            setMessage('Invalid password format.');
            return;
        }
        try { const response = await fetch('http://localhost:5202/login', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password }),
            });

            console.log('response', response);
            if (response.ok) {
                const data = await response.json();
                setMessage('Login successful!');
            }

        } catch (error){
            setMessage('An error occurred during login.');
        }
    }

    return (
        // onSubmit event handler added to the form element
        <form onSubmit={handleSubmit}> 
            <h2>Login</h2>
            <div>
                <label htmlFor="email">Email:</label>
                <input 
                    type="email" 
                    id="email"
                    name="email" 
                    value={email}
                    onChange={(e) =>{
                        console.log('e from email', e);
                        setEmail(e.target.value)
                        }}
                    required />
            </div>
            <div>
                <label htmlFor="password">Password:</label>
                <input 
                    type="password" 
                    id="password" 
                    name="password" 
                    value={password}
                    onChange={(e) => {
                        console.log('e from password', e);
                        setPassword(e.target.value);
                    }}
                    required />
            </div>
            <button type="submit">Login</button>
            {message && <div>{message}</div>}
        </form>
    );
}

export default Login;