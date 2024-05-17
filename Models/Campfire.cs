using mygame;
using mygame.Models;
using static System.Formats.Asn1.AsnWriter;

namespace game.Models;

public class Campfire
{
    private Texture2D _texture;
    private Vector2 _position;

    private bool _buttonDraw;
    private bool _isFired;

    private Animation _fireAnimation = new(Globals.Content.Load<Texture2D>("fireeffect"), 19, 1, 0.1f);

    public Campfire(Texture2D texture, Vector2 position)
    {
        _texture = texture;
        _position = position;
    }

    public void Update(Vector2 playerPosition)
    {
        bool nearEnough = Vector2.Distance(playerPosition, _position) < 100;

        if (nearEnough && !_isFired)
        {
            _buttonDraw = true;
        }
        else
        {
            _buttonDraw = false;
        }

        if (nearEnough && Keyboard.GetState().IsKeyDown(Keys.E) && !_isFired)
        {
            _isFired = true;
        }

        if (_isFired)
        {
            _fireAnimation.Update();
        }
    }

    public void Draw()
    {
        if (_isFired) _fireAnimation.Draw(_position + new Vector2(13, -45));

        Globals.SpriteBatch.Draw(_texture, _position, Color.White);

        if (_buttonDraw) Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("ebutton"), _position + new Vector2(25, -20), Color.White);
    }
}