using System;
using System.Collections.Generic;

namespace Refactoring
{
    class Program
    {
        static decimal price;
        static string orderSummary;
        static Tuple<string, List<Product>> order;

        static void Main(string[] args)
        {
            CreateInputData();

            MakeCalculation();

            PrintResult();

        }

        static void CreateInputData()
        {
            order = new Tuple<string, List<Product>>("John Doe",
                new List<Product>
                {
                new Product
                {
                    ProductName = "Pulled Pork",
                    Price = 6.99m,
                    Weight = 0.5m,
                    PricingMethod = EnumPricingMethod.PerPound,
                },
                new Product
                {
                    ProductName = "Coke",
                    Price = 3m,
                    Quantity = 2,
                    PricingMethod = EnumPricingMethod.PerItem
                }
            }
            ); ;
            price = 0m;
            orderSummary = "ORDER SUMMARY FOR " + order.Item1 + ": \r\n";
        }

        static void MakeCalculation()
        {
            foreach (var orderProduct in order.Item2)
            {
                orderSummary += orderProduct.ProductName;

                CalculateProduct(orderProduct);

                orderSummary += "\r\n";
            }
        }

        static void PrintResult()
        {
            Console.WriteLine(orderSummary);
            Console.WriteLine("Total Price: $" + price);

            Console.ReadKey();
        }

        static void CalculateProduct(Product orderProduct)
        {
            var productPrice = 0m;

            if (orderProduct.PricingMethod == EnumPricingMethod.PerPound)
            {
                productPrice = (orderProduct.Weight.Value * orderProduct.Price);
                orderSummary += string.Format(" ${0} ({1} pounds at ${2} per pound)", productPrice, orderProduct.Weight, orderProduct.Price);
            }
            else if (orderProduct.PricingMethod == EnumPricingMethod.PerItem)
            {
                productPrice = (orderProduct.Quantity.Value * orderProduct.Price);
                orderSummary += string.Format(" ${0} ({1} items at ${2} each)", productPrice, orderProduct.Quantity, orderProduct.Price);
            }
            else
            {
                throw new Exception("Unexpected price method");
            }

            price += productPrice;
        }
    }

}

