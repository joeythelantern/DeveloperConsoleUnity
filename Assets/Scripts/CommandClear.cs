using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandClear : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandClear()
        {
            Name = "Clear";
            Command = "clear";
            Description = "Erases the console text.";
            Help = "Use this command to start a fresh console.";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            DeveloperConsole.Instance.ClearConsole();
        }

        public static CommandClear CreateCommand()
        {
            return new CommandClear();
        }
    }
}