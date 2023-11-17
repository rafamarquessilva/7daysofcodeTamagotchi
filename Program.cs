using Tamagotchi.Controller;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var controller = new TamagotchiController();
        await controller.InicializarPokemonApiServiceAsync();
        await controller.JogarAsync();
    }
}