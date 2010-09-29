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
            string url = @"http://www.worldoflogs.com/guilds/24254/rankings/players/?page={0}";
            int pagecount = 1;

            HtmlDocument doc = htmlWeb.Load(String.Format(url, pagecount));
            var players = doc.DocumentNode.SelectNodes("//tr[@class='even' or @class='odd']/td[2]/a");
            while (players != null)
            {
                foreach (var player in players)
                {
                    //Console.WriteLine(player.InnerText);
                    AddPlayer(player.InnerText);
                }
                pagecount++;
                doc = htmlWeb.Load(String.Format(url, pagecount));
                players = doc.DocumentNode.SelectNodes("//tr[@class='even' or @class='odd']/td[2]/a");
            }

            foreach (var player in rankCount.Keys)
            {
                Console.WriteLine(player + "\t" + rankCount[player]);
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