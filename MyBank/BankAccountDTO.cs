using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyBank
{
    class BankAccountDTO
    {
        private List<BankAccount> BankAccounts;
       // private List<Transaction> Transactions;

        public BankAccountDTO() {
            try
            {


                BankAccounts = readAccounts();
            }
            catch
            {
                using (StreamWriter outputFile = new StreamWriter("data.json"))
                {
                    outputFile.WriteLine();
                }
                BankAccounts = new List<BankAccount>();
            }
          //  Transactions = readTransactions();
        }

        public List<BankAccount> getBankAccounts()
        {
            return BankAccounts;
        }

       public List<BankAccount> readAccounts()
        {
            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                List<BankAccount> items = JsonSerializer.Deserialize<List<BankAccount>>(json);
                return items;
            }
        }

      public  List<Transaction> readTransactions()
        {
            using (StreamReader r = new StreamReader("transactions.json"))
            {
                string json = r.ReadToEnd();
                List<Transaction> transactions = JsonSerializer.Deserialize<List<Transaction>>(json);
                return transactions;
            }
            
        }

       public void writeAccounts(List<BankAccount> source)
        {
            List<BankAccount> destination = (List<BankAccount>) source
                .Select(d => new BankAccount(d.Number, d.Owner, d.allTransactions));
                

            string jsonString = JsonSerializer.Serialize(
                destination,
                new JsonSerializerOptions { WriteIndented = true }
            );

            using (StreamWriter outputFile = new StreamWriter("data.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }

     
    }
    
}