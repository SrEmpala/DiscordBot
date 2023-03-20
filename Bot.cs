using BotConsole.Commands;
using DiscordBot;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BotConsole
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }


        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += OnClientReady;
            Client.ComponentInteractionCreated += ButtonResponse;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<CommandBot>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        public Frases updateMessage = new Frases();
        private async Task ButtonResponse(DiscordClient sender, ComponentInteractionCreateEventArgs e)
        {


            if (e.Interaction.Data.CustomId == "1")
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("Nova playlist:").WithDescription($"{updateMessage.RandomPlaylist()}")
                    .WithColor(DiscordColor.HotPink)
                    ));
            }
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
