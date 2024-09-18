import React from 'react'
import MainContent from '../components/MainContent'
import Navbar from '../components/Navbar';

export default function HomePage(){

    return(
      <div className="min-h-screen bg-black text-white">
        <Navbar />
        <MainContent />
      </div>
    )


}