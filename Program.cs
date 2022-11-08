using System;
using System.Collections.Generic;
using projekt_scraper;
using System.Threading;

namespace pokemonspel
{
    class Program
    {
        static int healthpotion = 0;
        static Scraper scrape = new Scraper();
        static List<string> pokemonInventory = new List<string>();
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Game();
        }

        static void Fight(string pokemon, string enemy)
        {
            scrape.initPokemon(pokemon);
            int[] pokemonStats = scrape.getPokemonStats();
            scrape.initPokemon(enemy);
            int[] enemyStats = scrape.getPokemonStats();
            int round = 0;
            while (pokemonStats[0] > 0 && enemyStats[0] > 0)
            {
                Console.Clear();
                Console.WriteLine($"{pokemon} HP: {pokemonStats[0]}\n{enemy} HP: {enemyStats[0]}");
                if (round % 2 == 0)
                {
                    writeString("Do you want to Attack(1) or Heal(2): ");
                    int playerChoice = chooseNum(1, 2);
                    if (playerChoice == 1)
                    {
                        // Attack (minska motståndarens hp)
                        int damage = calculateDamage(pokemonStats, enemyStats);
                        enemyStats[0] -= damage;
                        Console.WriteLine($"Attacking {enemy} for {damage} damage");
                    }
                    if (playerChoice == 2)
                    {
                        bool rest = false;
                        Console.WriteLine("Rest\n");
                        if (healthpotion > 0)
                        {
                            Console.Write($"You have {healthpotion} health potion(s), do you want to use one?");
                            string choice = Console.ReadLine();
                            if (choice == "yes")
                            {
                                Console.WriteLine("Healing for +20 hp");
                                pokemonStats[0] += 20;
                                healthpotion -= 1;
                            }
                            else 
                            {
                                rest = true;
                            }
                        }
                        else 
                        {
                            writeString("No healthpotions left, ");
                            rest = true;
                        }

                        if(rest)
                        {
                            writeString("Resting for +10 hp\n");
                            pokemonStats[0] += 10;
                        }
                    }
                }
                else
                {
                    int enemyChoice = rnd.Next(1, 5);
                    if (enemyChoice == 4)
                    {
                        enemyStats[0] += 10;
                        Console.WriteLine($"{enemy} healed for +10 hp");
                    }
                    else
                    {
                        int damage = calculateDamage(enemyStats, pokemonStats);
                        pokemonStats[0] -= damage;
                        Console.WriteLine($"{enemy} attacks {pokemon} for {damage} damage");
                        Wait(1);
                    }
                }

                round += 1;
                Wait(1);
            }

            if (pokemonStats[0] > 0)
            {
                Console.WriteLine($"{pokemon} won vs {enemy}");
            }
            else
            {
                Console.WriteLine($"{enemy} won vs {pokemon}");
            }
        }

        static int calculateDamage(int[] attackingStats, int[] defendingStats) 
        {
            return (attackingStats[1] / ((defendingStats[2]) / 10));
        }

        static int chooseNum(int min, int max)
        {
            int path = 0;
            try
            {
                path = int.Parse(Console.ReadLine());
                
                if(path < min || path > max) 
                {
                    throw new Exception();
                }
            }
            catch
            {
                writeString("Please, choose a valid number\n");
                path = chooseNum(min, max);
            }

            return path;
        }

        static void Wait(int wait)
        {
            Thread.Sleep(wait * 1000);
        }

        static void writeString(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(40);
            }
        }

        static string choosePokemon() 
        {
            int cnt = 1;
            writeString("\nYour pokemons:\n");
            foreach(string pokemon in pokemonInventory) 
            {
                writeString($"{cnt} - {pokemon}\n");
                cnt += 1;
            }

            writeString("Choose your pokemon: ");

            return pokemonInventory[chooseNum(1, pokemonInventory.Count)-1];
        }

        static void Game()
        {
            // Randomize första pokemonen
            string pokename = scrape.getPokemonFromType("any", 250, 350);
            scrape.initPokemon(pokename);
         
            /*
            foreach(int i in scrape.getPokemonStats()) 
            {
                Console.WriteLine(i);
            }
            Console.Write(pokename);*/
            writeString("Enter your name: ");
            string name = Console.ReadLine();
            Console.Clear();
            writeString($"You wake up in a flowery field. \nYou look down upon your jacket to see a nametag that says {name}.\n");
            Wait(1);
            writeString($"In front of you, you see a wild {pokename}!!! \nThe {pokename} looks agitated and ready to attack\n");
            Wait(1);
            writeString($"You reach into your backpack and find a pokeball. \nThe {pokename} is now charging towards you.\n");
            Wait(1);
            writeString($"You throw the pokeball at {pokename}, catching it.\n");
            pokemonInventory.Add(pokename);
            Wait(1);
            writeString($"You also find a health potion hidden in the grass\n");
            Wait(1);
            writeString($"The health potion is added to your inventory");
            Wait(1);
            Console.Clear();
            Wait(1);
            healthpotion += 1;
            Wait(1);
            writeString($"You continue your journey until you end up at a crossroads\n");
            Wait(1); 
            writeString($"On the right path you see a tower of smoke coming from the forest while on the left you can hear running water\n");
            Wait(1); 
            writeString($"Will you take the right path(1) or the left path (2)? ");
            int WhichPath = chooseNum(1, 2);
            Console.Clear();
            if (WhichPath == 1)
            {
                string enemy = scrape.getPokemonFromType("fire", 100, 350);
                writeString($"A wild {enemy} appears!\nIt starts to attack you\n"); 
                Fight(choosePokemon(), enemy);
            }
            if (WhichPath == 2)
            {
                // water type och typ hitta item shield + def
                string enemy = scrape.getPokemonFromType("water", 100, 350);
                writeString($"A wild {enemy} appears!\nIt starts to attack you\n");
                Fight(choosePokemon(), enemy);
            }
            /*
            writeString($"A hooded stranger comes up to you\n");
            Wait(1);
            writeString($"That was an epic battle he says \n");
            Wait(1);
            writeString($"for your troubles I will show you the way to the nearest village\n");
            Wait(1);
            writeString($"You arrive at the village to find it under attack from a huge snorlax\n"); // add pokemon or sum shit
            Wait(1);
            writeString($"Will you engage the Snorlax(1) or leave the villagers to their fate(2)\n");
            WhichPath = chooseNum(1, 2);
            if (WhichPath == 1)
            {
                scrape.initPokemon("snorlax");
                int[] snorlaxstats = scrape.getPokemonStats();
                writeString($"You run up to the Snorlax and startle it.");

                Fight(pokename, "snorlax");
                //make battle om vinst ger byborna ngt bra vapen och typ en permanent health boost + potions 
                //is a snorlax 
            }
            if (WhichPath == 2)
            {
                // evil du får reduced hp for ur actions
            }
            writeString($"You continue on your journey and find yourself getting hungry.\n");
            Wait(1);
            writeString($"You spot an apple tree in a small clearing and decide to head towards it.\n");
            Wait(1);
            writeString($"You pick an apple when suddenly a wild pokemon appears.\n");

            // random grass type

            writeString($"With barely having survived the battle and your belly full of apples your journey continues.\n");
            Wait(1);
            writeString($"You have wandered for hours and start to get tired.\n");
            Wait(1);
            writeString($"You see a field that looks like the perfect place to set up camp and rest for the night.\n");
            Wait(1);
            writeString($"You tread the field carefully not wanting to wake what may rest here.\n");
            Wait(1);
            writeString($"Suddenly you see a shadowy figure rise from the grassy field.\n");

            // ghost eller normal type pokebitch

            writeString("You set up camp and rest for the night and regain all your lost health.\n");
            Wait(1);
            writeString("You wake up to find the hooded stranger from before staring down at you.\n");
            Wait(1);
            writeString("You must be quite strong having defeated all those pokemon, he says.\n");
            Wait(1);
            writeString("Well, I do what I can sir, what is your name stranger?\n");
            Wait(1);
            writeString("That is not important right now, what is important is that i have a mission for you.\n");
            Wait(1);
            writeString("You shall travel to that mountain in the distance and kill the beast inside for a reward.\n");
            Wait(1);
            writeString("Then you shall meet me at the top of the mountain to claim your reward.\n");
            Wait(1);
            writeString("You accept the strangers quest and start heading towards the mountain.\n");*/
        }
    }
}