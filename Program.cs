using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using HtmlAgilityPack;
using System.Collections;


namespace TotalRankedPlayersByGuild
{
    class Program
    {
        static Dictionary<string, int> rankCount = new Dictionary<string, int>();

        [STAThread]
        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc;
            HtmlNodeCollection players;
            int pagecount = 1;
            string url = @"http://www.worldoflogs.com/guilds/1240/rankings/players/?page={0}";
            List<string> encounters = new List<string>() { "Magmaw", "Conclave of Wind", "Chimaeron", "Cho&#39;gall", "Maloriak", "Argaloth", "Valiona &amp; Theralion", "Omnotron Defense System", "Halfus Wyrmbreaker", "Nefarian", "Atramedes", "Al'Akir", "Twilight Ascendant Council" };
            do
            {
                doc = htmlWeb.Load(String.Format(url, pagecount));
                players = doc.DocumentNode.SelectNodes("//tr[@class='even' or @class='odd']");
                if (players != null)
                {
                    foreach (var player in players)
                    {
                        //check to see if it is a current fight
                        foreach (var encounter in encounters)
                        {
                            if (player.InnerText.Contains(encounter))
                            {
                                AddPlayer(player.SelectSingleNode("td[2]/a").InnerText);

                                List<string> listing = new List<string>();
                                listing.Add(player.SelectSingleNode("td[2]/a").InnerText.PadRight(20));
                                listing.Add(player.SelectSingleNode("td[6]").InnerText.PadRight(30));
                                listing.Add(player.SelectSingleNode("td[8]").InnerText);
                                listing.Add(player.SelectSingleNode("td[1]/a/span").InnerText);
                                Console.WriteLine(string.Join("\t", listing.ToArray()));
                            }
                        }
                    }
                }
                pagecount++;

            } while (players != null);

            var sortedList = (from entry in rankCount orderby entry.Value descending select entry);
            foreach (var player in sortedList)
            {
                Console.WriteLine(player.Key.PadRight(12) + " " + player.Value);
            }
            Console.ReadLine();

        }

        static void AddPlayer(string Name)
        {
            if (!rankCount.ContainsKey(Name))
            {
                rankCount.Add(Name, 1);
            }
            else
            {
                rankCount[Name] = rankCount[Name] + 1;
            }
        }

    }
}