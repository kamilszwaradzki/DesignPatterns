using System;
using System.Collections.Generic;
namespace VegetablesObserver
{
    abstract class Vegetables
    {
        private double _pricePerKilogram;
        private List<IGroceryStore> _groceryStores = new List<IGroceryStore>();

        public Vegetables(double pricePerKilogram)
        {
            _pricePerKilogram = pricePerKilogram;
        }

        public void Add(IGroceryStore groceryStore)
        {
            _groceryStores.Add(groceryStore);
        }

        public void Detach(IGroceryStore groceryStore)
        {
            _groceryStores.Remove(groceryStore);
        }

        public void Notify()
        {
            foreach (IGroceryStore groceryStore in _groceryStores)
            {
                groceryStore.Update(this);
            }

            Console.WriteLine("");
        }

        public double pricePerKilograms
        {
            get { return _pricePerKilogram; }
            set
            {
                if (_pricePerKilogram != value)
                {
                    _pricePerKilogram = value;
                    Notify(); //Automatically notify our observers of price changes
                }
            }
        }
    }
    class Carrots : Vegetables
    {
        public Carrots(double price) : base(price) { }
    }
    class Tomatoes : Vegetables
    {
        public Tomatoes(double price) : base(price) { }
    }
    interface IGroceryStore
    {
        void Update(Vegetables vegetables);
    }
    class GroceryStore : IGroceryStore
    {
        private string _name;
//        private Vegetables _vegetables;
        private double _purchase;

        public GroceryStore(string name, double purchase)
        {
            _name = name;
            _purchase = purchase;
        }

        public void Update(Vegetables vegetables)
        {
            Console.WriteLine("Notify,that {1} {0} " + " change price to {2:C} per kilogram.", _name, vegetables.GetType().Name, vegetables.pricePerKilograms);
            if (vegetables.pricePerKilograms < _purchase)
            {
                Console.WriteLine(_name + " wants to buy some " + vegetables.GetType().Name + "!");
            }
        }
        static void Main(string[] args)
        {
            Carrots carrots = new Carrots(0.82);
            carrots.Add(new GroceryStore("Cappy", 0.77));
            carrots.Add(new GroceryStore("Hortex", 0.74));
            carrots.Add(new GroceryStore("Inny_Hortex", 0.75));

            carrots.pricePerKilograms = 0.79;
            carrots.pricePerKilograms = 0.76;
            carrots.pricePerKilograms = 0.74;
            carrots.pricePerKilograms = 0.81;

            Tomatoes tomatoes = new Tomatoes(0.99);
            tomatoes.Add(new GroceryStore("Janusz_Hort", 0.60));
            tomatoes.Add(new GroceryStore("Cappyex", 0.550));
            tomatoes.Add(new GroceryStore("Del Monte", 0.10));

            tomatoes.pricePerKilograms = 0.67;
            tomatoes.pricePerKilograms = 0.87;
            tomatoes.pricePerKilograms = 0.34;
            tomatoes.pricePerKilograms = 0.11;

            carrots.pricePerKilograms = 0.88;

            Console.ReadKey();
        }

    }
}
