using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_part2
{
    public class Player
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;
        private KeyboardState newKState;
        private KeyboardState oldKState;
        private List<Bullet> bullets = new List<Bullet>();

        public List<Bullet> Bullets{
            get{ return bullets; }
        }
        
        public Player(Texture2D texture, Vector2 position, int pixelSize){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int)position.X, (int)position.Y, pixelSize, pixelSize);
        }

        private void Move(){
            if(newKState.IsKeyDown(Keys.A) && position.X >= 0){
                position.X -= 2;
            }
            if(newKState.IsKeyDown(Keys.D) && position.X <= 750){
                position.X += 2;
            }

            hitbox.Location = position.ToPoint();
        }

        private void Shoot(){
            if(newKState.IsKeyDown(Keys.Space) && oldKState.IsKeyUp(Keys.Space)){
                Bullet bullet = new Bullet(texture, position);
                bullets.Add(bullet);
            }
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
            
            foreach(Bullet bullet in bullets){
                bullet.Draw(spriteBatch);
            }
        }

        public void Update(){
            newKState = Keyboard.GetState();
            Move();
            Shoot();
            oldKState = newKState;

            foreach(Bullet bullet in bullets){
                bullet.Update();
            }
        }
    }
}