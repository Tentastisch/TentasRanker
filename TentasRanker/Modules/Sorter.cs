using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentasRanker.Pages;

namespace TentasRanker.Modules
{
    internal class Sorter
    {
        private static Random random = new Random();

        public static List<string> list = new List<string>();
        public static List<string> sortList = new List<string>();
        private static List<int> already = new List<int>();

        public static int rounds = 2;
        private static int roundsAlready = 1;

        public static int left = 0;
        public static int right = 1;

        public static void ChangeRounds(string text)
        {
            if (int.TryParse(text, out rounds))
                rounds = int.Parse(text);
            else
                rounds = 2;
        }

        public static void StartSort()
        {
            sortList = new List<string>(list);
            NewRound();
        }

        public static void NewRound()
        {
            var rand = 0;

            while (true)
            {
                rand = random.Next(0, list.Count);

                if (list.Count == already.Count)
                {
                    list = new List<string>(sortList);
                    if (roundsAlready == rounds)
                    {
                        MainWindow.FinishSorting();
                        return;
                    }

                    roundsAlready++;
                    already.Clear();
                    Sort.ChangeRound(roundsAlready);
                    NewRound();
                    return;
                }

                if (!already.Contains(rand))
                    break;
            }

            left = rand;
            already.Add(rand);

            int rightRand = 0;
            while (true)
            {
                rightRand = random.Next(0, list.Count);

                if (rand != rightRand)
                    break;
            }

            right = rightRand;
            Sort.SetLeft(list[rand]);
            Sort.SetRight(list[rightRand]);
        }

        public static void FinishRound(bool leftWon)
        {
            var gotLeftText = list[left];
            var gotRightText = list[right];

            var posLeft = sortList.FindIndex(x => x.Equals(gotLeftText));
            var posRight = sortList.FindIndex(x => x.Equals(gotRightText));

            if (leftWon)
            {
                if (left > right)
                {
                    var leftText = sortList[posLeft];
                    var rightText = sortList[posRight];

                    sortList[posRight] = leftText;
                    sortList[posLeft] = rightText;
                }
            }
            else
            {
                if (left < right)
                {
                    var leftText = sortList[posLeft];
                    var rightText = sortList[posRight];

                    sortList[posRight] = leftText;
                    sortList[posLeft] = rightText;
                }
            }

            NewRound();
        }
    }
}
