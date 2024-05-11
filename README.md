# Inventory-Manager-Discord-Bot
![image](https://github.com/rarefushion/Inventory-Manager-Discord-Bot/assets/13991987/83b93f9e-7330-4c79-b882-e1119be66c03)
![image](https://github.com/rarefushion/Inventory-Manager-Discord-Bot/assets/13991987/088f3e6a-ec9c-4efa-93e2-116a4d225cb8)

You'll need to look up a video on how to make a discord bot. This is just the code part. <br />
How to use: <br />
Create a console .net app with VS <br />
Add these files to the project (copy them over > in solution explorer show all files > select the three folders, right click iclude in this project) <br />
Add the required packages (Tools > NuGet Package Manager > Manage NuGet Packages for Solution > Browse) <br />
  1.DSharpPlus <br />
  2.DSharpPlus.CommandsNext <br />
  3.DSharpPlus.SlashCommands <br />
Create config.json in bin > Debug that looks like this: <br />
{ <br />
&emsp;"token": "{Create a discord bot and add their token here. You prob need to look up a video on it}", <br />
&emsp;"prefix": "/", <br />
&emsp;"inventorypath": "{a local path to save user files(the inventories) so like: "C:/Discord Inventories"}" <br />
} <br />
Replace the {} with what the request. <br />
Feel free to dm if you need help! twitter @rarefushion or whatever <br />
<br />
How to use within discord: <br />
there will be built in commands. use / to display them. should be self explanatory, you have: <br />
/add {quantity} {itemName} and optional {path} for orginizing different inventories <br />
you also have /listitems {@user} <br />
if these don't show up when typing / something went wrong and the bot wont work. You have to use the fillable fields. <br />
