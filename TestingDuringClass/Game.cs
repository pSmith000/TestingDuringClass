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
                    PrintPlayerStats();
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
            TypeOutWords("\n\nYou hear a voice and open your eyes. You awaken to an empty, dimly lit room" +
                "\nThere is only one door and a table in front of you with a handgun, a flashlight, and a note on it. The note says" +
                "\n'Survive for me. You must always move forward.'\n\nYou pick up the flashlight and handgun. \n", 35);

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
            int room1 = rnd.Next(81);

            if (room1 > 0 && room1 < 11)
            {
                //Each of these rooms have a specific scene within them
                //this could either be good or bad for the player
                //this is the first room
                
                TypeOutWords("You walk in to a dimly lit room. A man stands before you bloodied and bruised." +
                    "\n He holds a sword in his hand. He slowly faces you and says 'r-r-re b-boot?' The man then" +
                    "\n lunges at you.\n\n", 15);
                if (_player.CurrentItem.Type == ItemType.ATTACK)
                {
                    TypeOutWords("You pull out your handgun and shoot the man in the chest. He pulls back and tilts his head at you." +
                        "\n're...boot...' He turns away and as he does his body siezes and falls to the ground. He seems dead.\n", 20);
                    TypeOutWords("SUSTAIN 5 SANITY LOSS", 50);
                    _player.GainInfection(5);
                    _player.LoseSanity(5);
                }
                else
                {
                    TypeOutWords("The man swings his sword widly at you. With no weapon yourself you are defenseless." +
                        "\nHe madly slashes your chest and arms screaming the same word over and over again." +
                        "\nre..boot   rebOOT   REBOOT   ??REE??BOOTT?????REEEB?OO?TTT\n He collapses to the floor in the middle of one of his swings at you.\n", 25);
                    TypeOutWords("You take severe injuries\n", 50);
                    TypeOutWords("SUSTAIN 35 DAMAGE\n", 50);
                    TypeOutWords("SUSTAIN 15 SANITY LOSS", 50);
                    _player.GainInfection(5);
                    _player.TakeDamage(35);
                    _player.LoseSanity(15);
                }
            }
            //The second room
            else if (room1 > 10 && room1 < 21)
            {
                TypeOutWords("You walk in to a room with a roaring fireplace and lush carpets. On the table you find a medkit and heal 20 health!", 25);
                _player.TakeDamage(-20);
            }
            //the third room
            else if (room1 > 20 && room1 < 31)
            {
                TypeOutWords("You enter in a sanctuary of sorts. There are pews, symbolic pieces, and scripture on the walls. It feels safe. You regain 20 sanity.", 25);
                _player.LoseSanity(-20);
                
            }
            //the fourth room
            else if (room1 > 30 && room1 < 41)
            {
                TypeOutWords("You walk in to a room that smells strongly of iron. It is too dark to see but the walls are covered in a warm liquid. \n" +
                    "While it does make you nauseous, you are safe.\n", 25);
                TypeOutWords("SUSTAIN 30 SANITY LOSS", 50);
                _player.LoseSanity(30);
            }
            //the fifth room
            else if (room1 > 40 && room1 < 51)
            {
                TypeOutWords("You stumble into a long hallway that splits off into two different paths. One on the left, and one on the right." +
                    "\n" , 25);

                GetInput("Which path would you like to take?", "Left", "Right");

                Random random = new Random();
                int num = random.Next(1, 3);

                if (num == 1)
                {
                    TypeOutWords("You take this path and walk along it for some time. As you go the walls get closer and closer to you. " +
                        "\nYou look back for an escape but the floor crumbles to a black pit behind you. You start to panic and sprint forward." +
                        "\nYou stumble... landing on the floor. You look up and meet the eyeless gaze of [REDACTED]. Hope is fleeting.", 25);
                    Thread.Sleep(400);
                    Console.Clear();
                    TypeOutWords("'Fall'\n", 150);
                    Thread.Sleep(400);
                    TypeOutWords("A demonically similar voice.\n\n", 100);
                    TypeOutWords("The ground crumbles beneath you and you fall", 50);
                    Console.SetCursorPosition(0, 0);
                    TypeOutWords("fallfallfallfallfallfallfallfallfallfallfall", 150);
                    Thread.Sleep(400);
                    Console.Clear();
                    TypeOutWords("You hit the ground with a thud. You open your eyes and you are in an empty concrete room with a door in front of you." +
                        "\n\nSUSTAIN 40 DAMAGE\nSUSTAIN 20 SANITY LOSS", 50);

                    _player.TakeDamage(40);
                    _player.LoseSanity(20);
                }
                else
                {
                    TypeOutWords("You stumble down this path for a few minutes. It opens up to a large elegant room with a pool in the center." +
                        "\nAs you set into the pool you immediately feel refreshed. You heal 15 health and 10 sanity.", 50);
                    _player.TakeDamage(-15);
                    _player.LoseSanity(-10);
                }
            }
            //the sixth room
            else if (room1 > 50 && room1 < 61)
            {
                TypeOutWords("Walking into this room you smell tar and asphalt. The air is humid and thick. Breathing is an almost impossible task. " +
                    "\nYou see a vial of purple sludge on the ground half spilled over. You inspect it. \n", 25);

                int input = GetInput("Would you like to drink?\n", "Yes", "No");

                if (input == 0)
                {
                    TypeOutWords("You decide to drink this putrid liquid. As it goes down your esophagus it has the taste of rotting flesh " +
                        "\nand the consistancy of spoiled milk." +
                        "\nImmediately you vomit. What comes out is a viscous black liquid. As this matter leaves your body, " +
                        "\nyour mind feels less cluttered.", 25);
                    _player.GainInfection(-15);
                }

                else if (input == 1)
                {
                    TypeOutWords("\nYou use your better judgement and decide not to drink the vial of mysterious liquid." +
                        "\nYou move forward.", 25);
                }
            }
            else if (room1 > 60 && room1 < 71)
            {
                TypeOutWords("You open the next door and inside you see ", 25);

                if (_player.CurrentItem.Type == ItemType.DEFENSE)
                {
                    TypeOutWords("a hideous creature shrouded in the darkness. You try to shine your flashlight directly at it, but it's gone." +
                        "\nAs you walk in you feel a drip on your shoulder and look up. Your eyes meet a souless, black grin. " +
                        "\nIt's skin hangs loosely from its bones as its head spins slowly towards you. You become paralyzed with fear." +
                        "\n\nAs you try to take a step back the creature falls upon you, scratching at your face as it tries to grab it." +
                        "\nYou rush forward to the next door and before you open it you look back for the creature, but see nothing." +
                        "\nIt is gone.\n\nSUSTAIN 30 SANITY LOSS\nSUSTAIN 15 DAMAGE", 25);
                    _player.TakeDamage(15);
                    _player.LoseSanity(30);
                    _player.GainInfection(8);
                }
                else
                {
                    TypeOutWords("nothing. You think of taking out your flashlight but decide its better to have a weapon than light." +
                        "\nYou walk through the room and seemingly nothing happens. Your anxious, but alive.\n\nSUSTAIN 5 SANITY LOSS", 25);
                    _player.LoseSanity(5);
                }
            }
            else if (room1 > 70 && room1 < 81)
            {
                TypeOutWords("This room is beyond dark.", 25);
            }
            Console.WriteLine("");

            TypeOutWords("Press ENTER to continue", 50);
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Prints player stats neatly in a box
        /// </summary>
        void PrintPlayerStats()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Health: " + _player.Health);
            Console.WriteLine("Sanity: " + _player.Sanity);
            Console.WriteLine("[REDACTED]: " + _player.Infection);
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
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

