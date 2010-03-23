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
        public delegate void MenuSelectFunction(object tag);
        public delegate void MenuHighlightFunction(object tag);
        public delegate void MenuAxisFunction(object tag, int axis);
        public delegate bool MenuAxisValidateFunction(object tag, int axis);
        public class MenuOption
        {
            public string Text;
            public bool SpecialHighlight;
            public MenuSelectFunction Select;
            public MenuHighlightFunction Highlight;
            public MenuAxisFunction Axis;
            public MenuAxisValidateFunction AxisValidate;
            public object Data;
            public int AxisValue;

            public MenuOption()
            {
                Select = (tag) => { };
                Highlight = (tag) => { };
                Axis = (tag, axis) => { };
                AxisValidate = (tag, axis) => { return false; };
            }
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
            int offset = 0;
            if (Game.Input.IsKeyPressed(Keys.Down))
            {
                offset += 1;
                while (Cursor + offset < MenuOptions.Count && MenuOptions[Cursor + offset] == null)
                    offset += 1;
            }
            if (Game.Input.IsKeyPressed(Keys.Up))
            {
                offset -= 1;
                while (Cursor + offset > 0 && MenuOptions[Cursor + offset] == null)
                    offset -= 1;
            }

            int previous = Cursor;

            Cursor = Cursor + offset;
            Cursor = Math.Max(Cursor, 0);
            Cursor = Math.Min(Cursor, MenuOptions.Count - 1);

            MenuOption option = MenuOptions[Cursor];

            if (Cursor != previous)
            {
                option.Highlight(option.Data);
                option.Axis(option.Data, option.AxisValue);
            }

            if (Game.Input.IsKeyPressed(Keys.Enter))
            {
                option.Select(option.Data);
            }

            if (Game.Input.IsKeyPressed(Keys.Left))
            {
                option.AxisValue -= 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                    option.Axis(option.Data, option.AxisValue);
                else
                    option.AxisValue += 1;
            }

            if (Game.Input.IsKeyPressed(Keys.Right))
            {
                option.AxisValue += 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                    option.Axis(option.Data, option.AxisValue);
                else
                    option.AxisValue -= 1;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            float Spacing = 20.0f;
            Vector2 position = Position;
            sprite_batch.DrawString(Game.DefaultFont, ">", Position + Vector2.UnitY * Spacing * Cursor - Vector2.UnitX * 25.0f, Color.White);
            foreach (MenuOption option in MenuOptions)
            {
                if (option != null)
                {
                    if (option.SpecialHighlight)
                    {
                        Vector2 measured = Game.DefaultFont.MeasureString(option.Text);
                        sprite_batch.DrawString(Game.DefaultFont, ">", position - Vector2.UnitX * 20.0f, Color.Yellow);
                        sprite_batch.DrawString(Game.DefaultFont, "<", position + Vector2.UnitX * (measured.X + 5.0f), Color.Yellow);
                        sprite_batch.DrawString(Game.DefaultFont, option.Text, position, Color.Yellow);
                    }
                    else
                    {
                        sprite_batch.DrawString(Game.DefaultFont, option.Text, position, Color.White);
                    }
                }
                position += Vector2.UnitY * Spacing;
            }
        }
    }
}
