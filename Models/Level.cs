using System.Collections.Generic;
using System;
using System.IO;
using game.Models;
using mygame.Models;
using System.IO.Pipes;
using mygame.Controllers;

namespace mygame;

public class Level
{
    private Hero _hero;

    private Score _score = new();
    private RenderTarget2D _target;

    private List<string> _lines;
    private Tile[,] _tiles;
    private Rectangle[,] _colliders;
    public Tile[,] Tiles => _tiles;

    private EndRock _end;
    private List<Chest> _chests = new();
    private List<Campfire> _campfires = new();
    private List<Enemy> _enemies = new();

    public Level(Stream fileStream)
    {
        _lines = GetLevelFileLines(fileStream);

        _tiles = new Tile[_lines[0].Length, _lines.Count];
        _colliders = new Rectangle[_lines[0].Length, _lines.Count];

        InitializeMap(_lines);
    }

    private void InitializeMap(List<string> lines)
    {
        _target = new(Globals.GraphicsDevice, _tiles.GetLength(0) * Tile.Size, _tiles.GetLength(1) * Tile.Size);

        Globals.GraphicsDevice.SetRenderTarget(_target);
        Globals.GraphicsDevice.Clear(Color.Transparent);
        Globals.SpriteBatch.Begin();

        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("background"), new Vector2(-20, 0), Color.White);

        for (int y = 0; y < _tiles.GetLength(1); y++)
        {
            for (int x = 0; x < _tiles.GetLength(0); x++)
            {
                var tilePosition = new Vector2(Tile.Size * x, Tile.Size * y);

                _tiles[x, y] = LoadTile(lines[y][x], x, y);
                _colliders[x, y] = new((int)tilePosition.X, (int)tilePosition.Y, Tile.Size, Tile.Size);

                var texture = _tiles[x, y].Texture;
                if (texture == null) continue;

                Globals.SpriteBatch.Draw(texture, tilePosition, Color.White);
            }
        }

        Globals.SpriteBatch.End();
        Globals.GraphicsDevice.SetRenderTarget(null);
    }

    private List<string> GetLevelFileLines(Stream fileStream)
    {
        List<string> lines = new();

        using StreamReader reader = new StreamReader(fileStream);
        string line = reader.ReadLine();
        var width = line.Length;
        while (line != null)
        {
            lines.Add(line);

            if (line.Length != width) throw new Exception($"The length of line {lines.Count} is different from all preceding lines.");

            line = reader.ReadLine();
        }

        return lines;
    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
            case '.':
                return new Tile(null, TileCollision.Passable);
            case ':':
                return new Tile(null, TileCollision.Impassable);
            case '1':
                return new Tile(Globals.Content.Load<Texture2D>("tile1"), TileCollision.Impassable);
            case '2':
                return new Tile(Globals.Content.Load<Texture2D>("tile2"), TileCollision.Platform);
            case 'b':
                return new Tile(Globals.Content.Load<Texture2D>("bush1"), TileCollision.Passable);
            case 'g':
                return new Tile(Globals.Content.Load<Texture2D>("grass"), TileCollision.Passable);
            case 'v':
                return new Tile(Globals.Content.Load<Texture2D>("box"), TileCollision.Impassable);
            case 't':
                return new Tile(Globals.Content.Load<Texture2D>("tree2"), TileCollision.Passable);
            case 'e':
            {
                _end = new EndRock(x, y);
                return new Tile(null, TileCollision.Passable);
                }
            case 'f':
            {
                _campfires.Add(new(Globals.Content.Load<Texture2D>("campfire"), new(x * Tile.Size, y * Tile.Size)));
                return new Tile(null, TileCollision.Passable);
            }
            case 'c':
            {
                _chests.Add(new(x, y, 100, _score));
                return new Tile(null, TileCollision.Passable);
            }
            case 'p':
            {
                _hero = new(Globals.Content.Load<Texture2D>("character"), new(x * Tile.Size, y * Tile.Size), 6, 1, this);
                return new Tile(null, TileCollision.Passable);
            }
            case 'w':
            {
                _enemies.Add(new(Globals.Content.Load<Texture2D>("skeletonwarrior"), new(x * Tile.Size, y * Tile.Size), 7, 1));
                return new Tile(null, TileCollision.Passable);
            }
            case 'l':
            {
                _enemies.Add(new(Globals.Content.Load<Texture2D>("skeletonwarriorleft"), new(x * Tile.Size, y * Tile.Size), 7, 1));
                return new Tile(null, TileCollision.Passable);
            }
            default:
                throw new Exception($"Unsupported tile type character '{tileType}' at position {x}, {y}.");
        }
    }

    public List<Rectangle> GetNearestColliders(Rectangle bounds)
    {
        int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Size);
        int rightTile = (int)Math.Ceiling((float)bounds.Right / Tile.Size) - 1;
        int topTile = (int)Math.Floor((float)bounds.Top / Tile.Size);
        int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / Tile.Size) - 1;

        leftTile = MathHelper.Clamp(leftTile, 0, _tiles.GetLength(0) - 1);
        rightTile = MathHelper.Clamp(rightTile, 0, _tiles.GetLength(0) - 1);
        topTile = MathHelper.Clamp(topTile, 0, _tiles.GetLength(1) - 1);
        bottomTile = MathHelper.Clamp(bottomTile, 0, _tiles.GetLength(1) - 1);

        List<Rectangle> result = new();
        
        for (int x = leftTile; x <= rightTile; x++)
        {
            for (int y = topTile; y <= bottomTile; y++)
            {
                if (_tiles[x, y].Collision != TileCollision.Passable) result.Add(_colliders[x, y]);
            }
        }
        
        return result;
    }

    public void Update()
    {
        LevelController.Update(_hero, _chests, _campfires, _enemies, _end);
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(_target, Vector2.Zero, Color.White);

        foreach (var chest in _chests)
        {
            chest.Draw();
        }

        foreach (var enemy in _enemies)
        {
            enemy.Draw();
        }

        foreach (var campfire in _campfires)
        {
            campfire.Draw();
        }

        _end.Draw();
        _hero.Draw();

        _score.Draw();
    }
}