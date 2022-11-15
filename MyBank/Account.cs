using System;
using Spectre.Console;

namespace MyBank
{
    public class Account
    {
        public BankAccount account {get; set;}

        //constructor runner
        public Account(BankAccount account)
        {
            AnsiConsole.Write(new Markup($"{account.Owner} has accedido a la cuenta {account.Number}, tu balance actual es de {account.Balance} \n"));
            seleccionarOpcion();

            void seleccionarOpcion()
            {
                AnsiConsole.WriteLine("\n ¿Qué quieres hacer?");
                var opt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Opciones: ")
                        .PageSize(4)
                        .AddChoices(
                            new[]
                            {
                                "Ingresar dinero.",
                                "Sacar dinero.",
                                "Ver tu historial.",
                                "Salir."
                            }
                        )
                );
                AnsiConsole.WriteLine($"Has seleccionado: {opt}.");

                switch (opt)
                {
                    case "Ingresar dinero.":
                        
                        ingresarDinero();
                        Console.Clear();
                        break;
                    case "Sacar dinero.":
                        Console.Clear();
                        sacarDinero();
                        break;
                    case "Ver tu historial.":
                        Console.Clear();
                        Console.WriteLine(account.GetAccountHistory());
                        seleccionarOpcion();
                        break;
                    default:
                        Console.Clear();
                        return;
                }
            }
            void ingresarDinero()
            {
                AnsiConsole.Write(new Markup("\nIngresa el valor a ingresar.[yellow](El valor tiene que ser superior a 0)[/]"));
                decimal amount = Decimal.Parse(Console.ReadLine());
                AnsiConsole.Write(new Markup("\n¿Qué concepto tiene esta transacción? [yellow]Ej. 'Sorteo navidad'[/]"));
                string note = Console.ReadLine();

                account.MakeDeposit(amount, note);

                seleccionarOpcion();
            }

            void sacarDinero()
            {
                AnsiConsole.Write(new Markup("\nIngresa el valor a sacar.[yellow](El valor tiene que ser superior a 0)[/]"));
                decimal amount = Decimal.Parse(Console.ReadLine());
                AnsiConsole.Write(new Markup("\n¿Qué concepto tiene esta transacción? [yellow]Ej. 'Compra del mes'[/]"));
                string note = Console.ReadLine();

                account.MakeWithdrawal(amount, note);
                seleccionarOpcion();
            }

            return;
        }
        public BankAccount getAccount()
        {
            return account;
        }
        
    }
}

