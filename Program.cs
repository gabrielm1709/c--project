using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace grupp_projekt_prog2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Web scraper. 
            int[] stats = new int[6];
            string[] statstext = new string[] {"HP", "Attack", "Defense", "Special attack", "Special defence", "Speed"};  
            string pokemon = "charizard";
            HtmlWeb web = new HtmlWeb();
            string url = "https://pokemondb.net/pokedex/" + pokemon;
            HtmlDocument doc = web.Load(url);
            var nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("cell-num"));
            var name = doc.DocumentNode.Descendants("h1");
            foreach (var n in name) 
            {
                pokemon = n.InnerText;
                break;
            }
            Console.WriteLine(pokemon);
            int cnt = 0;
            foreach (var stat in nodes)
            {
                if (cnt < 16 && cnt % 3 == 0)
                {
                    stats[cnt / 3] = int.Parse(stat.InnerText);
                }
                cnt += 1;
            }

            for(int i=0;i<6;i++)
            {
                Console.WriteLine($"{statstext[i]}: {stats[i]}");
            }
        }
        static void RND(int args)
        {
            Random rnd = new Random();
            int rand = rnd.Next(1, 898);

        }
        static void Game(string n, int i, int[] stats)
        {

            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine($"You wake up in a flowery field \n You look down upon your jacket to see a nametag \n the nametag says {name} \n In front of you 3 wild pokemon appear");
        }

        static string[] getStatsByName(int id)
        {
            string[] stats = new string[6];
            return stats;
        }
        static string[] getStatsById(int id) 
        {
            string[] stats = new string[6];
            return stats;
        }
    }
}
        