using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;
using Spectre.Console;
using Terminal.Gui;

namespace MyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> accounts = new List<BankAccount>();
            
                BankAccountDTO bankAccountDTO = new BankAccountDTO();
                accounts = bankAccountDTO.getBankAccounts();

            AnsiConsole.Write(
                new Markup(
                    "Bienvenido a [bold blue]NGB[/], por favor introduce tu [green]tu nombre:[/] \n"
                )
            );
            string userName = "";
            while (userName == "")
            {
                userName = Console.ReadLine();

                if (userName == null || userName == "")
                {
                    AnsiConsole.Write(
                        new Markup(
                            "[red bold]El nombre introducido no es correcto, por favor introducelo de nuevo.[/]"
                        )
                    );
                }
            }

            seleccionarOpcion();

            void crearCuenta()
            {
                Console.WriteLine(
                    $"Bienvenido a la creación de cuenta. \n Por favor selecciona el tipo de cuenta que quieres crear"
                );
                var acc = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Opciones: ")
                        .PageSize(4)
                        .AddChoices(new[] { "Cuenta corriente" })
                );

                switch (acc)
                {
                    case "Cuenta corriente":
                        //code
                        AnsiConsole.Write(
                            new Markup(
                                "Introduce el [green]saldo inicial[/] que deseas ingresar (el saldo no puede ser 0): \n"
                            )
                        );
                        decimal initialValue = Convert.ToDecimal(Console.ReadLine());

                        if (initialValue == 0)
                        {
                            AnsiConsole.Write(
                                new Markup("[red]El saldo inicial tiene que ser diferente de 0")
                            );
                            return;
                        }
                        ;

                        var account = new BankAccount(userName, initialValue);
                        AnsiConsole.Write(
                            new Markup(
                                $"Gracias por crear una cuenta corriente con nosotros, {userName}. \n Your bank account number is: {account.Number} y tu saldo inicial es: {account.Balance} \n"
                            )
                        );
                        try
                        {
                            insertarCuenta(account);
                        }
                        catch(Exception e)
                        {
                            AnsiConsole.Write(
                                new Markup(
                                    "\n[red]Ha habido un error con la cuenta seleccionada, por favor intentalo de nuevo[/]"
                                )
                            );
                            Console.WriteLine($"{e}");
                        }
                        seleccionarOpcion();
                        break;
                    default:
                        Console.Write("Error");
                        AnsiConsole.Write(
                            new Markup(
                                "[red]Ha habido un error con la cuenta seleccionada, por favor intentalo de nuevo[/]"
                            )
                        );
                        break;
                }
            }

            void seleccionarOpcion()
            {
                AnsiConsole.WriteLine($"Bienvenid@ {userName}, ¿Qué quieres hacer?");
                var opt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Opciones: ")
                        .PageSize(4)
                        .AddChoices(
                            new[]
                            {
                                "Crear una cuenta",
                                "Entrar a tu cuenta",
                                "Borrar tu cuenta",
                                "Salir"
                            }
                        )
                );
                AnsiConsole.WriteLine($"Has seleccionado: {opt}.");

                switch (opt)
                {
                    case "Crear una cuenta":
                        Console.Clear();
                        crearCuenta();
                        break;
                    case "Entrar a tu cuenta":
                        entrar();
                        Console.Clear();

                        break;
                    case "Borrar tu cuenta":
                        borrarCuenta();
                        Console.Clear();
                        break;
                    default:
                        exit();
                        Console.Clear();
                        break;
                }
            }
            void entrar() {
                BankAccount userAccount;
                try
                {
                    userAccount = accounts.Single(r => r.Owner == userName);
                }
                    catch
                {
                    AnsiConsole.Write(new Markup($"No existen cuentas con usuario {userName}. \n"));
                    seleccionarOpcion();
                    return;
                }
                var accountC = new Account(userAccount);
                accounts[accounts.IndexOf(userAccount)] = accountC.getAccount();
                bankAccountDTO.writeAccounts(accounts);
                seleccionarOpcion();
            }
            void borrarCuenta() {
                var itemToRemove = accounts.Single(r => r.Owner == userName);
                AnsiConsole.Write(new Markup($"¿{userName} estas seguro que deseas [red]borrar[/] la cuenta {itemToRemove.Number}? Su saldo de {itemToRemove.Balance} será donado a una ONG. \n"));
                var opt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Opciones: ")
                        .PageSize(4)
                        .AddChoices(
                            new[]
                            {
                                "Si, estoy seguro",
                                "No, volver"
                            }
                        )
                );

                if(opt == "No, volver")
                {
                    seleccionarOpcion();
                }

                accounts.Remove(itemToRemove);
                bankAccountDTO.writeAccounts(accounts);
                AnsiConsole.Write(new Markup("Tu cuenta se ha eliminado satisfactoriamente, esperamos volver a contar contigo en un futuro."));
                seleccionarOpcion();
            }

            void exit()
            {
                
            }

            void insertarCuenta(BankAccount account)
            {
                accounts.Add(account);
                bankAccountDTO.writeAccounts(accounts);
               // escribir(accounts);
            }
        }
    }
}
