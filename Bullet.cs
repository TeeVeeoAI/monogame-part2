using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_part2
{
    public class Bullet
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;

        public Rectangle Hitbox{
            get{ return hitbox; }
        }
        public Vector2 Position{
            get{ return position; }
        }

        public Bullet(Texture2D texture, Vector2 spawnPosition){
            this.texture = texture;
            this.position = spawnPosition;
            hitbox = new Rectangle((int)position.X,(int)position.Y,10,10);
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.Orange);
        }

        public void Update(){
            position.Y -= 50f * 1f/60f;

            hitbox.Location = position.ToPoint();
        }
    }
}