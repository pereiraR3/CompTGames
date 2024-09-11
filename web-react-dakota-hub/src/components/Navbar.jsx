import React, { useState } from 'react'; 
import NavbarDesktop from './NavbarDesktop'; 
import NavbarMobile from './NavbarMobile'; 
import useMediaQuery from '../hooks/useMediaQuery'; // Hook personalizado para verificar a largura da tela

const Navbar = () => {
  const isDesktop = useMediaQuery('(min-width: 768px)');

  const [isOpen, setIsOpen] = useState(false);


  const navItems = [
    
    { id: 1, text: 'About' },

    { id: 2, text: 'Home' },

  ];

  const toggleMobileNav = () => {
    setIsOpen(!isOpen); 
  };

  return (
    <>
      {isDesktop ? (
        // Renderiza NavbarDesktop se a largura da tela for maior que 768px
        <NavbarDesktop navItems={navItems} />
      ) : (
        // Renderiza NavbarMobile se a largura da tela for menor que 768px
        <NavbarMobile
          navItems={navItems} // Passa os itens de navegação
          isOpen={isOpen} // Estado da visibilidade do menu móvel
          toggleMobileNav={toggleMobileNav} // Função para abrir/fechar o menu móvel
        />
      )}
    </>
  );
};

export default Navbar;