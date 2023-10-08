using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homewwork_4
{
    // Lớp ngân hàng
    class Bank
    {
        private List<Account> accounts;

        public Bank()
        {
            accounts = new List<Account>();
        }

        // Mở một tài khoản mới
        public void OpenAccount(string accountNumber, string ownerName, string idNumber, decimal balance, decimal interestRate)
        {
            Account account = new Account(accountNumber, ownerName, idNumber, balance, interestRate);
            accounts.Add(account);
        }

        // Nhập tiền vào tài khoản
        public void Deposit(string accountNumber, decimal amount, DateTime transactionDate)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount, transactionDate);
            }
            else
            {
                Console.WriteLine("Tài khoản không tồn tại.");
            }
        }

        // Rút tiền từ tài khoản
        public void Withdraw(string accountNumber, decimal amount, DateTime transactionDate)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount, transactionDate);
            }
            else
            {
                Console.WriteLine("Tài khoản không tồn tại.");
            }
        }

        // Xem số tiền hiện có trong tài khoản
        public void CheckBalance(string accountNumber)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                Console.WriteLine("Số dư hiện tại của tài khoản {0} là {1} Euros.", accountNumber, account.Balance);
            }
            else
            {
                Console.WriteLine("Tài khoản không tồn tại.");
            }
        }

        // Tính lãi suất cho tất cả các tài khoản và cập nhật số tiền
        public void CalculateInterest()
        {
            foreach (Account account in accounts)
            {
                decimal interest = account.Balance * account.InterestRate / 100;
                account.Deposit(interest, DateTime.Now);
            }
        }

        // In ra báo cáo
        public void PrintReport()
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine("Số tài khoản: {0}", account.AccountNumber);
                Console.WriteLine("Số dư hiện tại: {0} Euros", account.Balance);
                Console.WriteLine("Lịch sử giao dịch:");

                foreach (Transaction transaction in account.Transactions)
                {
                    Console.WriteLine("- Ngày: {0}, Kiểu giao dịch: {1}, Số tiền: {2} Euros", transaction.TransactionDate, transaction.TransactionType, transaction.Amount);
                }

                Console.WriteLine();
            }
        }

        // Tìm tài khoản theo số tài khoản
        private Account FindAccount(string accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }

    // Lớp tài khoản
    class Account
    {
        public string AccountNumber { get; set; }//số tài khoản
        public string OwnerName { get; set; }//tên chủ tài khoản
        public string IdNumber { get; set; }//số cmnd
        public decimal Balance { get; set; }//số dư
        public decimal InterestRate { get; set; }//lãi suất
        public List<Transaction> Transactions { get; set; }//anh sách giao dich

        public Account(string accountNumber, string ownerName, string idNumber, decimal balance, decimal interestRate)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            IdNumber = idNumber;
            Balance = balance;
            InterestRate = interestRate;
            Transactions = new List<Transaction>();
        }

        // Nhập tiền vào tài khoản
        public void Deposit(decimal amount, DateTime transactionDate)
        {
            Balance += amount;
            Transaction transaction = new Transaction(transactionDate, "Nhập tiền", amount);
            Transactions.Add(transaction);
        }

        // Rút tiền từ tài khoản
        public void Withdraw(decimal amount, DateTime transactionDate)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Transaction transaction = new Transaction(transactionDate, "Rút tiền", amount);
                Transactions.Add(transaction);
            }
            else
            {
                Console.WriteLine("Số dư không đủ để thực hiện giao dịch rút tiền.");
            }
        }
    }

    // Lớp giao dịch
    class Transaction
    {
        public DateTime TransactionDate { get; set; }// Ngày giao dịch
        public string TransactionType { get; set; }// Loại giao dịch
        public decimal Amount { get; set; }// Số tiền

        public Transaction(DateTime transactionDate, string transactionType, decimal amount)
        {
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            Amount = amount;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Bank bank = new Bank();

            // Mở tài khoản
            bank.OpenAccount("001", "Alice", "901", 100, 5);
            bank.OpenAccount("002", "Bob", "902", 50, 5);
            bank.OpenAccount("003", "Alice", "901", 200, 10);
            bank.OpenAccount("004", "Eve", "903", 200, 10);

            // Nhập tiền vào tài khoản
            bank.Deposit("001", 100, new DateTime(2005, 7, 15));
            bank.Deposit("001", 100, new DateTime(2005, 7, 31));
            bank.Deposit("002", 150, new DateTime(2005, 7, 1));
            bank.Deposit("002", 150, new DateTime(2005, 7, 15));
            bank.Deposit("003", 200, new DateTime(2005, 7, 5));
            bank.Deposit("004", 250, new DateTime(2005, 7, 31));

            // Rút tiền từ tài khoản
            bank.Withdraw("001", 10, new DateTime(2005, 7, 10));
            bank.Withdraw("002", 20, new DateTime(2005, 7, 15));
            bank.Withdraw("003", 30, new DateTime(2005, 7, 31));
            bank.Withdraw("004", 40, new DateTime(2005, 7, 31));

            // Tính lãi suất và cập nhật số tiền
            bank.CalculateInterest();

            // In báo cáo
            bank.PrintReport();
        }
    }
}
