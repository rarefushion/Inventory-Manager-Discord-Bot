using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static Inventory_Manager.Inventory.User;

namespace Inventory_Manager.Inventory
{
    internal static class InventoryManager
    {
        public static User.Item AddItem(ulong guildID, User user, User.Inventory inv, string editorID, string itemName, long quan, string path)
        {
            User.Item toReturn = null;
            //Indexing item if present
            List<string> items = new List<string>();
            int index = -1;
            for (int i = 0; i < inv.Items.Count; i++)
            {
                items.Add(inv.Items[i].Name);
                if (inv.Items[i].Name == itemName)
                    index = i;
            }
            //Item already present
            if (index >= 0)
            {
                inv.Items[index].Quantity += quan;
                if (inv.Items[index].Quantity <= 0)
                    inv.Items.RemoveAt(index);
                toReturn = inv.Items[index];
            }
            else
            {
                toReturn = new User.Item { Name = itemName, Quantity = quan };
                inv.Items.Add(toReturn);
            }
            //Logging action
            user.AuditLog.Add($"{editorID}: {quan} {itemName} '{path}'");
            int FSCount = 0;
            while (user.AuditLog.Count > 50 && FSCount++ < 100)
                user.AuditLog.RemoveAt(0);
            InventoryFileManager.Write(guildID, user);

            return toReturn;
        }

        public static string ListItems(User.Inventory inv, bool readSub, string path)
        {
            path += "/" + inv.Name;
            string items = $"{path}:\n";
            User.Item item;
            for (int i = 0; i < inv.Items.Count; i++)
            {
                item = inv.Items[i];
                items += $"{item.Quantity} {item.Name}{((item.Quantity > 1) ? "s" : "")} \n";
            }
            if (readSub)
                for (int i = 0; i < inv.Inventories.Count; i++)
                    items += "\n" + ListItems(inv.Inventories[i], true, path);
            return items;
        }
    }
}
