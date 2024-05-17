using mygame;
using mygame.Models;

namespace game.Models;

public class Chest
{
    private Texture2D _texture;
    private Vector2 _position;
    private Score _score;

    private int _value;
    private bool _isCollected;

    private bool _buttonDraw;

    public Chest(int x, int y, int value, Score score)
    {
        _position = new (x * Tile.Size, y * Tile.Size);
        _value = value;
        _score = score;
    }

    public void Update(Vector2 playerPosition)
    {
        bool nearEnough = Vector2.Distance(playerPosition, _position) < 100;

        if (nearEnough && !_isCollected)
        {
            _buttonDraw = true;
        }
        else
        {
            _buttonDraw = false;
        }

        if (nearEnough && Keyboard.GetState().IsKeyDown(Keys.E) && !_isCollected)
        {
            _score.Update(_value);
            _isCollected = true;
        }
    }

    public void Draw()
    {
        _texture = Globals.Content.Load<Texture2D>(_isCollected ? "chestopened" : "chest");

        Globals.SpriteBatch.Draw(_texture, _position, Color.White);

        if (_buttonDraw) Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("ebutton"), _position + new Vector2(8, -20), Color.White);
    }
}