using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sklep context = new Sklep();

            Operacje operacje = new Operacje(context);

            while (true)
            {

                Console.WriteLine("Dodaj produkt wybierz 1");
                Console.WriteLine("Wypisz wszystkie produkty 2");
                Console.WriteLine("Dodaj zamówienie 3");
                Console.WriteLine("wypisz ostanie zamowienie 4");
                var a = int.Parse(Console.ReadLine());
                if (a == 1)
                {
                    operacje.AddProduct();
                }
                else if (a == 2)
                {
                    operacje.ShowAllproducts();

                }
                else if (a==3)

                {
                    operacje.Add_Order();
                }
                else if (a == 4)
                {
                    operacje.Showorders();
                }

                        }

           
        }
    }
}
