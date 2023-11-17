using System.Text.Json.Serialization;

namespace Tamagotchi.Model
{
    public class PokemonSpeciesResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("next")]
        public string Next { get; set; }
        [JsonPropertyName("previous")]
        public string Previous { get; set; }
        [JsonPropertyName("results")]
        public List<PokemonResult> Results { get; set; }
    }
}
