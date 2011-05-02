//
// Content.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CContent
    {
        public static Texture2D LoadTexture2D(CGalaxy game, string name)
        {
            Texture2D texture = game.Content.Load<Texture2D>(name);
            texture.Name = name;
            return texture;
        }
    }
}
