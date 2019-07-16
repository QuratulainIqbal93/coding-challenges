using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketKata
{
    class ItemDetails 
    {
        public string itemSKU;
        public int Price;
        public int Quantity;

        public ItemDetails(string i)
        {
            itemSKU = i;
            Price = 0;
            Quantity = 0;
        }
        public ItemDetails(string i, int p, int q)
        {
            itemSKU = i;
            Price = p;
            Quantity = q;

        }
        public ItemDetails(string i, int p)
        {
            itemSKU = i;
            Price = p;
            Quantity = 1;

        }

    }

    class Inventory
    {
       public  List<ItemDetails> items = new List<ItemDetails>();
       public  List<ItemDetails> itemsOnOffer = new List<ItemDetails>();


        public void getOffers(Inventory A)
        {

            Console.Write("Enter SKU for the item on offer : ");
            string i = Console.ReadLine();

            Console.Write("Enter quantity of the item on special offer : ");
            int q = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the special price for this item : ");
            int p = Convert.ToInt32(Console.ReadLine());

            A.itemsOnOffer.Add(new ItemDetails(i, p, q));

        }

        public void addItems(Inventory A)
        {

            Console.Write("Enter item SKU : ");
            string i = Console.ReadLine();

            Console.Write("Enter unit price : ");
            int p = Convert.ToInt32(Console.ReadLine());

            A.items.Add(new ItemDetails(i, p));

        }

    }
    class Checkout : ICheckout
    {
        public Inventory newStock = new Inventory();
        public static List<ItemDetails> scannedItems = new List<ItemDetails>();

        public void Scan(string item)
        {
            if(scannedItems.Count!=0 && scannedItems.Exists(i => i.itemSKU == item))
            {
                int scannedItemQuantity = scannedItems.Last(i => i.itemSKU == item).Quantity++;
               

                if (newStock.itemsOnOffer.Exists(i => i.itemSKU == item) && scannedItems.Last(i => i.itemSKU == item).Quantity == newStock.itemsOnOffer.First(i => i.itemSKU == item).Quantity)
                {

                    scannedItems.RemoveAt(scannedItems.IndexOf(scannedItems.Last(i => i.itemSKU == item)));
                    ItemDetails offerItem = new ItemDetails(item, newStock.itemsOnOffer.First(i => i.itemSKU == item).Price, scannedItemQuantity); 
                    scannedItems.Add(offerItem);

                    ItemDetails scanningItem = new ItemDetails(item);
                    scannedItems.Add(scanningItem);

                }
                else
                {
                    scannedItems.Last(i => i.itemSKU == item).Price = scannedItems.Last(i => i.itemSKU == item).Price + newStock.items.First(i => i.itemSKU == item).Price;

                }

            }
            else
            {
                if (newStock.items.Count != 0 && newStock.items.Exists(i => i.itemSKU == item))
                {
                    ItemDetails scanningItem = new ItemDetails(item,newStock.items.First(i => i.itemSKU == item).Price, 1 );
                    scannedItems.Add(scanningItem);
                }
                else
                {
                    Console.WriteLine("Item doesn't exist in Inventory! Please try again.");
                    itemsToScan();
                }  
                
            }
            
        }

        public int getTotalPrice()
        {
            int totalPrice = 0;

            if (scannedItems.Count != 0)
            {
                totalPrice = scannedItems.Sum(i => i.Price);
            }
            else
            {
                Console.WriteLine("No Items scanned!");
            }
            
            return totalPrice;
        }

        public void itemsToScan()
        {
            Console.Write("Enter item SKU to scan : ");
            string scanItem = Console.ReadLine();
            Scan(scanItem);

        }


    }
     interface ICheckout
    {
        void itemsToScan();
        void Scan(string item);
        int getTotalPrice();
        

    }
    class SuperMarket
    {
        static void Main(string[] args)
        {
            
            Checkout checkout1 = new Checkout();

            

            Console.WriteLine("Please add items to the Inventory: ");
            ConsoleKeyInfo keyInfo;

            do
            {
                checkout1.newStock.addItems(checkout1.newStock);
                Console.WriteLine("Press any key to add another item or press 'X' to finish.");
                keyInfo = Console.ReadKey();
            }
            while (keyInfo.Key != ConsoleKey.X);

            Console.WriteLine("\nPlease provide the details of items on Special Offer : ");
            do
            {
                checkout1.newStock.getOffers(checkout1.newStock);
                Console.WriteLine("Press any key to add another item or press 'X' to finish.");
                keyInfo = Console.ReadKey();
            }
            while (keyInfo.Key != ConsoleKey.X);

            Console.WriteLine("\nStarting checkout process... \n");
            do
            {
                checkout1.itemsToScan();
                Console.WriteLine("Press any key to scan next item or press 'X' to finish.");
                keyInfo = Console.ReadKey();
            }
            while (keyInfo.Key != ConsoleKey.X);

            Console.WriteLine("\nYour total price is : " + checkout1.getTotalPrice());
            
            Console.ReadKey();



        }
    }
}
