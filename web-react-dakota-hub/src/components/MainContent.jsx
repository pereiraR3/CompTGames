import React from 'react';
import GlossarySection from './GlossarySection';
import ExperienceSection from './ExperienceSection';

const MainContent = () => {
  return (
    <main className="p-4 md:p-8 mt-14">
      {/* Section 1 and AI Glossary side by side */}
      <div className="flex flex-col md:flex-row items-center justify-center gap-x-8 my-8">
        {/* Left Section with Title and Description */}
        <section className="w-full md:w-1/2 lg:w-1/3 text-center md:text-left">
          <div>
            <h1
              className="text-4xl md:text-5xl lg:text-6xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-[#0BF587] to-[#FFFFFF]"
              style={{ fontFamily: "'Orbitron', sans-serif" }} // Aplicando a fonte Orbitron apenas aqui
            >
              Let’s <br />
              Simplifying your <br />
              Search for AI
            </h1>
            <p className="mt-4 text-gray-300 text-base md:text-lg lg:text-xl">
              With virtual technology, you can experience the digital world more realistically and enjoy gaming in a new style.
            </p>
          </div>
        </section>

        {/* Right Section: AI Glossary */}
        <div className="w-full md:w-1/2 lg:w-1/3 flex justify-center mt-8 md:mt-0">
          <GlossarySection />
        </div>
      </div>

      {/* Section 3: New Experience */}
      <ExperienceSection />
    </main>
  );
};

export default MainContent;
