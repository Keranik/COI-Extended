using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COIExtended.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;  // Make sure you have System.Drawing.Common package
using System.IO;

class Program
{
    static Random random = new Random();
    const int GridSize = 4000;
    const int minDistance = 300;
    const int maxDistance = 4000;
    const int DotCount = 100;

    static void Main()
    {
        Console.WriteLine("Enter a seed (leave blank for a random seed):");
        string input = Console.ReadLine();
        int seed = string.IsNullOrEmpty(input) ? Environment.TickCount : int.Parse(input);

        Random random = new Random(seed);

        List<Dot> dots = PlaceDots(random, maxDistance, minDistance);
        ConnectDots(dots, random);

        // Creating a bitmap image to visualize the dot map
        using (Bitmap bmp = new Bitmap(GridSize, GridSize))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // Set background color

                // Prepare to write coordinates to a text file
                using (StreamWriter writer = new StreamWriter("DotCoordinates.txt"))
                {
                    foreach (var dot in dots)
                    {
                        DrawDot(g, dot);
                        foreach (var connectedDotId in dot.Connections)
                        {
                            DrawLine(g, dot, dots[connectedDotId]);
                        }

                        // Write the dot in the specified format
                        WriteDotCoordinates(writer, dot);
                    }

                    // After all dots are written, write the connections
                    WriteDotConnections(writer, dots);
                }

                bmp.Save("DotMap.bmp"); // Save the image
            }
        }

        Console.WriteLine("Map image saved as 'DotMap.bmp'");
        Console.WriteLine("Dot coordinates saved in 'DotCoordinates.txt'");
    }

    static bool IsConnected(Dot start, Dot end, Dictionary<int, Dot> dotMap)
    {
        Queue<Dot> queue = new Queue<Dot>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(start);
        visited.Add(start.Id);

        while (queue.Count > 0)
        {
            Dot current = queue.Dequeue();
            if (current.Id == end.Id)
            {
                return true;
            }

            foreach (var neighborId in current.Connections)
            {
                if (!visited.Contains(neighborId))
                {
                    visited.Add(neighborId);
                    queue.Enqueue(dotMap[neighborId]);
                }
            }
        }

        return false;
    }

    static void WriteDotCoordinates(StreamWriter writer, Dot dot)
    {
        string locationName = dot.Id == 0 ? "COI: Extended (Home)" : $"Location {dot.Id}";
        writer.WriteLine($"WorldMapLocation worldMapLocation{dot.Id + 1} = new WorldMapLocation(\"{locationName}\", new Vector2i({dot.X}, {dot.Y}));");
        writer.WriteLine($"worldMap.AddLocation(worldMapLocation{dot.Id + 1}, false);");
    }

    static void WriteDotConnections(StreamWriter writer, List<Dot> dots)
    {
        foreach (var dot in dots)
        {
            foreach (var connectedDotId in dot.Connections)
            {
                writer.WriteLine($"worldMap.AddConnection(worldMapLocation{dot.Id + 1}, worldMapLocation{connectedDotId + 1});");
            }
        }
    }
    static void DrawDot(Graphics g, Dot dot)
    {
        const int dotSize = 10;
        g.FillEllipse(Brushes.Black, dot.X - dotSize / 2, dot.Y - dotSize / 2, dotSize, dotSize);
    }

    static void DrawLine(Graphics g, Dot startDot, Dot endDot)
    {
        g.DrawLine(Pens.Black, startDot.X, startDot.Y, endDot.X, endDot.Y);
    }

    static List<Dot> PlaceDots(Random random, int maxDistance, int minDistance)
    {
        List<Dot> dots = new List<Dot>();

        // Add the "Home" location
        Dot homeDot = new Dot(1860, 2717, 0);
        if (IsWithinBoundary(homeDot)) dots.Add(homeDot);

        // Generate additional dots within boundaries
        for (int i = 0; i < DotCount; i++)
        {
            int x = random.Next(-100, 1800); // Adjusted for x-axis boundary
            int y = random.Next(-100, 1000); // Adjusted for y-axis boundary
            Dot newDot = new Dot(x, y, i + 1);

            if (IsWithinBoundary(newDot))
            {
                dots.Add(newDot);
            }
        }

        return dots;
    }

    static bool IsWithinBoundary(Dot dot)
    {
        return dot.X >= -100 && dot.X <= 1800 && dot.Y >= -100 && dot.Y <= 1000;
    }

    static bool IsValidPlacement(Dot newDot, List<Dot> existingDots, int maxDistance, int minDistance)
    {
        foreach (var dot in existingDots)
        {
            double dist = Distance(dot, newDot);
            if (dist < minDistance || dist > maxDistance)
            {
                return false;
            }
        }
        return true;
    }

    static void ConnectDots(List<Dot> dots, Random random)
    {
        var dotMap = dots.ToDictionary(d => d.Id); // Create a map for easy access

        foreach (var dot in dots)
        {
            var potentialConnections = dots.Where(d => d != dot && !dot.IsFullyConnected)
                                           .OrderBy(d => Distance(dot, d))
                                           .Take(4 - dot.Connections.Count);

            foreach (var connectDot in potentialConnections)
            {
                if (!connectDot.IsFullyConnected && !IsConnected(dot, connectDot, dotMap))
                {
                    dot.AddConnection(connectDot.Id);
                    connectDot.AddConnection(dot.Id);
                }
            }
        }
    }

    static double Distance(Dot a, Dot b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

class Dot
{
    public int Id { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public HashSet<int> Connections { get; private set; }

    public bool IsFullyConnected => Connections.Count >= 4;

    public Dot(int x, int y, int id)
    {
        Id = id;
        X = x;
        Y = y;
        Connections = new HashSet<int>();
    }

    public void AddConnection(int dotId)
    {
        if (!IsFullyConnected && dotId != Id)
        {
            Connections.Add(dotId);
        }
    }
}


