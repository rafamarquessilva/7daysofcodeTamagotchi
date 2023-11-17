using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tamagotchi.Model
{
    public class PokemonDetailsResult
    {

        [JsonPropertyName("abilities")]
        public List<AbilityDetail> Abilities { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("order")]
        public int Order { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }

    }
    public class AbilityDetail
    {
        [JsonPropertyName("ability")]
        public Ability Ability { get; set; }
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
