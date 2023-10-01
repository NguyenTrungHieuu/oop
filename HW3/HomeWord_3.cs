using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewwork_3
{
    // Lớp Item đại diện cho một mặt hàng
    class Item
    {
        private double price;
        private double discount;

        public Item(double price, double discount)
        {
            this.price = price;
            this.discount = discount;
        }

        public double GetPrice()
        {
            return price;
        }

        public double GetDiscount()
        {
            return discount;
        }
    }

    // Lớp Employee đại diện cho một nhân viên
    class Employee
    {
        private string name;

        public Employee(string name)
        {
            this.name = name;
        }

        // Phương thức để trả về tên của nhân viên
        public string GetName()
        {
            return name;
        }
    }

    // Lớp GroceryBill đại diện cho hóa đơn mua hàng
    class GroceryBill
    {
        private Employee clerk;
        private List<Item> items;

        public GroceryBill(Employee clerk)
        {
            this.clerk = clerk;
            items = new List<Item>();
        }

        // Phương thức để thêm một mặt hàng vào hóa đơn
        public void Add(Item item)
        {
            items.Add(item);
        }

        // Phương thức để tính tổng giá trị của các mặt hàng trong hóa đơn
        public double GetTotal()
        {
            double total = 0.0;
            foreach (Item item in items)
            {
                total += item.GetPrice();
            }
            return total;
        }

        // Phương thức để in thông tin hóa đơn
        public void PrintReceipt()
        {
            Console.WriteLine("Biên lai:");
            Console.WriteLine("Nhân viên: " + clerk.GetName());
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Mặt hàng");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("   Giá     |  Giảm giá   ");
            Console.WriteLine("--------------------------------------");

            foreach (Item item in items)
            {
                Console.WriteLine("{0,-10:C} | {1,-10:C} ", item.GetPrice(), item.GetDiscount());
            }

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Tổng: {0,27:C}", GetTotal());
        }
    }

    // Lớp DiscountBill kế thừa từ GroceryBill để tính toán giảm giá cho khách hàng ưu đãi
    class DiscountBill : GroceryBill
    {
        private bool preferred;
        private int discountCount;
        private double discountAmount;

        public DiscountBill(Employee clerk, bool preferred) : base(clerk)
        {
            this.preferred = preferred;
            discountCount = 0;
            discountAmount = 0.0;
        }

        // Ghi đè phương thức Add() để tính toán giảm giá cho khách hàng ưu đãi
        public new void Add(Item item)
        {
            base.Add(item);
            if (preferred && item.GetDiscount() > 0)
            {
                discountCount++;
                discountAmount += item.GetDiscount();
            }
        }

        // Phương thức để trả về số lượng mặt hàng được giảm giá
        public int GetDiscountCount()
        {
            return discountCount;
        }

        // Phương thức để trả về tổng giá trị giảm giá
        public double GetDiscountAmount()
        {
            return discountAmount;
        }

        // Phương thức để trả về tỉ lệ phần trăm giảm giá so với tổng giá trị hóa đơn ban đầu
        public double GetDiscountPercent()
        {
            double total = GetTotal();
            if (total > 0)
            {
                return (discountAmount / total) * 100;
            }
            return 0;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tạo một nhân viên
            Employee clerk = new Employee("John");

            // Tạo một hóa đơn mua hàng
            GroceryBill groceryBill = new GroceryBill(clerk);

            // Thêm các mặt hàng vào hóa đơn mua hàng
            Item item1 = new Item(1.35, 0.25);
            Item item2 = new Item(2.50, 0.0);
            Item item3 = new Item(3.75, 0.50);

            groceryBill.Add(item1);
            groceryBill.Add(item2);
            groceryBill.Add(item3);

            // In thông tin hóa đơn mua hàng
            groceryBill.PrintReceipt();

            // Tạo một hóa đơn mua hàng ưu đãi
            DiscountBill discountBill = new DiscountBill(clerk, true);

            // Thêm các mặt hàng vào hóa đơn ưu đãi
            discountBill.Add(item1);
            discountBill.Add(item2);
            discountBill.Add(item3);

            // In thông tin hóa đơn mua hàng ưu đãi
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            discountBill.PrintReceipt();

            // In thông tin giảm giá
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Thông tin giảm giá:");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Số lượng giảm giá: {0}", discountBill.GetDiscountCount());
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Số tiền giảm giá: ${0}", discountBill.GetDiscountAmount());
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Phần trăm giảm giá: {0}%", discountBill.GetDiscountPercent());
        }
    }
}
