using mygame;
using mygame.Controllers;
using mygame.Managers;
using mygame.Models;
using static System.Formats.Asn1.AsnWriter;

namespace game.Models;

public class EndRock
{
    private Texture2D _texture = Globals.Content.Load<Texture2D>("end");
    private Vector2 _position;
    private bool _buttonDraw;

    public EndRock(int x, int y)
    {
        _position = new(x * Tile.Size, y * Tile.Size);
    }

    public void Update(Vector2 playerPosition)
    {
        EndRockController.Update(_position, playerPosition, ref _buttonDraw);
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, _position, Color.White);

        if (_buttonDraw) Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("ebutton"), _position + new Vector2(8, -20), Color.White);
    }
}