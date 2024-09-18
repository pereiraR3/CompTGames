import React from 'react';

const ExperienceSection = () => {
  return (
    <section className="ml-4 md:ml-0 grid grid-cols-1 md:grid-cols-1 lg:grid-cols-2 gap-8 my-12 px-4">
      
      {/* Imagens que serão ocultadas em telas pequenas e tablets */}
      <div className="hidden lg:flex flex-col md:flex-row justify-center items-center gap-8 mt-8">
        
        <img 
          src="src/assets/foto01.png" 
          alt="Mobile Search" 
          className="w-32 h-auto md:w-64 shadow-lg transform md:-translate-y-20"
        />

        <img 
          src="src/assets/foto02.png" 
          alt="Mobile Search" 
          className="w-32 h-auto md:w-64 shadow-lg transform md:translate-y-10"
        />

      </div>

      {/* Texto e botão responsivos, centralizado em telas md */}

      <div className="flex flex-col justify-center items-center md:items-center lg:items-start md:w-full lg:w-1/2 px-4">
        
        <h2
          className="text-2xl sm:text-3xl md:text-4xl font-bold text-center lg:text-left text-transparent bg-clip-text bg-gradient-to-r from-[#0BF587] to-[#FFFFFF]"
          style={{ fontFamily: "'Orbitron', sans-serif" }}
        >
          New experience when looking for the best tool
        </h2>

        <p className="text-gray-400 mt-4 text-sm sm:text-base text-center lg:text-left">
          Become an expert in minutes and connect with the technologies that are shaping the next era. Don’t waste time, the future of innovation awaits you!
        </p>

        <button
          className="mt-4 px-4 py-2 rounded-md w-full sm:w-40 md:w-32 text-white transition-all duration-300 ease-in-out"
          style={{
            backgroundImage: 'linear-gradient(90deg, #0BF587 0%, #5AC64C 100%, #38F100 100%)',
          }}
          onMouseEnter={(e) => e.currentTarget.style.backgroundImage = 'linear-gradient(90deg, #38F100 0%, #0BF587 100%)'}
          onMouseLeave={(e) => e.currentTarget.style.backgroundImage = 'linear-gradient(90deg, #0BF587 0%, #5AC64C 100%, #38F100 100%)'}
        >
          Search Now
        </button>
        
      </div>
    </section>
  );
};

export default ExperienceSection;
