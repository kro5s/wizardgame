using mygame.Managers;
using mygame.Models;

namespace mygame.Controllers;

public class HeroController
{
    private Hero _hero;
    private readonly AnimationManager _anims;
    private readonly Level _level;
    private readonly int _width;
    private readonly int _height;

    private Vector2 _velocity;
    private bool _onGround;

    private Vector2 _direction = new(0, 0);

    public HeroController(AnimationManager anims, Level level, int width, int height)
    {
        _anims = anims;
        _level = level;
        _width = width;
        _height = height;
    }
    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new((int)pos.X + Physics.OFFSET, (int)pos.Y, _width - (2 * Physics.OFFSET), _height);
    }

    private void UpdateVelocity()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.A)) _velocity.X = -Physics.SPEED;
        else if (keyboardState.IsKeyDown(Keys.D)) _velocity.X = Physics.SPEED;
        else _velocity.X = 0;

        _velocity.Y += Physics.GRAVITY * Globals.TotalSeconds;

        if (keyboardState.IsKeyDown(Keys.Space) && _onGround)
        {
            _velocity.Y = -Physics.JUMP;
        }
    }

    private void UpdatePosition()
    {
        _onGround = false;
        var newPos = _hero.position + (_velocity * Globals.TotalSeconds);
        Rectangle newRect = CalculateBounds(newPos);

        foreach (var collider in _level.GetNearestColliders(newRect))
        {
            if (newPos.X != _hero.position.X)
            {
                newRect = CalculateBounds(new(newPos.X, _hero.position.Y));
                if (newRect.Intersects(collider))
                {
                    if (newPos.X > _hero.position.X) newPos.X = collider.Left - _width + Physics.OFFSET;
                    else newPos.X = collider.Right - Physics.OFFSET;
                    continue;
                }
            }

            newRect = CalculateBounds(new(_hero.position.X, newPos.Y));
            if (newRect.Intersects(collider))
            {
                if (_velocity.Y > 0)
                {
                    newPos.Y = collider.Top - _height;
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

        _hero.position = newPos;
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

    public void Update(Hero hero)
    {
        _hero = hero;

        UpdateVelocity();
        UpdatePosition();
        UpdateDirection();

        _anims.Update(_direction);
    }
}