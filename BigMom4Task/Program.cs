using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GMA4Task
{
    class Program
    {
        private static string pathToTreasure = null;
        private static HashSet<string> positionsSearched = new HashSet<string>();

        private static int MaxCount = 0;

        private static void Search(char[] chestsOpen, char[] keyTypesAvailable, int[] chests_keyTypeToOpen, List<int>[] chests_keysInside, int[] moves, int move)
        {
            if (pathToTreasure != null) return;//stop search

            // need to keep track of all possible states
            // state means chests open and key types available
            StringBuilder sb = new StringBuilder();
            sb.Append(chestsOpen);

            for (int i = 0; i < keyTypesAvailable.Length; i++)
            {
                if (keyTypesAvailable[i] != 0)
                {
                    sb.Append(Convert.ToChar(i)).Append(keyTypesAvailable[i]);
                }
            }

            string positionID = sb.ToString();
            if (positionsSearched.Contains(positionID)) return;

            positionsSearched.Add(positionID);

            if (positionsSearched.Count > MaxCount)
            {
                MaxCount = positionsSearched.Count;
            }

            if (move == moves.Length)
            {
                pathToTreasure = "";
                for (int i = 0; i < moves.Length; i++)
                {
                    if (pathToTreasure != "") pathToTreasure += " ";
                    pathToTreasure += moves[i] + 1;

                }
                return;
            }

            for (int i = 0; i < chestsOpen.Length; i++)
            {
                if (chestsOpen[i] == 0)
                {
                    //try to open
                    int keyTypeToOpen = chests_keyTypeToOpen[i];

                    if (keyTypesAvailable[keyTypeToOpen] > 0)
                    {
                        //make move
                        moves[move] = i;
                        chestsOpen[i] = 'X';
                        keyTypesAvailable[keyTypeToOpen]--;

                        //read all keys inside chest
                        foreach (int keyInside in chests_keysInside[i])
                        {
                            keyTypesAvailable[keyInside]++;
                        }

                        Search(chestsOpen, keyTypesAvailable, chests_keyTypeToOpen, chests_keysInside, moves, move + 1);

                        //undo move

                        foreach (int keyInside in chests_keysInside[i])
                        {
                            keyTypesAvailable[keyInside]--;
                        }
                        keyTypesAvailable[keyTypeToOpen]++;
                        chestsOpen[i] = Convert.ToChar(0);
                        moves[move] = 0;
                    }
                }
            }
        }//Search 


        private static void PreliminarySearch(char[] chestsOpen, char[] keyTypesAvailable, int[] chests_keyTypeToOpen, List<int>[] chests_keysInside)
        {
            //initial DFS to avoid stuck in dead sub-graphs

            for (int i = 0; i < chestsOpen.Length; i++)
            {
                if (chestsOpen[i] == 0)
                {
                    //try to open
                    int keyTypeToOpen = chests_keyTypeToOpen[i];
                    if (keyTypesAvailable[keyTypeToOpen] > 0)
                    {
                        //make move
                        chestsOpen[i] = 'X';

                        //read all keys inside the chest, add them to available keys array
                        foreach (int keyInside in chests_keysInside[i])
                        {
                            keyTypesAvailable[keyInside]++;
                        }

                        PreliminarySearch(chestsOpen, keyTypesAvailable, chests_keyTypeToOpen, chests_keysInside);
                    }
                }
            }
        }

        private static bool PreliminarySearchImpossible(char[] KTA, int[] chests_keyTypeToOpen, List<int>[] chests_keyInside)
        {
            char[] chestsOpen = new char[chests_keyTypeToOpen.Length];//by default false, all chests are closed
            char[] keyTypesAvailable = new char[KTA.Length];
            KTA.CopyTo(keyTypesAvailable, 0);

            PreliminarySearch(chestsOpen, keyTypesAvailable, chests_keyTypeToOpen, chests_keyInside);

            for (int i = 0; i < chestsOpen.Length; i++)
            {
                if (chestsOpen[i] == 0) return true;  // impossible to find solution

            }
            return false;
        }

        static void Main(string[] args)
        {
            try
            {
                int started = Environment.TickCount;
                string path = @"D:\Games\GM\";
                string fileIn = "input.txt";
                string fileOut = "output.txt";

                StreamReader sr = new StreamReader(path + fileIn);
                StreamWriter sw = new StreamWriter(path + fileOut);

                int testCount = Convert.ToInt32(sr.ReadLine());

                for (int t = 0; t < testCount; t++)
                {
                    string[] splitted = sr.ReadLine().Split(' ');

                    int keyCount = Convert.ToInt32(splitted[0]);
                    int chestCount = Convert.ToInt32(splitted[1]);

                    char[] keyTypesAvailable = new char[201];//All chest types and key types will be integers between 1 and 200 inclusive. keys available to me

                    splitted = sr.ReadLine().Split(' ');
                    if (splitted.Length != keyCount) throw new ApplicationException("Wrong!");

                    for (int i = 0; i < keyCount; i++)
                    {
                        int keyType = Convert.ToInt32(splitted[i]);
                        keyTypesAvailable[keyType]++;
                    }

                    int[] chests_keyTypeToOpen = new int[chestCount];
                    List<int>[] chests_keysInside = new List<int>[chestCount];
                    for (int i = 0; i < chestCount; i++)
                    {
                        //read each chest info
                        splitted = sr.ReadLine().Split(' ');
                        chests_keyTypeToOpen[i] = Convert.ToInt32(splitted[0]);

                        int keysinsideChest = Convert.ToInt32(splitted[1]);

                        if (keysinsideChest != splitted.Length - 2) throw new ApplicationException("Wrong");

                        chests_keysInside[i] = new List<int>();
                        for (int j = 0; j < keysinsideChest; j++) 
                        {
                            int availableKey = Convert.ToInt32(splitted[j + 2]);
                            chests_keysInside[i].Add(availableKey);
                        }
                    }

                    // start full search
                    // make a node which describes a set (keys available, chests open / still closed)
                    // move means -> take a first closed chest, try to open -> transfer to a new state. keep transition info

                    pathToTreasure = null;
                    positionsSearched.Clear();

                    bool impossible = PreliminarySearchImpossible(keyTypesAvailable, chests_keyTypeToOpen, chests_keysInside);

                    if (!impossible)
                    {
                        char[] chestsOpen = new char[chests_keyTypeToOpen.Length];  // by default false - all closed
                        int[] moves = new int[chests_keyTypeToOpen.Length];
                        Search(chestsOpen, keyTypesAvailable, chests_keyTypeToOpen, chests_keysInside, moves, 0);
                    }

                    string message = pathToTreasure ?? "IMPOSSIBLE";
                    sw.WriteLine("Case #{0}: {1}", (t + 1), message);
                }


                Console.WriteLine("Dictionary count: " + MaxCount);

                sr.Close();
                sw.Close();

                int elapsed = Environment.TickCount - started;
                Console.WriteLine("Time elapsed: " + elapsed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}