using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace projekt_scraper
{
    class Scraper
    {
        public HtmlWeb web = new HtmlWeb();
        public HtmlDocument doc = null;

        public void initPokemon(string pokemon)
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

        public string getPokemonFromType(string type, int minScore, int maxScore)
        {
            doc = web.Load("https://pokemondb.net/pokedex/all");
            List<string> pokemons = new List<string>();
            var nodes = doc.DocumentNode.SelectNodes("//tr");

            foreach (var node in nodes.Skip(1))
            {
                string name = "";
                bool correctType = false;
                int score = 0;
                foreach (var n in node.Descendants())
                {
                    if (n.HasClass("ent-name"))
                    {
                        name = n.InnerText;
                    }

                    if (n.HasClass($"type-{type}") || type == "any")
                    {
                        correctType = true;
                    }

                    if (n.HasClass("cell-total"))
                    {
                        score = int.Parse(n.InnerText);
                    }
                }

                if (correctType && score <= maxScore && score >= minScore)
                {
                    pokemons.Add(name);
                }
            }

            Random rnd = new Random();

            string pokename = pokemons[rnd.Next(0, pokemons.Count() - 1)];

            return pokename;
        }
    }
}