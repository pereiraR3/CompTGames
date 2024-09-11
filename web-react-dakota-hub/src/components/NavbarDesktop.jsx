import React from 'react'; // Importa o React, necessário para criar componentes funcionais em React
import logo from '../assets/logo.png'; // Importa a imagem do logotipo, que será exibida na navbar

// Componente NavbarDesktop: Recebe os itens de navegação (navItems) como prop
const NavbarDesktop = ({ navItems }) => {

  // Função que rola suavemente a página até o formulário de busca
  const handleScrollToSearch = () => {
    const searchForm = document.getElementById(''); // Obtém o formulário de busca pelo ID
    if (searchForm) {
      searchForm.scrollIntoView({ behavior: 'smooth' }); // Rola suavemente até o formulário de busca
    }
  };

  // Renderiza o JSX que compõe a estrutura da navbar
  return (
    <nav className="bg-[#3a3454] shadow-md fixed top-0 left-0 w-full z-54">
      {/* Contêiner que centraliza o conteúdo da navbar */}
      <div className="container mx-auto flex justify-between items-center h-20 px-4">
        
        {/* Seção do logotipo e título */}
        <div className="flex items-center  from-green-400 to-blue-500 p-4 rounded-lg shadow-lg">
        <img src={logo} alt="gov.br" className="h-20 mr-4" /> {/* Exibe o logotipo com altura definida */}
        <h1 className="text-3xl font-extrabold text-white tracking-wide drop-shadow-lg">
            FIND YOUR AI HERE
        </h1> {/* Título ao lado do logotipo, estilizado */}
        </div>

        {/* Seção de navegação (itens de menu) */}
        <div className="flex items-center space-x-8">
          {/* Lista de itens de navegação mapeados a partir da prop navItems */}
          <ul className="flex space-x-4">
            {navItems.map((item) => (
              <li
                key={item.id} // Cada item de navegação tem uma chave única (necessário para renderização de listas no React)
                className="text-sm text-[#0bf587] cursor-pointer font-bold hover:text-[#0aae6c] transition-colors duration-300"
                onClick={item.action ? item.action : null} // Define a ação de clique se houver ação associada
              >
                {item.text} {/* Exibe o texto do item de navegação */}
              </li>
            ))}
          </ul>

          {/* Botão que, ao ser clicado, rola até o formulário de busca */}
          <button
            className="bg-[#0aae6c] text-white font-bold px-4 py-2 rounded transition-colors duration-300 ease-in-out hover:bg-[#0bf587] hover:text-black"
            onClick={handleScrollToSearch} // Aciona a função de rolar até o formulário de busca ao clicar
          >
            Do your search
          </button>
        </div>
      </div>
    </nav>
  );
};

export default NavbarDesktop; // Exporta o componente para ser usado em outras partes da aplicação