using System.Text.Json;
using Tamagotchi.Model;

namespace Tamagotchi.Service
{
    public class PokemonApiService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<List<PokemonResult>> ObterEspeciesDisponiveisAsync()
        {
            // Obter a lista de espécies de Pokémons
            var resultado = await HttpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");

            if (resultado.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException($"{resultado.StatusCode}-{resultado.RequestMessage}");

            var retorno = await resultado.Content.ReadAsStringAsync();
            var pokemonEspeciesResposta = JsonSerializer.Deserialize<PokemonSpeciesResult>(retorno);

            return pokemonEspeciesResposta.Results;

            //string resposta = HttpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon/");
            //var pokemonEspeciesResposta = JsonSerializer.Deserialize<PokemonSpeciesResult>(resposta);

            //return pokemonEspeciesResposta.Results;
        }

        public async Task<PokemonDetailsResult> ObterDetalhesDaEspecieAsync(PokemonResult especie)
        {

            string respostaDetalhe = await HttpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{especie.Nome}");
            var pokemonDetalhes = JsonSerializer.Deserialize<PokemonDetailsResult>(respostaDetalhe);

            return pokemonDetalhes;
        }

    }
}
