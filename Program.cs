using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace TotalRankedPlayersByGuild
{
    class Program
    {
        static Dictionary<string, int> rankCount = new Dictionary<string, int>();

        [STAThread]
        static void Main(string[] args)
        {

            IE ie = new IE("http://www.worldoflogs.com/guilds/24254/rankings/players/");

            var playerRankTable = ie.Table(Find.ByClass("playerRankMixed"));
            while (playerRankTable.TableRows.Count > 1)
            {
                foreach (var curRow in playerRankTable.TableBodies[0].TableRows)
                {
                    if (curRow.TableCells.Count > 1)
                    {
                        AddPlayer(curRow.TableCells[1].Text);
                    }
                }
                ie.Link(Find.ByText("more")).Click();
            }
            ie.Close();

            foreach (var name in rankCount.Keys)
            {
                Console.WriteLine(name + "\t" + rankCount[name]);
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
