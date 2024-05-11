using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using Inventory_Manager.commands;
using Inventory_Manager.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeBot.Config;

namespace Inventory_Manager
{
    internal class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Command { get; set; }

        static async Task Main(string[] args)
        {
            JSONReader jsonReader = new JSONReader();
            await jsonReader.ReadJSON();
            InventoryFileManager.filePath = jsonReader.inventoryPath;

            DiscordConfiguration discordConfiguration = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfiguration);

            Client.Ready += Client_Ready;

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Command = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfig = Client.UseSlashCommands();

            slashCommandsConfig.RegisterCommands<AllCommands>();
            slashCommandsConfig.RegisterCommands<AllCommands>(1224448476918710292);
            slashCommandsConfig.RegisterCommands<AllCommands>(1211480999498752030);

            await Client.ConnectAsync();
            await Task.Delay(-1);

        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
