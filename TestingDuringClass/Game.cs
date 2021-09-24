using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace HelloDungeonExpanded
{
    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }
    class Game
    {
        private int _currentScene = 0;
        private bool _gameOver;
        private Player _player;
        private Entity _currentEnemy;
        private string _playerName;
        private Item[] _items;

        public void Run()
        {
            //The main game loop
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        public void Start()
        {
            InitializeItems();
        }

        public void Update()
        {
            DisplayCurrentScene();
        }

        public void End()
        {

        }

        public void InitializeItems()
        {
            Item gun = new Item { Name = "Handgun", StatBoost = 25, Type = ItemType.ATTACK };

            Item flashlight = new Item { Name = "Flashlight", StatBoost = 15, Type = ItemType.DEFENSE };

            _items = new Item[] { gun, flashlight };
        }

        public void InitializeEnemy()
        {
            Entity cthulu = new Entity("[REDACTED]", 300, 35, 10);

            Entity god = new Entity("[REDACTED]", 150, 50, 45);

            Entity satan = new Entity("[REDACTED]", 200, 40, 30);

            Random rnd = new Random();
            int num = rnd.Next(1, 3);

            if (num == 1)
            {
                _currentEnemy = cthulu;
            }
            else if (num == 2)
            {
                _currentEnemy = god;
            }
            else if (num == 3)
            {
                _currentEnemy = satan;
            }
        }

        public void DisplayOpeningMenu()
        {
            int choice = GetInput("Welcome to the game. Would you like to: \n", "Start a New Game", "Load an Old Save");

            if (choice == 0)
            {
                _currentScene = 1;
            }
        }

        void GetPlayerName()
        {
            bool validInputRecieved = true;
            while (validInputRecieved == true)
            {
                TypeOutWords("Welcome! Please enter your name.\n> ", 50);
                _playerName = Console.ReadLine();
                Console.Clear();

                int choice = GetInput("You've entered " + _playerName + ", are you sure you want to keep this name?\n",
                    "Yes", "No");
                if (choice == 0)
                {
                    validInputRecieved = false;
                }
                else
                {
                    validInputRecieved = true;
                }
            }

        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Nice to meet you " + _playerName + ". Please select a character.\n", "Brawler", "Mad Man", "Thief");

            if (choice == 0)
            {
                _player = new Player(_playerName, 50, 25, 0, _items, "Brawler");
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 75, 15, 10, _items, "Mad Man");
            }
            else if (choice == 2)
            {
                _player = new Player(_playerName, 75, 15, 10, _items, "Thief");
            }
        }

        public void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
                case 1:
                    GetPlayerName();
                    CharacterSelection();
                    break;
               
            }
        }



        public void TypeOutWords(string sentence, int timeBetweenLetters)
        {
            char[] chars = sentence.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                Console.Write(chars[i]);
                Thread.Sleep(timeBetweenLetters);
            }
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                //Print options
                TypeOutWords(description, 50);

                for (int i = 0; i < options.Length; i++)
                {
                    TypeOutWords((i + 1) + "." + options[i], 30);
                    Console.WriteLine(" ");
                }
                TypeOutWords("> ", 50);

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputRecieved))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        //Set input recieved to be the default value
                        inputRecieved = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //If the player didn't type an int
                else
                {
                    //Set input recieved to be the default value
                    inputRecieved = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
        }
    }
}

