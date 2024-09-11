import React, { useState, useEffect } from 'react';
import { HiOutlineEye, HiOutlineEyeOff } from 'react-icons/hi'; // Importando ícone de password da Heroicons
import { HiOutlineUser } from 'react-icons/hi'; // Importando ícone de usuário da Heroicons

function constructFieldSimpleInput(value, setValue, maxLength) {
    return (
        <div className="relative flex items-center">
            <input
                type="text"
                value={value}
                onChange={e => setValue(e.target.value)}
                className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                maxLength={maxLength}
                placeholder="Enter your username"
            />
            <HiOutlineUser className="w-5 h-5 absolute right-4 text-gray-400" />
        </div>
    );
}

function constructFieldPasswordInput(value, setValue, maxLength) {
    const [showPassword, setShowPassword] = useState(false);

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    return (
        <div className="relative flex items-center">
            <input
                type={showPassword ? "text" : "password"}
                value={value}
                onChange={e => setValue(e.target.value)}
                className="w-full text-gray-800 text-sm border border-gray-300 px-4 py-3 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                maxLength={maxLength}
                placeholder="Enter your password"
            />
            <button
                type="button"
                onClick={togglePasswordVisibility}
                className="absolute right-4 text-gray-600 hover:text-gray-800 focus:outline-none"
            >
                {showPassword ? (
                    <HiOutlineEyeOff className="w-5 h-5" />
                ) : (
                    <HiOutlineEye className="w-5 h-5" />
                )}
            </button>
        </div>
    );
}

function selectField(type, value, setValue, maxLength) {
    switch (type) {
        case "simple":
            return constructFieldSimpleInput(value, setValue, maxLength);

        case "password":
            return constructFieldPasswordInput(value, setValue, maxLength);

        default:
            return null;
    }
}

export default function FieldInput({ maxLength, type }) {
    const [value, setValue] = useState("");
    const [lengthString, setLengthString] = useState(maxLength);

    useEffect(() => {
        setLengthString(maxLength - value.length);
    }, [value, maxLength]);

    return (
        <div className="flex flex-col">
            <label htmlFor="input" className="text-gray-800 text-sm mb-2 block">
                {type === "password" ? "Password:" : "Username:"}
            </label>
            {selectField(type, value, setValue, maxLength)}
            <span className="text-sm text-gray-500 mt-2">Remaining: {lengthString} characters</span>
        </div>
    );
}
