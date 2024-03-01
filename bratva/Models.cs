using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bratva
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public List<Cart> Carts { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Item Item { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class ListViewItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Path { get; set; }
    }

    public class ListViewCartModel
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public string Path { get; set; }
    }
}
