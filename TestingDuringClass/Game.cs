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
            InitializeEnemy();
        }

        public void Update()
        {
            DisplayCurrentScene();
        }

        public void End()
        {

        }

        public void Save()
        {
            StreamWriter writer = new StreamWriter("SaveData.txt");

            writer.WriteLine(_currentScene);

            _player.Save(writer);

            _currentEnemy.Save(writer);

            writer.Close();
        }

        public bool Load()
        {
            bool loadSuccessful = true;

            if (!File.Exists("SaveData.txt"))
            {
                loadSuccessful = false;
            }

            StreamReader reader = new StreamReader("SaveData.txt");

            if (!int.TryParse(reader.ReadLine(), out _currentScene))
            {
                //...return false
                loadSuccessful = false;
            }

            string job = reader.ReadLine();

            _player = new Player(_items);

            _player.Job = job;

            if (!_player.Load(reader))
            {
                loadSuccessful = false;
            }

            _currentEnemy = new Entity();

            if (!_currentEnemy.Load(reader))
            {
                loadSuccessful = false;
            }

            reader.Close();

            return loadSuccessful;
        }
        
            
        

        public void InitializeItems()
        {
            Item gun = new Item { Name = "Handgun", StatBoost = 25, Type = ItemType.ATTACK };

            Item flashlight = new Item { Name = "Flashlight", StatBoost = 15, Type = ItemType.DEFENSE };

            _items = new Item[] { gun, flashlight };
        }

        public void InitializeEnemy()
        {
            Entity cthulu = new Entity("Cthulu", 300, 35, 10);

            Entity god = new Entity("God", 150, 50, 45);

            Entity satan = new Entity("Satan", 200, 40, 30);

            Random rnd = new Random();
            int num = rnd.Next(1, 4);

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
            else if (choice == 1)
            {
                if (Load())
                {
                    Console.WriteLine("Welcome back!");
                    Console.ReadKey(true);
                    Console.Clear();
                    
                }
                else
                {
                    Console.WriteLine("Load Failed.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        void GetPlayerName()
        {
            bool validInputRecieved = true;
            while (validInputRecieved == true)
            {
                TypeOutWords("Good choice! Now, please enter your name.\n> ", 50);
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
                _player = new Player(_playerName, 50, 25, 0, 100, 0, _items, "Brawler");
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 75, 15, 10, 75, 0, _items, "Mad Man");
            }
            else if (choice == 2)
            {
                _player = new Player(_playerName, 75, 15, 10, 85, 10, _items, "Thief");
            }

            _currentScene = 2;
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
                case 2:
                    Introduction();
                    break;
                case 3:
                    FirstEncounter();
                    break;
               
            }
        }

        public void DisplayEquipItemMenu()
        {
            //Get item index
            int choice = GetInput("Select an item to equip.\n", _player.GetItemNames());

            //Equip item at given index
            if (!_player.TryEquipItem(choice))
            {
                TypeOutWords("You couldn't find that item in your bag.", 50);
            }

            //Print feedback
            TypeOutWords("You equipped the " + _player.CurrentItem.Name + "!", 50);
        }

        public void Introduction()
        {
            Console.Write("Autosaving ");

            for (int i = 0; i < 101; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, 75);
                Console.Write(i + "%");
                Thread.Sleep(num);
                Console.SetCursorPosition(11, 0);
                Console.Write(" ");
            }

            Thread.Sleep(1000);

            Console.Clear();

            Save();

            TypeOutWords("Game Saved", 50);

            Thread.Sleep(500);

            Console.Clear();

            TypeOutWords("Now that the boring things are over let me introduce myself.\n", 50);
            TypeOutWords(". . .", 150);
            TypeOutWords("I am your enemy", 50);

            Thread.Sleep(400);

            TypeOutWords("\nNow... be rebooted into my world", 100);

            Thread.Sleep(400);

            TypeOutWords("dddddddddddrebootworld\n" + _playerName + "         ootintomyworld\nmyworldmyworld     ERROR\n       orldREB0OT", 10);

            for (int i = 0; i < 99; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, 75);
                Console.Write("INSTALLING INFECTION ");
                Console.Write(i + "%");
                Thread.Sleep(num);
                Console.SetCursorPosition(16, 8);
                Console.Write(" ");
            }

            Thread.Sleep(1500);

            Console.Clear();

            TypeOutWords(".....", 150);
            TypeOutWords("\n'It's time to wake up'", 50);
            TypeOutWords("\n\n(You hear a voice and open your eyes. You awaken to an empty, dimly lit room" +
                "\nThere is only one door and a table in front of you with a handgun, a flashlight, and a note on it. The note says" +
                "\n'Survive for me. You must always move forward.')\n\nYou pick up the flashlight and handgun. ", 35);

            _currentScene = 3;
        }

        public void FirstEncounter()
        {
            int choice = GetInput("What would you like to do?\n", "Push Forward", "Equip Item", "Remove Item", "Save Game");

            if (choice == 0)
            {
                RoomJourney();
            }
            else if (choice == 1)
            {
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (choice == 2)
            {
                if (!_player.TryRemoveCurrentItem())
                {
                    TypeOutWords("You don't have anything equipped.", 50);
                }
                else
                {
                    TypeOutWords("You placed the item in your bag", 50);
                }
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (choice == 3)
            {
                Save();
                TypeOutWords("Saved Game", 50);
                Console.ReadKey(true);
                Console.Clear();
            }


        }

        public void RoomJourney()
        {
            Random rnd = new Random();
            int room1 = rnd.Next(1, 7);

            if (room1 == 1)
            {
                //Each of these rooms have a specific scene within them
                //this could either be good or bad for the player
                //this is the first room
                
                Console.WriteLine("You walk in to a dimly lit room. A man stands before you bloodied and bruised." +
                    "\n He holds a sword in his hand. He slowly face you and says 'r-r-re b-boot?' The man then" +
                    "\n lunges at you.\n");
                if (_player.CurrentItem.Type == ItemType.ATTACK)
                {
                    Console.WriteLine("You pull out your handgun and shoot the man in the chest. He pulls back and tilts his head at you." +
                        "\n 're...boot...' He turns away and as he does his body siezes and falls to the ground. He is dead.\n");
                    Console.WriteLine("SUSTAIN 5 SANITY LOSS");
                    _player.GainInfection(5);
                    _player.LoseSanity(5);
                }
                else
                {
                    Console.WriteLine("The man swings his sword widly at you. With no weapon yourself you are defenseless." +
                        "\n He madly slashes your chest and arms screaming the same word over and over again." +
                        "\n re..boot   rebOOT   REBOOT   ??REE??BOOTT?????REEEB?OO?TTT\n He collapses to the floor in the middle of one of his swings at you.\n");
                    Console.WriteLine("You take severe injuries");
                    Console.WriteLine("SUSTAIN 35 DAMAGE");
                    Console.WriteLine("SUSTAIN 15 SANITY LOSS");
                    _player.GainInfection(5);
                    _player.TakeDamage(35);
                    _player.LoseSanity(15);
                }
            }
            //The second room
            else if (room1 == 2)
            {
                Console.WriteLine("You walk in to a room with a roaring fireplace and lush carpets. On the table you find a medkit and heal 20 health!");
                _player.TakeDamage(-20);
            }
            //the third room
            else if (room1 == 3)
            {
                Console.WriteLine("You enter in a sanctuary of sorts. There are pews, symbolic pieces, and scripture on the walls. It feels safe. You regain 20 sanity.");
                _player.LoseSanity(-20);
                
            }
            //the fourth room
            else if (room1 == 4)
            {
                Console.WriteLine("You walk in to a room that smells strongly of iron. It is too dark to see but the walls are covered in a warm liquid. \n" +
                    "While it does make you nauseous, you are safe.\n");
                Console.WriteLine("SUSTAIN 30 SANITY LOSS");
                _player.LoseSanity(30);
            }
            //the fifth room
            else if (room1 == 5)
            {
                Console.WriteLine("You stumble into a long hallway. As you walk down this dim lit path you find a sword lying on the ground.\n" +
                    "You may take it if you do not have a weapon already.");
            }
            //the sixth room
            else if (room1 == 6)
            {
                Console.WriteLine("Walking into this room you smell tar and asphalt. The air is humid and thick. Breathing is an almost impossible task. " +
                    "\nYou see a vial of purple sludge on the ground half spilled over. You inspect it. \n");

                int input = GetInput("Would you like to drink?", "Yes", "No");

                if (input == 1)
                {
                    Console.WriteLine("\nYou decide to drink this putrid liquid. As it goes down your esophagus it has the taste of rotting flesh and the consistancy of spoiled milk." +
                        "\nImmediately you vomit. What comes out is a viscous black liquid. As this matter leaves your body, your mind feels less cluttered.");
                    _player.GainInfection(-15);
                }

                else if (input == 2)
                {
                    Console.WriteLine("\nYou use your better judgement and decide not to drink the vial of mysterious liquid." +
                        "\nYou move forward.");
                }
            }
            Console.WriteLine("");

            Console.WriteLine("Press ENTER to continue");
            Console.ReadKey();
            Console.Clear();
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

