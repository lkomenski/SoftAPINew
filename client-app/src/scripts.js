function validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(String(email).toLowerCase());
}

function validatePassword(password) {
    // Password must be at least 8 characters long
    return password.length >= 8;
}

export { validateEmail, validatePassword };