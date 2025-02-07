using System;
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
    private int hp = 3;
    private Texture2D fullHeart;
    private Texture2D heart;
    private Texture2D notFullHeart;

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

        fullHeart = Content.Load<Texture2D>("fullHeart");

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
        EnemyBulletCollisionAndPlayer();
        SpawnEnemy();
    
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(20,15,30));

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        DrawHeart(_spriteBatch);
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

    private void EnemyBulletCollisionAndPlayer(){
        for(int i = 0; i < enemies.Count; i++){
            for(int j = 0; j < player.Bullets.Count; j++){
                if(enemies[i].Hitbox.Intersects(player.Hitbox)){
                    hp--;
                    enemies.RemoveAt(i);
                    if (hp <= 0){
                        Exit();
                    }
                }
                else if(enemies[i].Position.Y > 470){
                    enemies.RemoveAt(i);
                }
                if(enemies[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
                    enemies.RemoveAt(i);
                    player.Bullets.RemoveAt(j);
                }
                else if(player.Bullets[j].Position.Y < 0){
                    player.Bullets.RemoveAt(j);
                }
            }
        }
    }
    private void DrawHeart(SpriteBatch spriteBatch){
        _spriteBatch.Draw(heart, new Rectangle(10,10,100,100), Color.White);
        _spriteBatch.Draw(heart, new Rectangle(110,10,100,100), Color.White);
        _spriteBatch.Draw(heart, new Rectangle(210,10,100,100), Color.White);
    }
}
