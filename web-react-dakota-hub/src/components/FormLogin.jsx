import React from 'react';
import FieldInput from './FieldInput';

export default function FormLogin() {
    return (
        <div className="w-full min-h-screen flex items-center justify-center mt-10">
            {/* Formulário destacado com várias camadas de sombra */}
            <div className="relative w-full max-w-md p-8 bg-white rounded-3xl shadow-lg hover:shadow-2xl transition-shadow duration-300 ease-in-out">
                <h2 className="text-gray-800 text-center text-2xl font-bold">Sign in</h2>

                <form className="space-y-6 mt-6">
                    <FieldInput maxLength={20} type="simple" />
                    <FieldInput maxLength={10} type="password" />
                    
                    <div className="flex flex-wrap items-center justify-between gap-4">
                        <div className="flex items-center">
                            <input 
                                id="remember-me" 
                                name="remember-me" 
                                type="checkbox" 
                                className="h-4 w-4 shrink-0 text-[#1cb679] focus:ring-[#1cb679] border-gray-300 rounded" 
                            />
                            <label htmlFor="remember-me" className="ml-3 block text-sm text-gray-800">
                                Remember me
                            </label>
                        </div>
                        <div className="text-sm">
                            <a href="javascript:void(0);" className="text-[#1cb679] hover:underline font-semibold">
                                Forgot your password?
                            </a>
                        </div>
                    </div>

                    <div className="mt-8">
                        <button 
                            type="button" 
                            className="w-full py-3 px-4 text-sm tracking-wide rounded-lg text-white bg-[#1cb679] hover:bg-[#1bc885] focus:outline-none"
                        >
                            Sign in
                        </button>
                    </div>

                    <p className="text-gray-800 text-sm mt-8 text-center">
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
