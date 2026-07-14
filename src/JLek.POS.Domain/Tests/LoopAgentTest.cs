using System;

namespace JLek.POS.Domain
{
    public class Customer
    {
        public string Name { get; set; }
        
        public void Print()
        {
            Console.WriteLine("Customer");
        }
    }
}

namespace JLek.POS.Domain
{
    public class LoopAgentTest
    {
        public void Test()
        {
            var customer = new Customer();

            customer.Print();

            customer.Name = "ABC";
        }
    }
}
