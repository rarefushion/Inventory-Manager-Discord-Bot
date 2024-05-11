using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;
using Inventory_Manager.Inventory;
using System.Collections.Generic;
using System.IO;

namespace Inventory_Manager.commands
{
    internal class AllCommands : ApplicationCommandModule
    {

        //[SlashCommand("Add", "Adds item to your inventory")]
        //public async Task Add(InteractionContext context, [Option("Quantity", "Number of this item to add")] long quan, [Option("ItemName", "Name for the item")] string itemName)
        //{
        //    await context.DeferAsync();
        //    User user = await InventoryFileManager.GetUser(context.Guild.Id, context.User.Username);
        //    User.Item added = InventoryManager.AddItem(context.Guild.Id, user, user.Inventorys, context.User.Username, itemName, quan);

        //    string totalText;
        //    if (added != null)
        //        totalText = "Total: " + added.Quantity.ToString();
        //    else
        //        totalText = "0 left, Item removed";
        //    DiscordEmbedBuilder content = new DiscordEmbedBuilder
        //    {
        //        Color = DiscordColor.Green,
        //        Title = $"Adding Item{((quan > 1) ? "s" : "")} to {context.Member.DisplayName}'s Inventory",
        //        Description = $"{quan} {itemName}{((quan > 1) ? "s" : "")} added {totalText}"
        //    };
        //    await context.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder().AddEmbed(content));
        //}

        [SlashCommand("Add", "Adds item to your inventory")]
        public async Task Add(InteractionContext context, [Option("Quantity", "Number of this item to add")] long quan, [Option("ItemName", "Name for the item")] string itemName, [Option("Inventory", "The inventory to place the item, use / for sub inventories")] string path = "")
        {
            await context.DeferAsync();
            User user = await InventoryFileManager.GetUser(context.Guild.Id, context.User.Username);
            User.Inventory inv = InventoryFileManager.GetInventory(user, path);
            User.Item added = InventoryManager.AddItem(context.Guild.Id, user, inv, context.User.Username, itemName, quan, path);

            string totalText;
            if (added != null)
                totalText = "Total: " + added.Quantity.ToString();
            else
                totalText = "0 left, Item removed";
            DiscordEmbedBuilder content = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = $"Adding Item{((quan > 1) ? "s" : "")} to {context.Member.DisplayName}/{path}",
                Description = $"{quan} {itemName}{((quan > 1) ? "s" : "")} added {totalText}"
            };
            await context.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder().AddEmbed(content));
        }

        [SlashCommand("ListItems", "List all items")]
        public async Task ListItems(InteractionContext context, [Option("User", "User to list")] DiscordUser disUser)
        {
            await context.DeferAsync();

            string title;
            if (context.Member.Id == disUser.Id)
                title = $"Listing {context.Member.DisplayName}'s inventory";
            else
                title = $"{context.Member.DisplayName} is listing {disUser.Username}'s inventory";

            User user = await InventoryFileManager.GetUser(context.Guild.Id, disUser.Username);
            string desc = "";
            User.Item item;
            for (int i = 0; i < user.Inventories.Items.Count; i++)
            {
                item = user.Inventories.Items[i];
                desc += $"{item.Quantity} {item.Name}{((item.Quantity > 1) ? "s" : "")} \n";
            }


            DiscordEmbedBuilder content = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = title,
                Description = desc
            };
            foreach (User.Inventory topInv in user.Inventories.Inventories)
            {
                string subInv = "";
                for (int i = 0; i < topInv.Items.Count; i++)
                {
                    item = topInv.Items[i];
                    subInv += $"{item.Quantity} {item.Name}{((item.Quantity > 1) ? "s" : "")} \n";
                }
                for (int i = 0; i < topInv.Inventories.Count; i++)
                    subInv += InventoryManager.ListItems(topInv.Inventories[i], true, "") + "\n";
                content.AddField(topInv.Name, subInv);
            }
            await context.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder().AddEmbed(content));
        }
    }
}
