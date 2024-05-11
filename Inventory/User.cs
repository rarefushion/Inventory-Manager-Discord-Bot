using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Manager.Inventory
{
    internal sealed class User
    {
        public string ID { get; set; }
        public Inventory Inventories { get; set; }
        public List<string> AuditLog { get; set; }

        public User(string ID) 
        {
            this.ID = ID;
            Inventories = new Inventory("parent");
            AuditLog = new List<string>();
        }

        internal class Item
        {
            public string Name { get; set; }
            public long Quantity { get; set; }
        }

        internal sealed class Inventory
        {
            public string Name { get; set; }
            public List<Item> Items { get; set; }
            public List<Inventory> Inventories { get; set; }

            public Inventory(string name)
            {
                Name = name;
                Items = new List<Item>();
                Inventories = new List<Inventory>();
            }
        }
    }
}
