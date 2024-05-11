# Inventory-Manager-Discord-Bot
You'll need to look up a video on how to make a discord bot. This is just the code part.
How to use:
Create a console .net app with VS
Add these files to the project (copy them over > in solution explorer show all files > select the three folders, right click iclude in this project)
Add the required packages (Tools > NuGet Package Manager > Manage NuGet Packages for Solution > Browse)
  1.DSharpPlus
  2.DSharpPlus.CommandsNext
  3.DSharpPlus.SlashCommands
Create config.json in bin > Debug that looks like this:
{
	"token": "{Create a discord bot and add their token here. You prob need to look up a video on it}",
	"prefix": "/",
	"inventorypath": "{a local path to save user files(the inventories) so like: "C:/Discord Inventories"}"
}
Replace the {} with what the request.
Feel free to dm if you need help! twitter @rarefushion or whatever
