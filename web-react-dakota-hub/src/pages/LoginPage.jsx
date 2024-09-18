import React from 'react';
import FormLogin from '../components/FormLogin';
import gif from '../assets/login-animate.svg';

const LoginPage = () => {
    return (
        <div className="bg-gradient-to-r from-black via-[#3A3454] to-[#6E00A0] flex flex-col lg:flex-row justify-between items-center min-h-[60vh]">
            
            {/* Div com o gradiente */}

            <div className="lg:flex w-full lg:w-1/1 flex justify-center items-center mt-6">                
                <img src={gif} alt="" className="w-full max-w-3xl h-auto" />
            </div>
            
            <FormLogin />
            
        </div>
    );
};

export default LoginPage;
