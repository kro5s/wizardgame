using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace mygame.Models;

public class Animation
{
    private readonly Texture2D _texture;

    private readonly List<Rectangle> _sourceRectangles = new();

    private readonly int _frames;
    private int _frame;
    private readonly float _frameTime;
    private float _frameTimeLeft;

    private bool _active = true;

    public bool OncePlayed { get; private set; }

    /// <summary>
    /// Creates new animation
    /// </summary>
    /// <param name="texture">Texture file</param>
    /// <param name="frameX">Horizontal amount of frames</param>
    /// <param name="frameY">Vertical amount of frames</param>
    /// <param name="frameTime">Duration of each frame</param>
    /// <param name="row">Amount of spritesheet rows</param>
    public Animation(Texture2D texture, int frameX, int frameY, float frameTime, int row = 1)
    {
        _texture = texture;
        _frameTime = frameTime;
        _frameTimeLeft = frameTime;
        _frames = frameX;

        var frameWidth = _texture.Width / frameX;
        var frameHeight = _texture.Height / frameY;

        for (var i = 0; i < _frames; i++)
        {
            _sourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
        }
    }

    public void Stop()
    {
        _active = false;
    }

    public void Start()
    {
        _active = true;
    }

    public void Reset()
    {
        _frame = 0;
        _frameTimeLeft = _frameTime;
    }

    public void Update()
    {
        if (!_active) return;

        _frameTimeLeft -= Globals.TotalSeconds;

        if (_frameTimeLeft <= 0)
        {
            OncePlayed = true;

            _frameTimeLeft += _frameTime;
            _frame = (_frame + 1) % _frames;
        }
    }

    public void Draw(Vector2 position)
    {
        Globals.SpriteBatch.Draw(_texture, position, _sourceRectangles[_frame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
    }
}