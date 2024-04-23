namespace Game;

public class Tank : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnMoved;
    public event EventHandler OnDied;
    public override bool CanMove() => true;
    public override bool IsSolid() => true;
    public override bool IsUnkillable() => false;
    public Tank()
    {
        // for player
    }
    public Tank(Field field, int x, int y)
    {
        X = x;
        Y = y;
        Field = field;
        Field.SubscribeToTank(this);
        OnCreated?.Invoke(this, EventArgs.Empty);
    }
    

    public override int SpeedTicks { get; set; } = 2;

    public override char GetSprite()
    {
        return Direction switch
        {
            Direction.Up => 'Ʌ',
            Direction.Down => 'V',
            Direction.Left => '<',
            Direction.Right => '>',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override void Move(int xDifference, int yDifference)
    {
        if (!CheckMovePosition(xDifference, yDifference)) return;
        X += xDifference;
        Y += yDifference;
        Direction = xDifference switch
        {
            0 when yDifference == -1 => Direction.Up,
            0 when yDifference == 1 => Direction.Down,
            -1 when yDifference == 0 => Direction.Left,
            1 when yDifference == 0 => Direction.Right,
            _ => Direction
        };
        OnMoved?.Invoke(this, EventArgs.Empty);
    }
    
    public override void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    public Bullet Bullet { get; set; }
    public bool IsShooting { get; set; } = false;
    public void Shoot()
    {
        if (IsShooting) return;
        int x = X;
        int y = Y;
        switch (Direction)
        {
            case Direction.Down:
                y = Y + 1;
                break;
            case Direction.Up:
                y = Y - 1;
                break;
            case Direction.Right:
                x = X + 1;
                break;
            case Direction.Left:
                x = X - 1;
                break;
        }
        if (CheckPositionExploding(x, y))
        {
            if(!CheckPositionOutOfRange(x, y)) Field.Map[x, y].TakeDamage();
            return;
        }
        Bullet = new Bullet(this);
        Bullet.OnDied += Bullet_OnDied;
        IsShooting = true;
    }

    private void Bullet_OnDied(object sender, EventArgs e)
    {
        IsShooting = false;
        Bullet = null;
    }
    public override void ProcessTurn()
    {
        
        Random random = new Random();
        int moveDirection = random.Next(5); // Generates a random number between 0 and 4

        switch (moveDirection)
        {
            case 0: // Move up
                Move(0, -1);
                break;
            case 1: // Move down
                Move(0, 1);
                break;
            case 2: // Move left
                Move(-1, 0);
                break;
            case 3: // Move right
                Move(1, 0);
                break;
            case 4: // Stay in place
                // Do nothing
                break;
        }
        Shoot();
    }
    private bool CheckPositionExploding(int x, int y) => CheckPositionOutOfRange(x, y) || CheckPositionIsSolid(x, y);
}