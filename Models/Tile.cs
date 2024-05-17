using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mygame.Models;

public enum TileCollision
{
    Passable,
    Impassable,
    Platform,
}

public struct Tile
{
    public Texture2D Texture;
    public TileCollision Collision;
    public static int Size = 32;

    public Tile(Texture2D texture, TileCollision collision)
    {
        Texture = texture;
        Collision = collision;
    }
}