using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TreasureIsland
{
    class Map
    {
        public static void CreateMap(ArrayList Base, ArrayList Water, Coord Treasure, ArrayList Bridge, int MaxX, int MaxY, ArrayList Doors)
        {
            string[,] Map = new string[MaxX + 1, MaxY + 1];

            for (int y = 0; y <= MaxY; y++)
            {
                for (int x = 0; x <= MaxX; x++)
                {
                    bool SpacesFlag = false;
                    bool CoordinatesFlag = false;

                    foreach (Coord B in Base)
                    {
                        if (B.x == x && B.y == y)
                        {
                            Map[x, y] = "@";
                            SpacesFlag = true;
                            CoordinatesFlag = true;
                            break;
                        }
                    }

                    foreach (Coord W in Water)
                    {
                        bool bridge = false;
                        foreach (Coord Br in Bridge)
                        {
                            if (Br.x == x && Br.y == y)
                            {
                                Map[x, y] = "#";
                                SpacesFlag = true;
                                CoordinatesFlag = true;
                                bridge = true;
                                break;
                            }
                        }
                        if (W.x == x && W.y == y && bridge == false)
                        {
                            Map[x, y] = "~";
                            SpacesFlag = true;
                            CoordinatesFlag = true;
                            break;
                        }
                        if (bridge == true)
                            break;
                    }

                    if (Treasure.x == x && Treasure.y == y)
                    {
                        Map[x, y] = "+";
                        SpacesFlag = true;
                    }

                    if (x == 0 && CoordinatesFlag == false)
                    {
                        Map[x, y] = (y % 10).ToString();
                        SpacesFlag = true;
                    }

                    else if (y == 0 && CoordinatesFlag == false)
                    {
                        Map[x, y] = (x % 10).ToString();
                        SpacesFlag = true;
                    }

                    if (SpacesFlag == false)
                        Map[x, y] = " ";
                }
            }
            ArrayList way = new ArrayList();
            foreach (Coord door in Doors)
            {
                if (way.Count == 0)
                    Way.CreatePath(Map, ref way, door, Treasure, MaxX, MaxY);
                else
                {
                    ArrayList NewWay = new ArrayList();
                    Way.CreatePath(Map, ref NewWay, door, Treasure, MaxX, MaxY);
                    if (way.Count > NewWay.Count && NewWay.Count != 0)
                        way = NewWay;
                }
            }
            if (way.Count == 0)
                throw new System.ArgumentException("The path cannot be built");
            else
            {
                foreach (Coord w in way)
                {
                    if (Map[w.x, w.y] == " " || Map[w.x, w.y] == "@")
                        Map[w.x, w.y] = "%";
                }
            }

            PrintFinalMap(Map, MaxX, MaxY);
        }
        private static void PrintFinalMap(string [,] Map, int MaxX, int MaxY)
        {
            Console.SetWindowSize(MaxX + 2, MaxY + 2);
            for (int y = 0; y <= MaxY; y++)
            {
                for (int x = 0; x <= MaxX; x++)
                {
                    Console.Write(Map[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}