using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TreasureIsland
{
    class Way
    {
        struct Node
        {
            public int x, y, parentX, parentY, gCost, hCost, fCost;
            public Node(int X, int Y, int ParentX, int ParentY, int GCost, int HCost)
            {
                this.x = X;
                this.y = Y;
                this.parentX = ParentX;
                this.parentY = ParentY;
                this.gCost = GCost;
                this.hCost = HCost;
                this.fCost = GCost + HCost;
            }
        }
        public static void CreatePath(string[,] Map, ref ArrayList Way, Coord door, Coord Treasure, int MaxX, int MaxY)
        {
            int W = MaxX + 1;
            int H = MaxY + 1;

            int[,] WhereWeCan = new int[W, H];

            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                    if (Map[x, y] == " " || Map[x, y] == "#")
                        WhereWeCan[x, y] = 0;
                    else
                        WhereWeCan[x, y] = 1;

            ArrayList ActivNode = new ArrayList();
            ArrayList DisActivNode = new ArrayList();
            Node Start = new Node(door.x, door.y, -1, -1, 0, Math.Abs(Treasure.x - door.x) + Math.Abs(Treasure.y - door.y));
            ActivNode.Add(Start);
            Node MinNode;

            while (ActivNode.Count != 0)
            {
                MinNode = (Node)ActivNode[0];
                foreach (Node node in ActivNode)
                {
                    if (MinNode.fCost > node.fCost)
                    {
                        MinNode = node;
                    }
                }
                if (MinNode.x + 1 == Treasure.x && MinNode.y == Treasure.y)
                {
                    ActivNode.Remove(MinNode);
                    DisActivNode.Add(MinNode);
                    Way = BuildWay(DisActivNode, MinNode);
                    return;
                }
                if (MinNode.x - 1 == Treasure.x && MinNode.y == Treasure.y)
                {
                    ActivNode.Remove(MinNode);
                    DisActivNode.Add(MinNode);
                    Way = BuildWay(DisActivNode, MinNode);
                    return;
                }
                if (MinNode.x == Treasure.x && MinNode.y + 1 == Treasure.y)
                {
                    ActivNode.Remove(MinNode);
                    DisActivNode.Add(MinNode);
                    Way = BuildWay(DisActivNode, MinNode);
                    return;
                }
                if (MinNode.x == Treasure.x && MinNode.y - 1 == Treasure.y)
                {
                    ActivNode.Remove(MinNode);
                    DisActivNode.Add(MinNode);
                    Way = BuildWay(DisActivNode, MinNode);
                    return;
                }

                if (MinNode.x + 1 < W && WhereWeCan[MinNode.x + 1, MinNode.y] == 0)
                {
                    Node NewNode = new Node(MinNode.x + 1, MinNode.y, MinNode.x, MinNode.y, MinNode.gCost + 1, Math.Abs(Treasure.x - MinNode.x + 1) + Math.Abs(Treasure.y - MinNode.y));
                    bool Search = GetSearch(ActivNode, DisActivNode, NewNode);

                    if (Search == false)
                        ActivNode.Add(NewNode);
                }
                if (MinNode.x - 1 > 0 && WhereWeCan[MinNode.x - 1, MinNode.y] == 0)
                {
                    Node NewNode = new Node(MinNode.x - 1, MinNode.y, MinNode.x, MinNode.y, MinNode.gCost + 1, Math.Abs(Treasure.x - MinNode.x - 1) + Math.Abs(Treasure.y - MinNode.y));
                    bool Search = GetSearch(ActivNode, DisActivNode, NewNode);

                    if (Search == false)
                        ActivNode.Add(NewNode);
                }
                if (MinNode.y + 1 < H && WhereWeCan[MinNode.x, MinNode.y + 1] == 0)
                {
                    Node NewNode = new Node(MinNode.x, MinNode.y + 1, MinNode.x, MinNode.y, MinNode.gCost + 1, Math.Abs(Treasure.x - MinNode.x) + Math.Abs(Treasure.y - MinNode.y + 1));
                    bool Search = GetSearch(ActivNode, DisActivNode, NewNode);

                    if (Search == false)
                        ActivNode.Add(NewNode);
                }
                if (MinNode.y - 1 > 0 && WhereWeCan[MinNode.x, MinNode.y - 1] == 0)
                {
                    Node NewNode = new Node(MinNode.x, MinNode.y - 1, MinNode.x, MinNode.y, MinNode.gCost + 1, Math.Abs(Treasure.x - MinNode.x) + Math.Abs(Treasure.y - MinNode.y - 1));
                    bool Search = GetSearch(ActivNode, DisActivNode, NewNode);

                    if (Search == false)
                        ActivNode.Add(NewNode);
                }
                ActivNode.Remove(MinNode);
                DisActivNode.Add(MinNode);
            }
        }
        private static bool GetSearch(ArrayList ActivNode, ArrayList DisActivNode, Node NewNode)
        {
            bool Search = false;
            foreach (Node node in ActivNode)
            {
                if (node.x == NewNode.x && node.y == NewNode.y)
                {
                    Search = true;
                    break;
                }
            }
            foreach (Node node in DisActivNode)
            {
                if (node.x == NewNode.x && node.y == NewNode.y)
                {
                    Search = true;
                    break;
                }
            }
            return Search;
        }
        private static ArrayList BuildWay(ArrayList DisActivNode, Node MinNode)
        {
            ArrayList Way = new ArrayList();
            while (MinNode.parentX != -1 && MinNode.parentY != -1)
            {
                Coord way = new Coord(MinNode.x, MinNode.y);
                foreach (Node node in DisActivNode)
                {
                    if (node.x == MinNode.parentX && node.y == MinNode.parentY)
                    {
                        MinNode = node;
                        Way.Add(way);
                        break;
                    }
                }
            }
            return Way;
        }
    }
}