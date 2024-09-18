import React from 'react';
import FieldInput from './FieldInput';
import { FaArrowLeft } from 'react-icons/fa'; // Importando um ícone

export default function FormLogin() {
    return (
        <div className="w-full min-h-screen flex items-center justify-center p-4">
            {/* Formulário destacado com várias camadas de sombra */}
            <div className="relative w-full max-w-sm sm:max-w-md p-6 sm:p-8 bg-white rounded-3xl shadow-lg hover:shadow-2xl transition-shadow duration-300 ease-in-out">
                
                {/* Link de retorno com ícone */}
                <a 
                    href="/" 
                    className="flex items-center text-[#1cb679] hover:text-[#1bc885] font-semibold transition-colors duration-300"
                >
                    <FaArrowLeft className="mr-2" />
                    Return
                </a>
                
                {/* Título */}
                <h2 className="text-gray-800 text-center text-xl sm:text-2xl font-bold">Sign in</h2>

                <form className="space-y-4 sm:space-y-6 mt-6">
                    {/* Inputs com menor espaçamento para mobile */}
                    <FieldInput maxLength={20} type="simple" />
                    <FieldInput maxLength={10} type="password" />
                    
                    {/* Checkbox e link de recuperação de senha */}
                    <div className="flex flex-wrap items-center justify-between gap-2 sm:gap-4">
                        <div className="flex items-center">
                            <input 
                                id="remember-me" 
                                name="remember-me" 
                                type="checkbox" 
                                className="h-4 w-4 text-[#1cb679] focus:ring-[#1cb679] border-gray-300 rounded" 
                            />
                            <label htmlFor="remember-me" className="ml-2 block text-sm text-gray-800">
                                Remember me
                            </label>
                        </div>
                        <div className="text-sm">
                            <a href="javascript:void(0);" className="text-[#1cb679] hover:underline font-semibold">
                                Forgot your password?
                            </a>
                        </div>
                    </div>

                    {/* Botão de login */}
                    <div className="mt-6 sm:mt-8">
                        <button 
                            type="button" 
                            className="w-full py-2 sm:py-3 px-4 text-sm sm:text-base tracking-wide rounded-lg text-white bg-[#1cb679] hover:bg-[#1bc885] focus:outline-none"
                        >
                            Sign in
                        </button>
                    </div>

                    {/* Link para registro */}
                    <p className="text-gray-800 text-sm mt-6 sm:mt-8 text-center">
                        Don't have an account? 
                        <a href="javascript:void(0);" className="text-[#1cb679] hover:underline ml-1 whitespace-nowrap font-semibold">
                            Register here
                        </a>
                    </p>
                </form>
            </div>
        </div>
    );
}
