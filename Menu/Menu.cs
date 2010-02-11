//
// Menu.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CMenu
    {
        public CGalaxy Game { get; set; }
        public delegate void MenuFunction();
        public struct MenuOption
        {
            public string Text;
            public MenuFunction Function;
            public object Data;
        }
        public List<MenuOption> MenuOptions { get; set; }
        public int Cursor { get; set; }
        public Vector2 Position { get; set; }

        public CMenu(CGalaxy game)
        {
            Game = game;
            Cursor = 0;
            MenuOptions = new List<MenuOption>();
            Position = Vector2.Zero;
        }

        public void Update()
        {
            Cursor += Game.Input.IsKeyPressed(Keys.Down) ? 1 : 0;
            Cursor -= Game.Input.IsKeyPressed(Keys.Up) ? 1 : 0;

            Cursor = Math.Max(Cursor, 0);
            Cursor = Math.Min(Cursor, MenuOptions.Count - 1);

            if (Game.Input.IsKeyPressed(Keys.Enter))
            {
                MenuOption option = MenuOptions[Cursor];
                option.Function();
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            float Spacing = 20.0f;
            Vector2 position = Position;
            foreach (MenuOption option in MenuOptions)
            {
                sprite_batch.DrawString(Game.DefaultFont, option.Text, position, Color.White);
                position += Vector2.UnitY * Spacing;
            }
            position = Position + Vector2.UnitY * Spacing * Cursor;
            sprite_batch.DrawString(Game.DefaultFont, ">", position - Vector2.UnitX * 20.0f, Color.White);
        }
    }
}
