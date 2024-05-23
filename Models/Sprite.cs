using System;
using mygame.Managers;
using mygame.Models;

namespace mygame;

public class Sprite
{
    public int Width { get; }
    public int Height { get; }

    public bool IsDead { get; set; }

    public AnimationManager Anims { get; set; }

    public Vector2 position;
    private Animation _animation;

    public Sprite(Texture2D texture, Vector2 position, int frameX, int frameY)
    {
        Width = texture.Width / frameX;
        Height = texture.Height / frameY;
        this.position = position;
        _animation = new Animation(texture, frameX, frameY, 0.1f);
    }

    public void Kill()
    {
        IsDead = true;
    }

    public void Draw()
    {
        if (IsDead) return;

        Anims.Draw(position);
    }
}