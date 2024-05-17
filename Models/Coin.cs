namespace mygame.Models;

public class Coin
{
    private static Texture2D _texture;

    private Vector2 _position;

    private readonly Animation _animation;

    public Coin(Vector2 position)
    {
        _texture ??= Globals.Content.Load<Texture2D>("coin");
        _animation = new(_texture, 6, 1, 0.1f);
        _position = position;
    }

    public void Update()
    {
        _animation.Update();
    }

    public void Draw()
    {
        _animation.Draw(_position);
    }
}