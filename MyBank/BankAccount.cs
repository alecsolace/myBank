
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank
{
    public class BankAccount
    {
        public string Number { get; set; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
            set { }
            
        }

        private static int accountNumberSeed = 1;

        public List<Transaction> allTransactions { get; set; } = new List<Transaction>();

        public BankAccount(string name, decimal initialBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            this.Owner = name;
            MakeDeposit(initialBalance, "Initial deposit");
        }

        public BankAccount() { }

        public BankAccount(string Number, string Owner, List<Transaction> transactions)
        {
            this.Number = Number;
            this.Owner = Owner;
            this.allTransactions = transactions;
        }

        public BankAccount(string Number, string Owner, decimal Balance)
        {
            this.Number = Number;
            this.Owner = Owner;
            this.Balance = Balance;
        }

        public List<Transaction> GetTransactions()
        {
            return this.allTransactions;
        }

        public void MakeDeposit(decimal amount, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, DateTime.Now, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, DateTime.Now, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        public static implicit operator List<object>(BankAccount v)
        {
            throw new NotImplementedException();
        }
    }
}