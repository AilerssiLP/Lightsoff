using System;
using LightsOff.Core;

namespace LightsOff
{
    public class Program
    {
        static void Main()
        {
            var field = new Field(5, 5);
            var ui = new ConsoleUI(field);
            ui.Play();

        }
    }
}
