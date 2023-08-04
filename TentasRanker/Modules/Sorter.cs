using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using TentasRanker.Pages;

namespace TentasRanker.Modules
{
    internal class Sorter
    {
        public static List<string> list = new List<string>();
        public static List<string> sortList = new List<string>();

        private static List<PivotTwo> pivotList = new List<PivotTwo>();

        public static int currentPivot = 0;
        public static int currently = 0;
        public static int allSorted = 0;

        private static int alreadyRounds = 1;

        public static void Start()
        {
            var pivotTwo = new PivotTwo();
            for (int i = 0; i < list.Count;  i++)
            {
                if (i % 2 == 0)
                {
                    pivotTwo.first = list[i];
                }
                else
                {
                    pivotTwo.second = list[i];
                    pivotList.Add(pivotTwo);
                    pivotTwo = new PivotTwo();
                }
            }

            for (int i = 0; i < pivotList.Count;i++)
            {
                Trace.WriteLine($"{pivotList[i].first} {pivotList[i].second}");
            }

            NewRound();
        }

        public static void NewRound()
        {
            for (int i = 0; i < pivotList.Count; i++)
            {
                Trace.WriteLine($"{pivotList[i].first} {pivotList[i].second}");
            }
            Trace.WriteLine($"\n");

            if (pivotList.Count - 1 == allSorted)
            {
                Finish();
            }

            if (currentPivot == pivotList.Count && alreadyRounds == 1)
            {
                for (int i = 0; i < pivotList.Count; i++)
                {
                    sortList.Add(pivotList[i].first);
                    sortList.Add(pivotList[i].second);
                    sortList.Add("\n");
                }

                File.WriteAllLines($"./{alreadyRounds}.txt", sortList);
                sortList.Clear();
                currentPivot = 0;
                alreadyRounds++;
                Sort.ChangeRound(alreadyRounds);
            }
            else if (currentPivot == pivotList.Count - 1 && alreadyRounds > 1)
            {
                for (int i = 0; i < pivotList.Count; i++)
                {
                    sortList.Add(pivotList[i].first);
                    sortList.Add(pivotList[i].second);
                    sortList.Add("\n");
                }

                File.WriteAllLines($"./{alreadyRounds}.txt", sortList);
                sortList.Clear();
                currently = 0;
                currentPivot = 0;
                alreadyRounds++;
                Sort.ChangeRound(alreadyRounds);
            }

            if (alreadyRounds == 1)
            {
                var pivot = pivotList[currentPivot];
                Sort.SetLeft(pivot.first);
                Sort.SetRight(pivot.second);
            }
            else
            {
                PivotTwo pivot = pivotList[currentPivot];
                PivotTwo nextPivot = null;
                if (currentPivot + 1 < pivotList.Count)
                {
                    nextPivot = pivotList[currentPivot + 1];
                }
                else
                {
                    nextPivot = pivotList[currentPivot - 1];
                }

                if (currently == 0)
                {
                    Sort.SetLeft(pivot.first);
                    Sort.SetRight(nextPivot.second);
                }
                else if (currently == 1)
                {
                    Sort.SetLeft(pivot.second);
                    Sort.SetRight(nextPivot.first);
                }
                else if (currently == 2)
                {
                    Sort.SetLeft(pivotList[currentPivot].first);
                    Sort.SetRight(pivotList[currentPivot].second);
                }
                else if (currently == 3)
                {
                    Sort.SetLeft(pivotList[currentPivot + 1].first);
                    Sort.SetRight(pivotList[currentPivot + 1].second);
                }

                
            }
        }

        public static void FinishRound(bool rightSelected)
        {
            if (rightSelected)
            {
                if (alreadyRounds == 1)
                {
                    var temp = pivotList[currentPivot].first;
                    pivotList[currentPivot].first = pivotList[currentPivot].second;
                    pivotList[currentPivot].second = temp;
                    currentPivot++;
                }
                else if (currently == 0)
                {
                    var tempPivot = new PivotTwo(pivotList[currentPivot].first, pivotList[currentPivot].second);
                    pivotList[currentPivot].first = pivotList[currentPivot + 1].first;
                    pivotList[currentPivot].second = pivotList[currentPivot + 1].second;

                    pivotList[currentPivot + 1].first = tempPivot.first;
                    pivotList[currentPivot + 1].second = tempPivot.second;
                    currently = 0;
                    currentPivot++;
                    allSorted = 0;
                }
                else if (currently == 1)
                {
                    var tempPivot = new PivotTwo(pivotList[currentPivot].first, pivotList[currentPivot].second);
                    pivotList[currentPivot].second = pivotList[currentPivot + 1].first;
                    pivotList[currentPivot + 1].first = tempPivot.second;

                    currently++;
                    allSorted = 0;
                }
                else if (currently == 2)
                {
                    var temp = pivotList[currentPivot].first;
                    pivotList[currentPivot].first = pivotList[currentPivot].second;
                    pivotList[currentPivot].second = temp;

                    currently++;
                    allSorted = 0;
                }
                else if (currently == 3)
                {
                    var temp = pivotList[currentPivot + 1].first;
                    pivotList[currentPivot + 1].first = pivotList[currentPivot + 1].second;
                    pivotList[currentPivot + 1].second = temp;
                    currently = 0;
                    currentPivot++;
                }
                else
                {
                    currently++;
                    allSorted = 0;
                } 
            }
            else
            {
                if (alreadyRounds == 1)
                {
                    currentPivot++;
                    allSorted = 0;
                }
                else if (currently == 0)
                {
                    currentPivot++;
                    allSorted++;
                }
                else if (currently == 1)
                {
                    currently = 0;
                    currentPivot++;
                    allSorted = 0;
                }
                else if (currently == 3)
                {
                    currently = 0;
                    currentPivot++;
                    allSorted = 0;
                }
                else
                {
                    currently++;
                    allSorted = 0;
                }
            }

            NewRound();
        }

        public static void Finish()
        {
            for (int i = 0; i < pivotList.Count; i++)
            {
                sortList.Add(pivotList[i].first);
                sortList.Add(pivotList[i].second);
            }

            MainWindow.FinishSorting();
        }

        public static void Reset()
        {
            list = new List<string>();
            sortList = new List<string>();

            pivotList = new List<PivotTwo>();

            currentPivot = 0;
            currently = 0;
            allSorted = 0;

            alreadyRounds = 1;
            MainWindow.Reset();
        }
    }
}
