using System;
using System.IO;
using game.Models;
using mygame.Models;

namespace mygame.Managers;

public class GameManager
{
    private Level _level;

    public GameManager()
    {
        using (Stream fileStream = TitleContainer.OpenStream("Content/Levels/0.txt"))
        {
            _level = new(fileStream, 0);
        }

        Globals.WindowSize = new(_level.Tiles.GetLength(0) * Tile.Size, _level.Tiles.GetLength(1) * Tile.Size);
    }

    public void Update()
    {
        InputManager.Update();

        _level.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();

        _level.Draw(); 

        Globals.SpriteBatch.End();
    }
}