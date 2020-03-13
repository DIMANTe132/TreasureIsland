using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace TreasureIsland
{
    class File
    {
        public void ReadFile()
        {
            string Path = @"C:\Users\qweds\Desktop\dotnet.course\Task1\TestData\HardMap.txt";

            try
            {
                string Str;

                ArrayList Base = new ArrayList();
                ArrayList Water = new ArrayList();
                ArrayList Bridge = new ArrayList();
                Coord Treasure = new Coord();
                ArrayList Doors = new ArrayList();

                int MaxX = 0;
                int MaxY = 0;

                using (StreamReader sr = new StreamReader(Path, System.Text.Encoding.Default))
                {
                    while ((Str = sr.ReadLine()) != null)
                    {
                        if (Str == "")
                            continue;

                        string[] SplitStr = Str.Split(new char[] { ' ' });

                        string StrWithoutSpaces = "";
                        foreach (string str in SplitStr)
                            StrWithoutSpaces += str;

                        string Parameters = "";
                        string FirstCoordinatesStr = "";
                        string SecondCoordinatesStr = "";

                        SplitStr = StrWithoutSpaces.Split(new char[] { '(' });
                        Parameters = SplitStr[1];
                        Parameters = Parameters.Substring(0, Parameters.Length - 1);

                        if (SplitStr[0] == "BASE")
                        {
                            SplitStr = Parameters.Split(new char[] { ':' });

                            FirstCoordinatesStr = SplitStr[0];
                            SecondCoordinatesStr = SplitStr[1];

                            string[] FirstCoordinatesArr = FirstCoordinatesStr.Split(new char[] { ',' });
                            string[] SecondCoordinatesArr = SecondCoordinatesStr.Split(new char[] { ',' });

                            for (int i = 0; i < FirstCoordinatesArr.Length; i++)
                                CheckCoord(ref FirstCoordinatesArr[i], ref SecondCoordinatesArr[i]);

                            if ((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) % 2 == 0)
                            {
                                Coord FirstDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2,
                                                            Convert.ToInt32(FirstCoordinatesArr[1]));
                                Coord SecondDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2,
                                                            Convert.ToInt32(SecondCoordinatesArr[1]));
                                Doors.Add(FirstDoor);
                                Doors.Add(SecondDoor);
                            }

                            else
                            {
                                Coord FirstDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2,
                                                            Convert.ToInt32(FirstCoordinatesArr[1]));
                                Coord SecondDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2 + 1,
                                                            Convert.ToInt32(FirstCoordinatesArr[1])); ;
                                Coord ThirdDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2,
                                                            Convert.ToInt32(SecondCoordinatesArr[1])); ;
                                Coord FourthDoor = new Coord((Convert.ToInt32(FirstCoordinatesArr[0]) + Convert.ToInt32(SecondCoordinatesArr[0])) / 2 + 1,
                                                            Convert.ToInt32(SecondCoordinatesArr[1])); ;
                                Doors.Add(FirstDoor);
                                Doors.Add(SecondDoor);
                                Doors.Add(ThirdDoor);
                                Doors.Add(FourthDoor);
                            }

                            if ((Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) % 2 == 0)
                            {
                                Coord FirstDoor = new Coord(Convert.ToInt32(FirstCoordinatesArr[0]),
                                                            (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2);
                                Coord SecondDoor = new Coord(Convert.ToInt32(SecondCoordinatesArr[0]),
                                                             (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2);
                                Doors.Add(FirstDoor);
                                Doors.Add(SecondDoor);
                            }

                            else
                            {
                                Coord FirstDoor = new Coord(Convert.ToInt32(FirstCoordinatesArr[0]), 
                                                           (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2);
                                Coord SecondDoor = new Coord(Convert.ToInt32(FirstCoordinatesArr[0]),
                                                            (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2 + 1); ;
                                Coord ThirdDoor = new Coord(Convert.ToInt32(SecondCoordinatesArr[0]),
                                                           (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2); ;
                                Coord FourthDoor = new Coord(Convert.ToInt32(SecondCoordinatesArr[0]),
                                                            (Convert.ToInt32(FirstCoordinatesArr[1]) + Convert.ToInt32(SecondCoordinatesArr[1])) / 2 + 1); ;
                                Doors.Add(FirstDoor);
                                Doors.Add(SecondDoor);
                                Doors.Add(ThirdDoor);
                                Doors.Add(FourthDoor);
                            }


                            for (int i = Convert.ToInt32(FirstCoordinatesArr[0]); i <= Convert.ToInt32(SecondCoordinatesArr[0]); i++)
                            {
                                for (int j = Convert.ToInt32(FirstCoordinatesArr[1]); j <= Convert.ToInt32(SecondCoordinatesArr[1]); j++)
                                {
                                    Coord CoordOfBase = new Coord(i, j);
                                    Base.Add(CoordOfBase);
                                    if (CoordOfBase.x > MaxX)
                                        MaxX = CoordOfBase.x;
                                    if (CoordOfBase.y > MaxY)
                                        MaxY = CoordOfBase.y;
                                }
                            }
                        }

                        else if (SplitStr[0] == "bridge")
                        {
                            SplitStr = Parameters.Split(new char[] { ',' });
                            
                            CheckCoord(ref SplitStr[0], ref SplitStr[0]);

                            Coord CoordOfBridge = new Coord(Convert.ToInt32(SplitStr[0]), Convert.ToInt32(SplitStr[1]));
                            Bridge.Add(CoordOfBridge);
                            if (CoordOfBridge.x > MaxX)
                                MaxX = CoordOfBridge.x;
                            if (CoordOfBridge.y > MaxY)
                                MaxY = CoordOfBridge.y;
                        }

                        else if (SplitStr[0] == "Treasure")
                        {
                            SplitStr = Parameters.Split(new char[] { ',' });

                            CheckCoord(ref SplitStr[0], ref SplitStr[0]);

                            Treasure.x = Convert.ToInt32(SplitStr[0]);
                            Treasure.y = Convert.ToInt32(SplitStr[1]);
                            if (Treasure.x > MaxX)
                                MaxX = Treasure.x;
                            if (Treasure.y > MaxY)
                                MaxY = Treasure.y;
                        }

                        else if (SplitStr[0] == "WATER")
                        {
                            int X = 0;
                            int Y = 0;
                            Parameters = Parameters.Replace("->", "-");
                            SplitStr = Parameters.Split(new char[] { '-' });
                            for (int i = 0; i <= SplitStr.Length - 1; i++)
                            {
                                string[] SplitParameters = SplitStr[i].Split(new char[] { ',' });

                                CheckCoord(ref SplitParameters[0], ref SplitParameters[1]);

                                if (i == 0)
                                    CreateNewWater(Water, Convert.ToInt32(SplitParameters[0]), Convert.ToInt32(SplitParameters[1]), ref MaxX, ref MaxY);

                                if (i > 0)
                                {
                                    int dx = (Convert.ToInt32(SplitParameters[0]) > X) ? (Convert.ToInt32(SplitParameters[0]) - X) :
                                        (X - Convert.ToInt32(SplitParameters[0]));
                                    int dy = (Convert.ToInt32(SplitParameters[1]) > Y) ? (Convert.ToInt32(SplitParameters[1]) - Y) :
                                        (Y - Convert.ToInt32(SplitParameters[1]));

                                    int sx = (Convert.ToInt32(SplitParameters[0]) >= X) ? (1) : (-1);
                                    int sy = (Convert.ToInt32(SplitParameters[1]) >= Y) ? (1) : (-1);

                                    if (dy < dx)
                                    {
                                        int d = (dy << 1) - dx;
                                        int d1 = dy << 1;
                                        int d2 = (dy - dx) << 1;
                                        int x = X + sx;
                                        int y = Y;
                                        for (int j = 1; j <= dx; j++)
                                        {
                                            if (d > 0)
                                            {
                                                d += d2;
                                                y += sy;
                                            }
                                            else
                                            {
                                                d += d1;
                                            }
                                            CreateNewWater(Water, x, y, ref MaxX, ref MaxY);
                                            x += sx;
                                        }
                                    }
                                    else
                                    {
                                        int d = (dx << 1) - dy;
                                        int d1 = dx << 1;
                                        int d2 = (dx - dy) << 1;
                                        int x = X;
                                        int y = Y + sy;
                                        for (int j = 1; j <= dy; j++)
                                        {
                                            if (d > 0)
                                            {
                                                d += d2;
                                                x += sx;
                                            }
                                            else
                                            {
                                                d += d1;
                                            }
                                            CreateNewWater(Water, x, y, ref MaxX, ref MaxY);
                                            y += sy;
                                        }
                                    }
                                }
                                X = Convert.ToInt32(SplitParameters[0]);
                                Y = Convert.ToInt32(SplitParameters[1]);
                            }
                        }
                    }
                }
                Map.CreateMap(Base, Water, Treasure, Bridge, MaxX, MaxY, Doors);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CheckCoord(ref string x,ref string y)
        {
            int Num;
            if (!Int32.TryParse(x, out Num))
            {
                Regex my_reg = new Regex(@"\D");
                x = my_reg.Replace(x, "");
                if (!Int32.TryParse(x, out Num) || String.IsNullOrEmpty(x))
                {
                    throw new Exception($"Value '{x}' in the file is not a number");
                }
            }
            if (!Int32.TryParse(y, out Num))
            {
                Regex my_reg = new Regex(@"\D");
                y = my_reg.Replace(x, "");
                if (!Int32.TryParse(y, out Num) || String.IsNullOrEmpty(y))
                {
                    throw new Exception($"Value '{y}' in the file is not a number");
                }
            }
        }

        private void CreateNewWater(ArrayList Water, int x, int y, ref int MaxX, ref int MaxY)
        {
            Coord CoordOfWater = new Coord(x, y);
            Water.Add(CoordOfWater);
            if (CoordOfWater.x > MaxX)
                MaxX = CoordOfWater.x;
            if (CoordOfWater.y > MaxY)
                MaxY = CoordOfWater.y;
        }
    }
}
