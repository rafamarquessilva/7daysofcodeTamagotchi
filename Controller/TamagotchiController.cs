using Tamagotchi.Model;
using Tamagotchi.Service;
using Tamagotchi.View;

namespace Tamagotchi.Controller
{
    public class TamagotchiController
    {

        private TamagotchiView menu { get; }
        private PokemonApiService pokemonApiService { get; set; }
        private List<PokemonResult> especiesDisponiveis { get; set; }
        private List<PokemonDetailsResult> mascotesAdotados { get; }


        public TamagotchiController()
        {
            menu = new TamagotchiView();
            mascotesAdotados = new List<PokemonDetailsResult>();
        }

        public async Task InicializarPokemonApiServiceAsync()
        {
            pokemonApiService = new PokemonApiService();
            especiesDisponiveis = await pokemonApiService.ObterEspeciesDisponiveisAsync();
        }

        public async Task JogarAsync()
        {

            menu.MostrarMensagemDeBoasVindas();

            while (true)
            {
                menu.MostrarMenuPrincipal();
                int escolha = menu.ObterEscolhaDoJogador();

                switch (escolha)
                {
                    case 1:
                        while (true)
                        {
                            menu.MostrarMenuDeAdocao();
                            escolha = menu.ObterEscolhaDoJogador();
                            switch (escolha)
                            {
                                case 1:
                                    menu.MostrarEspeciesDisponiveis(especiesDisponiveis);
                                    break;
                                case 2:
                                    menu.MostrarEspeciesDisponiveis(especiesDisponiveis);
                                    int indiceEspecie = menu.ObterEspecieEscolhida(especiesDisponiveis);
                                    PokemonDetailsResult detalhes = await pokemonApiService.ObterDetalhesDaEspecieAsync(especiesDisponiveis[indiceEspecie]);
                                    menu.MostrarDetalhesDaEspecie(detalhes);
                                    break;
                                case 3:
                                    menu.MostrarEspeciesDisponiveis(especiesDisponiveis);
                                    indiceEspecie = menu.ObterEspecieEscolhida(especiesDisponiveis);
                                    detalhes = await pokemonApiService.ObterDetalhesDaEspecieAsync(especiesDisponiveis[indiceEspecie]);
                                    menu.MostrarDetalhesDaEspecie(detalhes);
                                    if (menu.ConfirmarAdocao())
                                    {
                                        mascotesAdotados.Add(detalhes);
                                        Console.WriteLine("Parabéns! Você adotou um " + detalhes.Name + "!");
                                        Console.WriteLine("──────────────");
                                        Console.WriteLine("────▄████▄────");
                                        Console.WriteLine("──▄████████▄──");
                                        Console.WriteLine("──██████████──");
                                        Console.WriteLine("──▀████████▀──");
                                        Console.WriteLine("─────▀██▀─────");
                                        Console.WriteLine("──────────────");
                                    }
                                    break;
                                case 4:
                                    break;
                            }
                            if (escolha == 4)
                            {
                                break;
                            }
                        }
                        break;
                    case 2:
                        menu.MostrarMascotesAdotados(mascotesAdotados);
                        break;
                    case 3:
                        Console.WriteLine("Obrigado por jogar! Até a próxima!");
                        return;
                }
            }
        }
    }
}
