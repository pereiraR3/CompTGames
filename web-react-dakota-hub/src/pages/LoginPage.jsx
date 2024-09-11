import React from 'react';
import FormLogin from '../components/FormLogin';
import Navbar from '../components/Navbar';
import gif from '../assets/login-animate.svg';

const LoginPage = () => {

    return (

            <div className="bg-[#1f1b2f] flex flex-col lg:flex-row justify-between items-center min-h-[60vh] p-4 bg-white"> 
                   
                <Navbar />
               
                <div className="hidden lg:flex w-full lg:w-1/1 flex justify-center items-center mt-20 ml-10">                
                    <img src={gif} alt="" className="w-full max-w-3xl h-auto mt-4" />
                </div>
               
                <FormLogin />
               
            </div>
               

    )

};

export default LoginPage;