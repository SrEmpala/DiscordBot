using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole.Commands
{
    public class CommandBot : BaseCommandModule
    {
        public Frases frase = new Frases();


        [Command("frase")]
        public async Task TestCommand(CommandContext ctx)
        {
            var messageToUser = new DiscordMessageBuilder().AddEmbed(
                new DiscordEmbedBuilder().WithTitle(frase.RandomMessage())
                .WithColor(DiscordColor.Red)
                );

            await ctx.Channel.SendMessageAsync(messageToUser);
        }

        [Command("ajuda")]
        public async Task Help(CommandContext ctx)
        {
            var helpMessage = new DiscordMessageBuilder().
                AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Comandos").WithDescription("!frase\n!playlist")
                .WithColor(DiscordColor.Red)
                );
            await ctx.Channel.SendMessageAsync(helpMessage);
        }

        [Command("playlist")]
        public async Task RandomPlaylist(CommandContext ctx)
        {
            DiscordButtonComponent playlistButton = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Danger, "1", "Nova Playlist");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("A playlist escolhida é:").WithDescription(frase.RandomPlaylist())
                .WithColor(DiscordColor.Red)
                ).AddComponents(playlistButton);

            await ctx.Channel.SendMessageAsync(message);
        }
    }
}
