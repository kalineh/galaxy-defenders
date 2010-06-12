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
        public delegate bool MenuSelectValidateFunction(object tag);
        public delegate void MenuHighlightFunction(object tag);
        public delegate void MenuAxisFunction(object tag, int axis);
        public delegate bool MenuAxisValidateFunction(object tag, int axis);
        public class MenuOption
        {
            public string Text;
            public string SubText;
            public bool SpecialHighlight;
            public MenuSelectFunction Select;
            public MenuSelectValidateFunction SelectValidate;
            public MenuHighlightFunction Highlight;
            public MenuAxisFunction Axis;
            public MenuAxisValidateFunction AxisValidate;
            public bool CancelOption;
            public object Data;
            public int AxisValue;

            public MenuOption()
            {
                Select = (tag) => { };
                SelectValidate = (tag) => { return true; };
                Highlight = (tag) => { };
                Axis = (tag, axis) => { };
                AxisValidate = (tag, axis) => { return false; };
            }
        }
        public List<MenuOption> MenuOptions { get; set; }
        public int Cursor { get; set; }
        public Vector2 Position { get; set; }
        public bool Centered { get; set; }

        public CMenu(CGalaxy game)
        {
            Game = game;
            Cursor = 0;
            MenuOptions = new List<MenuOption>();
            Position = Vector2.Zero;
        }

        public void Update()
        {
            if (Game.Input.IsKeyPressed(Keys.Escape) || Game.Input.IsPadCancelPressed(PlayerIndex.One))
            {
                foreach (MenuOption cancel_option in MenuOptions)
                {
                    if (cancel_option.CancelOption)
                    {
                        CSound.DirectPlay(Game, "MenuCancel", 1.0f);
                        cancel_option.Highlight(cancel_option.Data);
                        cancel_option.Select(cancel_option.Data);
                    }
                }
            }

            int offset = 0;
            if (Game.Input.IsKeyPressed(Keys.Down) || Game.Input.IsPadDownPressed(PlayerIndex.One))
            {
                offset += 1;
                while (Cursor + offset < MenuOptions.Count && (MenuOptions[Cursor + offset] == null || MenuOptions[Cursor + offset].SelectValidate(MenuOptions[Cursor + offset].Data) == false))
                    offset += 1;
            }
            if (Game.Input.IsKeyPressed(Keys.Up) || Game.Input.IsPadUpPressed(PlayerIndex.One))
            {
                offset -= 1;
                while (Cursor + offset > 0 && (MenuOptions[Cursor + offset] == null || MenuOptions[Cursor + offset].SelectValidate(MenuOptions[Cursor + offset].Data) == false))
                    offset -= 1;
            }

            int previous = Cursor;

            Cursor = Cursor + offset;
            Cursor = Math.Max(Cursor, 0);
            Cursor = Math.Min(Cursor, MenuOptions.Count - 1);

            MenuOption option = MenuOptions[Cursor];

            if (Cursor != previous)
            {
                CSound.DirectPlay(Game, "MenuMoveItem", 1.0f);

                option.Highlight(option.Data);
                option.Axis(option.Data, option.AxisValue);
            }

            if (Game.Input.IsKeyPressed(Keys.Enter) || Game.Input.IsPadConfirmPressed(PlayerIndex.One))
            {
                CSound.DirectPlay(Game, "MenuSelect", 1.0f);
                option.Select(option.Data);
            }

            if (Game.Input.IsKeyPressed(Keys.Left) || Game.Input.IsPadLeftPressed(PlayerIndex.One))
            {
                option.AxisValue -= 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                {
                    CSound.DirectPlay(Game, "MenuDowngrade", 1.0f);
                    option.Axis(option.Data, option.AxisValue);
                }
                else
                    option.AxisValue += 1;
            }

            if (Game.Input.IsKeyPressed(Keys.Right) || Game.Input.IsPadRightPressed(PlayerIndex.One))
            {
                option.AxisValue += 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                { 
                    CSound.DirectPlay(Game, "MenuUpgrade", 1.0f);
                    option.Axis(option.Data, option.AxisValue);
                }
                else
                    option.AxisValue -= 1;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            float Spacing = 36.0f;
            Vector2 position = Position;
            foreach (MenuOption option in MenuOptions)
            {
                if (option != null)
                {
                    bool valid = option.SelectValidate(option.Data);
                    if (option == MenuOptions[Cursor])
                    {
                        sprite_batch.DrawString(Game.DefaultFont, ">", position - Vector2.UnitX * 25.0f, Color.White);
                    }

                    if (option.SpecialHighlight)
                    {
                        Vector2 measured = Game.DefaultFont.MeasureString(option.Text);
                        sprite_batch.DrawString(Game.DefaultFont, ">", position - Vector2.UnitX * 20.0f, Color.Yellow);
                        sprite_batch.DrawString(Game.DefaultFont, "<", position + Vector2.UnitX * (measured.X + 5.0f), Color.Yellow);
                        sprite_batch.DrawString(Game.DefaultFont, option.Text, position, Color.Yellow);
                    }
                    else
                    {
                        Color color = valid ? Color.White : Color.Gray;
                        sprite_batch.DrawString(Game.DefaultFont, option.Text, position, color);
                    }

                    if (option.SubText != null)
                    {
                        position += Vector2.UnitY * Spacing;
                        Color color = valid ? Color.LightGray : Color.Gray;
                        sprite_batch.DrawString(Game.DefaultFont, option.SubText, position + Vector2.UnitX * 10.0f, color);
                    }
                }
                position += Vector2.UnitY * Spacing;
            }
        }
    }
}
