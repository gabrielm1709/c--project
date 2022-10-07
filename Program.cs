using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace grupp_projekt_prog2
{
    public class Scraper
    {
        public HtmlWeb web = new HtmlWeb();
        public HtmlDocument doc = null; 
      
        public void pokemonInit(string pokemon) 
        {
            doc = web.Load($"https://pokemondb.net/pokedex/{pokemon}");
        }

        public string getPokemonName()
        { 
            return doc.DocumentNode.SelectSingleNode("//h1").InnerText;
        } 

        public int[] getPokemonStats() 
        {
            int[] stats = new int[6];
            var nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("cell-num"));
        
            int cnt = 0;
            foreach (var stat in nodes)
            {
                if (cnt < 16 && cnt % 3 == 0)
                {
                    stats[cnt / 3] = int.Parse(stat.InnerText); 
                }
                cnt += 1;
            }

            return stats;
        }
    } 

    class Program
    {
        static void Main(string[] args)
        {
            Scraper scrap = new Scraper();
            Random rnd = new Random();
            // Web scraper. 
       
            string[] statstext = new string[] { "HP", "Attack", "Defense", "Special attack", "Special defence", "Speed" };
            scrap.pokemonInit(char.Parse(rnd.Next(1, 898)));
            string name = scrap.getPokemonName();
            int[] stats = scrap.getPokemonStats();
           
            foreach(int stat in stats)    
            {
                Console.WriteLine(stat);
            } 

            //Game(rand, stats, pokename);
        }
        static void RND(int args)
        {

            Random rnd = new Random();
            int rand = rnd.Next(1, 898);


        }
         
        static void Attack(int i, int[] stats, string pokename, int[] playerstats)
        {
            int playerhp = 1; 
            while( stats[1] > 0 || playerhp > 0)
                {
                Console.WriteLine("Do you want to Attack(1), Rest(2), Bag(3)");
                int playerchoise = int.Parse(Console.ReadLine());
                if (playerchoise == 1)
                {
                    
                }
                if (playerchoise == 2)
                {

                }
                if (playerchoise == 3)
                {
                    foreach(string in inventory)
                    {

                    }
                }
        }
        }
        static void Game(int i, int[] stats, string pokename)
        {
            int[] playerstats = { 100, 10, 1,  1};
            //                hp  atk  def  crt chans
            List<string> inventory = new List<string>();  
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine($"You wake up in a flowery field \nYou look down upon your jacket to see a nametag that says {name}. ");
            Thread.Sleep(1000);
            Console.WriteLine($"In front of you, you see a wild {pokename}!!!\n The {pokename} looks agitated and ready to attack ");
            Thread.Sleep(1000);
            Console.WriteLine($"You reach into your backpack and find a sharp rock \nThe {pokename} is now charging towards you. ");
            Thread.Sleep(1000);
            Console.WriteLine($"You shank the {pokename} with your sharp rock dealing critical damage ");

            Console.WriteLine($"You take a tooth from the now dead {pokename} +2 damage \nYou also find a smal health potion hidden in the grass");

            Console.WriteLine($"The small health potion is added to your inventory");

            playerstats[1]+=2;
            inventory.Add("Small health potion");
            


        }
    }
}