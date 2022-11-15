using System;
using Spectre.Console;
namespace MyBank
{
    public class ConsoleInterface
    {
        public ConsoleInterface()
        {
            AnsiConsole.Markup("[underline red]Hello[/] World!");
        }
    }
}

