using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            string addMessage = " command has been added to the console.";

            DeveloperConsole.AddCommandsToConsole(Command, this);
            DeveloperConsole.AddStaticMessageToConsole(Name + addMessage);
        }

        public abstract void RunCommand();
    }

    public class DeveloperConsole : MonoBehaviour
    {
        public static DeveloperConsole Instace { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }

        [Header("UI Components")]
        public Canvas consoleCavas;
        public ScrollRect scrollRect;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;

        private void Awake()
        {
            if (Instace != null)
            {
                return;
            }

            Instace = this;
            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCavas.gameObject.SetActive(false);

            CreatCommands();
        }

        private void CreatCommands()
        {
            CommandQuit commandQuit = CommandQuit.CreateCommand();
        }

        public static void AddCommandsToConsole(string _name, ConsoleCommand _command)
        {
            if (!Commands.ContainsKey(_name))
            {
                Commands.Add(_name, _command);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                consoleCavas.gameObject.SetActive(!consoleCavas.gameObject.activeInHierarchy);
            }

            if (consoleCavas.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        ParseInput(inputText.text);
                    }
                }
            }
        }

        public void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public static void AddStaticMessageToConsole(string msg)
        {
            DeveloperConsole.Instace.consoleText.text += msg + "\n";
            DeveloperConsole.Instace.scrollRect.verticalNormalizedPosition = 0f;
        }

        private void ParseInput(string input)
        {
            string[] _input = input.Split(null);

            if(_input.Length == 0 || _input == null)
            {
                AddMessageToConsole("Command not recognized.");
                return;
            }

            if(!Commands.ContainsKey(_input[0]))
            {
                AddMessageToConsole("Command not recognized.");
            }
            else
            {
                Commands[_input[0]].RunCommand();
            }
        }
    }
}