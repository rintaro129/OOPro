using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BattleCity;

public class EnemyLvl3(Field field, int x, int y) : EnemyLvl2(field, x, y)
{
    public override string GetName() => "Enemy Tank (Level 3)";
    public override int SpeedTicks { get; set; } = 10;
    public List<Tuple<int, int>> UnderFireFromPlayerCoordinates { get; set; }
    Tuple<int, int> Destination {  get; set; }
    Tuple<int, int>[,] Parent { get; set; }
    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0)
            return;

        UnderFireFromPlayerCoordinates = new List<Tuple<int, int>>();
        Destination = null;
        Parent = new Tuple<int, int>[Field.FieldSizeX, Field.FieldSizeY];

        UnderFireFromPlayerCoordinates = GetUnderFireCoordinates();
        if (UnderFireFromPlayerCoordinates.Count <= 0) { 
            base.ProcessTurn(); 
            return; 
        }
        if(UnderFireFromPlayerCoordinates.Contains(Tuple.Create(X, Y))) 
        {
            int differenceX = Field.Player.X - X;
            if(differenceX!= 0) 
                differenceX /= Math.Abs(differenceX);
            int differenceY = Field.Player.Y - Y;
            if (differenceY != 0)
                differenceY /= Math.Abs(differenceY);
            Move(differenceX, differenceY);
            Shoot();
            WasDamaged = false;
            return;
        }
        Destination = GetClosestDestinationDijkstra();
        if(Destination == null)
        {
            base.ProcessTurn();
            return;
        }
        int x, y;
        (x, y) = GetMoveToDestination(Destination);
        Move(x, y);
        Shoot();
        WasDamaged = false;
    }
    private List<Tuple<int, int>> GetUnderFireCoordinates()
    {
        List<Tuple<int, int>> res = new List<Tuple<int, int>>();
        int[][] Directions = [[1, 0], [-1, 0], [0, 1], [0, -1]];
        foreach(int[] direction in Directions)
        {
            int x = Field.Player.X; 
            int y = Field.Player.Y;
            while (true)
            {
                x += direction[0];
                y += direction[1];
                if (CheckPositionOutOfRange(x, y) || (Field.Map[x, y] != null && Field.Map[x,y].IsUnkillable())) break;
                res.Add(new Tuple<int, int>(x, y));
            }
        }
        res.Sort();
        return res;
    }
    private Tuple<int, int> GetClosestDestinationDijkstra()
    {
        int[,] dist = new int[Field.FieldSizeX, Field.FieldSizeY];
        bool[,] used = new bool[Field.FieldSizeX, Field.FieldSizeY];
        for (int i = 0; i < dist.GetLength(0); i++)
        {
            for (int j = 0; j < dist.GetLength(1); j++)
            {
                dist[i, j] = int.MaxValue;
                used[i, j] = false;
            }
        }
        dist[X, Y] = 0;
        PriorityQueue<Tuple<int, int>, int> priorityQueue = new PriorityQueue<Tuple<int, int>, int>();
        priorityQueue.Enqueue(Tuple.Create(X, Y), dist[X, Y]);

        while (priorityQueue.Count > 0)
        {
            Tuple<int, int> v = priorityQueue.Dequeue();
            if (UnderFireFromPlayerCoordinates.Contains(v)) return v;
            int x = v.Item1;
            int y = v.Item2;
            if (used[x, y]) continue;
            used[x, y] = true;
            
            int[][] Directions = [[1, 0], [-1, 0], [0, 1], [0, -1]];
            foreach(int[] direction in Directions)
            {
                int ux = x + direction[0];
                int uy = y + direction[1];
                if (CheckPositionOutOfRange(ux, uy) 
                    || (Field.Map[ux, uy] != null && Field.Map[ux, uy].IsUnkillable()) 
                    || used[ux, uy]) continue;
                if (dist[ux, uy] > dist[x, y] + 1)
                {
                    dist[ux, uy] = dist[x, y] + 1;
                    Parent[ux, uy] = Tuple.Create(x, y);
                    priorityQueue.Enqueue(Tuple.Create(ux, uy), dist[ux, uy]);
                }
            }
        }
        return null;
    }
    private (int, int) GetMoveToDestination(Tuple<int, int> Destination)
    {
        Tuple<int, int> currentTile = Destination;
        while (true)
        {
            int x = currentTile.Item1;
            int y = currentTile.Item2;
            if (Parent[x, y].Equals(Tuple.Create(X, Y)))
            {
                int differenceX = x - X;
                int differenceY = y - Y;
                return (differenceX, differenceY);
            }
            currentTile = Parent[x, y];
        }
    }
}