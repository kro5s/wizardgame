using mygame.Managers;
using mygame.Models;

namespace mygame;

public class Hero : Sprite 
{
    private readonly Level _level;

    private const float SPEED = 300f;
    private const float GRAVITY = 5000f;
    private const float JUMP = 1500f;
    private const int OFFSET = 10;

    private Vector2 _velocity;
    private bool _onGround;

    private bool _isDead;

    private Vector2 _direction = new(0, 0);

    public Hero(Texture2D texture, Vector2 position, int frameX, int frameY, Level level) : base(texture, position, frameX, frameY)
    {
        _level = level;

        Anims = new AnimationManager();
        Anims.AddAnimation(new Vector2(0, 0), new Animation(Globals.Content.Load<Texture2D>("character"), 6, 1, 0.1f));
        Anims.AddAnimation(new Vector2(1, 0), new Animation(Globals.Content.Load<Texture2D>("run"), 8, 1, 0.09f));
        Anims.AddAnimation(new Vector2(-1, 0), new Animation(Globals.Content.Load<Texture2D>("runleft"), 8, 1, 0.09f));
        Anims.AddAnimation(new Vector2(1, -1), new Animation(Globals.Content.Load<Texture2D>("fall"), 2, 1, 1f));
        Anims.AddAnimation(new Vector2(-1, -1), new Animation(Globals.Content.Load<Texture2D>("fallleft"), 2, 1, 1f));
        Anims.AddAnimation(new Vector2(0, -1), new Animation(Globals.Content.Load<Texture2D>("fall"), 2, 1, 1f));
    }

    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new((int)pos.X + OFFSET, (int)pos.Y, Width - (2 * OFFSET), Height);
    }

    private void UpdateVelocity()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.A)) _velocity.X = -SPEED;
        else if (keyboardState.IsKeyDown(Keys.D)) _velocity.X = SPEED;
        else _velocity.X = 0;

        _velocity.Y += GRAVITY * Globals.TotalSeconds;

        if (keyboardState.IsKeyDown(Keys.Space) && _onGround)
        {
            _velocity.Y = -JUMP;
        }
    }

    private void UpdatePosition()
    {
        _onGround = false;
        var newPos = position + (_velocity * Globals.TotalSeconds);
        Rectangle newRect = CalculateBounds(newPos);

        foreach (var collider in _level.GetNearestColliders(newRect))
        {
            if (newPos.X != position.X)
            {
                newRect = CalculateBounds(new(newPos.X, position.Y));
                if (newRect.Intersects(collider))
                {
                    if (newPos.X > position.X) newPos.X = collider.Left - Width + OFFSET;
                    else newPos.X = collider.Right - OFFSET;
                    continue;
                }
            }

            newRect = CalculateBounds(new(position.X, newPos.Y));
            if (newRect.Intersects(collider))
            {
                if (_velocity.Y > 0)
                {
                    newPos.Y = collider.Top - Height;
                    _onGround = true;
                    _velocity.Y = 0;
                }
                else
                {
                    newPos.Y = collider.Bottom;
                    _velocity.Y = 0;
                }
            }
        }

        position = newPos;
    }

    private void UpdateDirection()
    {
        if (_velocity is { X: > 0, Y: 0 })
        {
            _direction = new Vector2(1, 0);
        }
        else if (_velocity is { X: < 0, Y: 0 })
        {
            _direction = new Vector2(-1, 0);
        }
        else if (_velocity.Y != 0 && _velocity.X > 0)
        {
            _direction = new Vector2(1, -1);
        }
        else if (_velocity.Y != 0 && _velocity.X < 0)
        {
            _direction = new Vector2(-1, -1);
        }
        else if (_velocity.Y != 0 && _velocity.X == 0 || !_onGround && _velocity.X == 0)
        {
            _direction = new Vector2(0, -1);
        }
        else
        {
            _direction = new Vector2(0, 0);
        }
    }

    public void Kill()
    {
        _isDead = true;
    }

    public void Update()
    {
        if (_isDead) return;

        UpdateVelocity();
        UpdatePosition();
        UpdateDirection();

        Anims.Update(_direction);
    }
}