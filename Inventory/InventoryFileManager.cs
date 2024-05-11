using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inventory_Manager.Inventory
{
    internal class InventoryFileManager
    {
        public static string filePath;

        public static User.Inventory GetInventory(User user, string path)
        {
            //Find sub Inventory
            User.Inventory inv = user.Inventories;
            string nextName;
            int FSCount = 0;
            if (path.Length != 0)
            {
                while (path.Contains("/") && FSCount++ < 100)
                {
                    nextName = path.Substring(0, path.IndexOf("/"));
                    path = path.Remove(0, path.IndexOf("/", 1) + 1);
                    inv = GetInventory(inv, nextName);
                }
                inv = GetInventory(inv, path);
            }
            return inv;
        }

        private static User.Inventory GetInventory(User.Inventory inv, string name)
        {
            //Indexing item if present
            List<string> invNames = new List<string>();
            int index = -1;
            for (int i = 0; i < inv.Inventories.Count; i++)
            {
                invNames.Add(inv.Inventories[i].Name);
                if (inv.Inventories[i].Name == name)
                    index = i;
            }
            if (index == -1)
                inv.Inventories.Add(new User.Inventory(name));

            for (int i = 0; i < inv.Inventories.Count; i++)
                if (inv.Inventories[i].Name == name)
                    return inv.Inventories[i];
            Console.WriteLine("Inv wasn't made on InventoryFileManager.GetInventory(inv, name)");
            return null;
        }

        public static async Task<User> GetUser(ulong serverID, string userID)
        {
            string filePath = InventoryFileManager.filePath + $"/{serverID}/" + userID + ".txt";
            if (!File.Exists(filePath))
                return new User(userID);
            using (StreamReader sr = new StreamReader(filePath, new UTF8Encoding(false)))
            {
                string json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<User>(json);
            }
        }

        public static void Write(ulong serverID, User user)
        {
            Directory.CreateDirectory(filePath + $"/{serverID}/");
            using (StreamWriter sw = new StreamWriter(filePath + $"/{serverID}/" + user.ID + ".txt"))
            {
                sw.Write(JsonConvert.SerializeObject(user, Formatting.Indented));
            }
        }
    }
}
