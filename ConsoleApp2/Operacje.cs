using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    
    public  class Operacje
    {
        public readonly Sklep _context;
        public Operacje(Sklep context)
        {
            _context = context; 
        }


        public void  ShowAllproducts()
        {
            foreach (var a in _context.products.Include("brand").Include("brand").Include("category"))
            {
                Console.WriteLine($"{a.product_name}  {a.brand.brand_name} {a.category.category_name}  {a.model_year} {a.list_price}");
            }

        }
        public void AddProduct()
        {
            product product1 = new product();
            Console.WriteLine("Podaj nazwę produktu");
            product1.product_name = Console.ReadLine();
            Console.WriteLine("Wybierz markę");
            int i = 1;
            foreach (var a  in _context.brands.OrderBy(c=>c.brand_id))
            {
                Console.WriteLine(i+ a.brand_name);
                i++;
            }
            var wybrana_marka = Console.ReadLine();
           product1.brand =  _context.brands.OrderBy(c => c.brand_id).Skip(int.Parse(wybrana_marka)-1).FirstOrDefault();
           
            Console.WriteLine("Wybierz kategorie");
            i = 1;
            foreach (var a in _context.categories.OrderBy(c => c.category_id))
            {
                Console.WriteLine(i + a.category_name);
                i++;
            }
            var wybrana_categoria = Console.ReadLine();
            product1.category = _context.categories.OrderBy(c => c.category_id).Skip(int.Parse(wybrana_categoria) - 1).FirstOrDefault();

            Console.WriteLine("Rok producji");

        var rok =   Console.ReadLine();
            product1.model_year = short.Parse(rok);
            Console.WriteLine("Cena");

            var cenna = Console.ReadLine();
            product1.list_price = Decimal.Parse(cenna);


            _context.products.Add(product1);

            _context.SaveChanges();



        }


        public void Add_Order()
        {
            order order = new order();
            Console.WriteLine("Podaj swój email lub numer telefonu");
            var EmailOrPhone = Console.ReadLine();
            customer customer1 = null; 
             customer1 = _context.customers.SingleOrDefault(a => a.email == EmailOrPhone || a.phone == EmailOrPhone);
            
            if(customer1 == null)
            {
                customer1 = new customer();
                Console.WriteLine("Podaj  imie");
                customer1.first_name = Console.ReadLine();
                Console.WriteLine("Podaj email");
                customer1.email = Console.ReadLine();
                Console.WriteLine("Podaj numer telefonu");
                customer1.phone = Console.ReadLine();
                Console.WriteLine("Podaj kod pocztowy");
                customer1.zip_code = Console.ReadLine();

                _context.customers.Add(customer1);
             
            }
            order.customer=customer1;
            Console.WriteLine("Wybierz Sklep");
            int i = 1;
            foreach (var a in _context.stores.OrderBy(c => c.store_id))
            {
                Console.WriteLine(i + a.store_name);
                i++;
            }
            var wybrana_marka = Console.ReadLine();
            order.store = _context.stores.OrderBy(c => c.store_id).Skip(int.Parse(wybrana_marka) - 1).FirstOrDefault();

            Console.WriteLine("Wybierz Personel");
            i = 1;
            foreach (var a in _context.staffs.OrderBy(c => c.staff_id))
            {
                Console.WriteLine(i + a.first_name + " " + a.last_name);
                i++;
            }
             wybrana_marka = Console.ReadLine();
            order.staff = _context.staffs.OrderBy(c => c.staff_id).Skip(int.Parse(wybrana_marka) - 1).FirstOrDefault();

            order.order_status = 1;
            order.order_date = DateTime.Now;
            order.required_date = DateTime.Now.AddDays(3);
           order_items order_Items = new order_items();
            Console.WriteLine("Wybierz Product");
             i = 1;
            foreach (var a in _context.products.OrderBy(c => c.product_id))
            {
                Console.WriteLine(i + a.product_name);
                i++;
            }
            wybrana_marka = Console.ReadLine();
            order_Items.product = _context.products.OrderBy(c => c.product_id).Skip(int.Parse(wybrana_marka) - 1).FirstOrDefault();

            Console.WriteLine("Podaj ilosc");
            order_Items.quantity=int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj rabat");
            order_Items.discount=decimal.Parse(Console.ReadLine());
            
            order_Items.list_price = 213.2M;

       
            order.order_items.Add(order_Items);

            _context.order_items.Add(order_Items);
           
            _context.orders.Add(order);
            _context.SaveChanges();

        }

        public void Showorders()
        {
            
            var ord = _context.orders.Include("store").Include("staff").Include("order_items").OrderByDescending(a=>a.order_id);


            foreach (var o in ord)
            {

                Console.WriteLine($"{o.order_id} {o.required_date} {o.store.store_name} {o.staff.first_name} {o.staff.last_name}");

                foreach (var a in o.order_items)
                {
                    Console.WriteLine($"{a.product.product_name} {a.product.brand.brand_name} {a.quantity} {a.list_price}");
                }
            }
            

        }
        public  void Add_Order2()
        {

            order_items oi = _context.order_items.OrderByDescending(a=>a.order_id).FirstOrDefault();
            order order1 = _context.orders.OrderByDescending(a => a.order_id).FirstOrDefault();

            int c = 0;
            while (1000 > c)
            {

                order1.order_items.Add(oi);

                _context.order_items.Add(oi);

                _context.orders.Add(order1);
                 _context.SaveChangesAsync();
                Thread.Sleep(3000);
                c++;
            }
           

        }

    }
}
