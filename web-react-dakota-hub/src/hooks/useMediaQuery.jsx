import { useState, useEffect } from 'react';

// Hook personalizado que recebe uma media query (query) como argumento
const useMediaQuery = (query) => {
  // Estado que guarda se a media query é ou não atendida (inicialmente false)
  const [matches, setMatches] = useState(false);

  // O useEffect será executado quando o componente montar ou quando a query mudar
  useEffect(() => {
    // Cria um objeto MediaQueryList baseado na media query fornecida
    const mediaQuery = window.matchMedia(query);

    // Define o estado inicial baseado na correspondência da query
    setMatches(mediaQuery.matches);

    // Função handler que atualiza o estado sempre que a media query mudar
    const handler = (event) => setMatches(event.matches);

    // Adiciona um listener para o evento 'change' que dispara quando a condição da media query muda
    mediaQuery.addEventListener('change', handler);

    // Função de limpeza: remove o listener do evento 'change' quando o componente desmonta ou a query muda
    return () => mediaQuery.removeEventListener('change', handler);

    // O efeito depende de 'query', ou seja, será reexecutado se a query mudar
  }, [query]);

  // Retorna o estado 'matches', que indica se a condição da media query foi atendida ou não
  return matches;
};

export default useMediaQuery;