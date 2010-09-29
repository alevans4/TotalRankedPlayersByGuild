using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using HtmlAgilityPack;


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
            string url = @"http://www.worldoflogs.com/guilds/24254/rankings/players/?page={0}";

            do
            {
                doc = htmlWeb.Load(String.Format(url, pagecount));
                players = doc.DocumentNode.SelectNodes("//tr[@class='even' or @class='odd']/td[2]/a");
                if (players != null)
                {
                    foreach (var player in players)
                    {
                        AddPlayer(player.InnerText);
                    }
                }
                pagecount++;

            } while (players != null);

            var sortedList = (from entry in rankCount orderby entry.Value descending select entry);
            foreach (var player in sortedList)
            {
                Console.WriteLine(player.Key + "\t" + player.Value);
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