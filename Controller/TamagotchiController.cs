using AutoMapper;
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
        private List<TamagotchiDto> mascotesAdotados { get; set; }

        IMapper mapper { get; set; }

        public TamagotchiController()
        {
            menu = new TamagotchiView();
            mascotesAdotados = new List<TamagotchiDto>();

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            mapper = config.CreateMapper();

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
                int escolha = menu.ObterEscolhaDoJogador(4);

                switch (escolha)
                {
                    case 1:
                        while (true)
                        {
                            menu.MostrarMenuDeAdocao();
                            escolha = menu.ObterEscolhaDoJogador(4);
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
                                        TamagotchiDto tamagotchi = mapper.Map<TamagotchiDto>(detalhes);

                                        mascotesAdotados.Add(tamagotchi);

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

                        if (mascotesAdotados.Count == 0)
                        {
                            Console.WriteLine("Você não tem nenhum mascote adotado.");
                            break;
                        }

                        menu.MostrarMascotesAdotados(mascotesAdotados);

                        Console.WriteLine("Escolha um mascote para interagir:");
                        int indiceMascote = menu.ObterEscolhaDoJogador(mascotesAdotados.Count) - 1;

                        var mascoteEscolhido = mascotesAdotados[indiceMascote];

                        int opcaoIteracao = 0;

                        while (opcaoIteracao != 4)
                        {

                            menu.MostrarMenuInteracao();
                            opcaoIteracao = menu.ObterEscolhaDoJogador(4);

                            switch (opcaoIteracao)
                            {
                                case 1:
                                    mascoteEscolhido.MostrarStatus(); ;
                                    break;
                                case 2:
                                    mascoteEscolhido.Alimentar();
                                    break;
                                case 3:
                                    mascoteEscolhido.Brincar();
                                    break;
                            }
                        }
                        break;
                    case 3:
                        menu.MostrarMascotesAdotados(mascotesAdotados);
                        break;
                    case 4:
                        Console.WriteLine("Obrigado por jogar! Até a próxima!");
                        return;
                }
            }
        }
    }
}
