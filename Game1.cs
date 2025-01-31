﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_part2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D spaceShip;
    private List<Enemy> enemies = new List<Enemy>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        spaceShip = Content.Load<Texture2D>("SpaceShip");

        player = new Player(spaceShip, new Vector2(380, 350), 50);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        player.Update();
        foreach (Enemy enemy in enemies){
            enemy.Update();
        }

        SpawnEnemy();
    
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(20,15,30));

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        player.Draw(_spriteBatch);
        foreach (Enemy enemy in enemies){
            enemy.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SpawnEnemy(){
        Random rand = new Random();
        int value = rand.Next(1,101);
        int spawnChancePercent = 5;
        if (value <= spawnChancePercent)
            enemies.Add(new Enemy(spaceShip));
    }
}
