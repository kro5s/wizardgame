using mygame;
using mygame.Controllers;
using mygame.Managers;
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
        ChestController.Update(_position, playerPosition, ref _isCollected, ref _buttonDraw, _score, _value);
    }

    public void Draw()
    {
        _texture = Globals.Content.Load<Texture2D>(_isCollected ? "chestopened" : "chest");

        Globals.SpriteBatch.Draw(_texture, _position, Color.White);

        if (_buttonDraw) Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("ebutton"), _position + new Vector2(8, -20), Color.White);
    }
}