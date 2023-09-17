using System;

namespace Homework_2
{
    public class Person
    {
        private string name;
        private string address;
        private double salary;
        //phương thức khởi tạo
        public Person(string name, string address, double salary)
        {
            this.name = name;
            this.address = address;
            this.salary = salary;
        }
        //phương thức trả về tên 
        public string Name
        {
            get { 
                    return name; //trả về giá trị
                }
            set
            {
                //nếu giá trị rỗng thì nhập lại
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Nhap lai ten");
                    string newName = Console.ReadLine();
                    Name = newName;  // Gọi lại setter để gán giá trị mới
                }
                else
                {
                    name = value;
                }
            }
        }

        //phương thức trả về giá trị địa chỉ
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Nhap lai dia chi");
                    string newAddress = Console.ReadLine();
                    Address = newAddress;  //gán tên địa chỉ mới
                }
                else
                {
                    address = value;
                }
            }
        }

        //phương thức trả về giá  trị lương
        public double Salary
        {
            get
            {
                return salary;
            }
            set
            {
                //nếu lương nhỏ hơn 0 thì nhập lại
                if (value <= 0)
                {
                    Console.WriteLine("Nhap lai luong");
                    double newSalary;
                    while (!double.TryParse(Console.ReadLine(), out newSalary) || newSalary <= 0)
                    {
                        Console.WriteLine("Lương phải là một số dương. Vui lòng nhập lại lương.");
                    }
                    salary = newSalary;//gán giá trị lương mới nếu lương nhập là số âm hoặc bằng 0
                }
                else
                {
                    salary = value;
                }
            }
        }

        //khai báo phương thức nhập thông tin 
        public static Person InputPersonInfo(string name, string address, string sSalary)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Nhap lai name ");
                name = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Nhap lai address ");
                address = Console.ReadLine();
            }

            double salary;
            if (!double.TryParse(sSalary, out salary) || salary <= 0)
            {
                Console.WriteLine("Nhap lai luong");
                while (!double.TryParse(Console.ReadLine(), out salary) || salary <= 0)
                {
                    Console.WriteLine("Lương phải là một số dương. Vui lòng nhập lại lương.");
                }
            }

            return new Person(name, address, salary);
        }

        //phuong thúc hiển thị thông tin
        public static void DisplayPersonInfo(Person person)
        {
            Console.WriteLine("Name: " + person.Name);
            Console.WriteLine("Address: " + person.Address);
            Console.WriteLine("Salary: " + person.Salary);
        }

        //khai báo phương thức SortBySalary nhận vào một mảng people chứa các đối tượng của lớp Person và trả về một mảng Person[].
        public static Person[] SortBySalary(Person[] people)// SortBySalary là sắp xếp theo mức lương
        {
            //try:  câu lệnh có thể gây ra ngoại lệ (thử các trường hợp ngoại lệ)
            try
            {
                //Vòng lặp này duyệt qua từng phần tử trong mảng people, trừ phần tử cuối cùng.
                for (int i = 0; i < people.Length - 1; i++)
                {
                    //Vòng lặp này duyệt qua từng phần tử trong mảng people cho đến phần tử kế cuối của phần tử 
                    for (int j = 0; j < people.Length - i - 1; j++)
                    {
                        if (people[j].Salary > people[j + 1].Salary)//Điều kiện này kiểm tra nếu mức lương 
                        {
                            Person temp = people[j];//Tạo một biến tạm thời temp để lưu giữ đối tượng people[j] trước khi hoán đổi vị trí.
                            people[j] = people[j + 1];
                            people[j + 1] = temp;//hoán đổi vị trí của hai đối tượn
                        }
                    }
                }
                //Trả về mảng people đã được sắp xếp theo mức lương.
                return people;
            }
            catch (Exception ex)//ngoại lệ xảy ra trong quá trình sắp xếp
            {
                throw new Exception("Can't sort people. " + ex.Message);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Person[] people = new Person[3];

                for (int i = 0; i < people.Length; i++)
                {
                    Console.WriteLine("NHAP THONG TIN " + (i + 1) + ":");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Address: ");
                    string address = Console.ReadLine();
                    Console.Write("Salary: ");
                    string sSalary = Console.ReadLine();

                    Person person = Person.InputPersonInfo(name, address, sSalary);
                    people[i] = person;
                }

                Console.WriteLine("\nThong tin:");
                foreach (Person person in people)
                {
                    Person.DisplayPersonInfo(person);
                    Console.WriteLine();
                }

                Console.WriteLine("\nSap xep theo muc luong:");
                Person[] sortedPeople = Person.SortBySalary(people);
                foreach (Person person in sortedPeople)
                {
                    Person.DisplayPersonInfo(person);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}