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
        public bool Visible { get; set; }
        public static Texture2D MenuItemTexture { get; set; }
        public static Texture2D MenuItemTextureSmall { get; set; }
        public static Texture2D MenuItemSelectedTexture { get; set; }
        public static Texture2D MenuItemSelectedTextureSmall { get; set; }
        public static Texture2D MenuItemInvalidTexture { get; set; }

        public static void LoadMenuTextures(CGalaxy game)
        {
            MenuItemTexture = CContent.LoadTexture2D(game, "Textures/UI/Menu/MenuItem");
            MenuItemTextureSmall = CContent.LoadTexture2D(game, "Textures/UI/Menu/MenuItemSmall");
            MenuItemSelectedTexture = CContent.LoadTexture2D(game, "Textures/UI/Menu/MenuItemSelected");
            MenuItemSelectedTextureSmall = CContent.LoadTexture2D(game, "Textures/UI/Menu/MenuItemSelectedSmall");
            MenuItemInvalidTexture = CContent.LoadTexture2D(game, "Textures/UI/Menu/MenuItemInvalid");
        }

        public CGalaxy Game { get; set; }
        public delegate void MenuSelectFunction(object tag);
        public delegate bool MenuSelectValidateFunction(object tag);
        public delegate void MenuHighlightFunction(object tag);
        public delegate void MenuAxisFunction(object tag, int axis);
        public delegate bool MenuAxisValidateFunction(object tag, int axis);
        public delegate void MenuCustomRender(object tag, SpriteBatch sprite_batch, Vector2 position);

        public enum PanelType
        {
            None,
            Normal,
            Small,
        };

        public class CMenuOption
        {
            public string Text
            {
                get { return TextLabel.Text; }
                set { TextLabel.Value = value; }
            }
            public string SubText
            {
                get { return SubTextLabel.Text; }
                set { SubTextLabel.Value = value; }
            }

            public CTextLabel TextLabel;
            public CTextLabel SubTextLabel;
            public PanelType PanelType;
            public string IconName;
            public CVisual IconVisual;
            public bool SpecialHighlight;
            public MenuSelectFunction Select;
            public MenuSelectValidateFunction SelectValidate;
            public MenuHighlightFunction Highlight;
            public MenuAxisFunction Axis;
            public MenuAxisValidateFunction AxisValidate;
            public MenuCustomRender CustomRender;
            public bool CancelOption;
            public object Data;
            public int AxisValue;
            public bool Visible;

            public CMenuOption()
            {
                Select = (tag) => { };
                SelectValidate = (tag) => { return true; };
                Highlight = (tag) => { };
                Axis = (tag, axis) => { };
                AxisValidate = (tag, axis) => { return false; };
                CustomRender = (tag, sprite_batch, position) => { };
                PanelType = PanelType.Normal;
                Visible = true;
                TextLabel = new CTextLabel();
                SubTextLabel = new CTextLabel();
            }
        }
        public List<CMenuOption> MenuOptions { get; set; }
        public IEnumerable<CMenuOption> VisibleMenuOptions
        {
            get
            {
                foreach (CMenuOption option in MenuOptions)
                {
                    if (!option.Visible)
                        continue;

                    yield return option;
                }
            }     
        }

        public int Cursor { get; set; }
        public Vector2 Position { get; set; }
        public bool Centered { get; set; }

        public CMenu(CGalaxy game)
        {
            Game = game;
            Cursor = 0;
            MenuOptions = new List<CMenuOption>();
            Position = Vector2.Zero;
            Visible = true;
        }

        public void Update()
        {
            if (!Visible)
                return;

            if (Game.Input.IsKeyPressed(Keys.Escape) || Game.Input.IsPadCancelPressedAny())
            {
                foreach (CMenuOption cancel_option in VisibleMenuOptions)
                {
                    if (cancel_option.CancelOption)
                    {
                        CAudio.PlaySound("MenuCancel");
                        cancel_option.Highlight(cancel_option.Data);
                        cancel_option.Select(cancel_option.Data);
                    }
                }
            }

            int offset = GetCursorInputOffset();
            int previous = Cursor;

            Cursor = Cursor + offset;
            Cursor = Math.Max(Cursor, 0);
            Cursor = Math.Min(Cursor, MenuOptions.Count - 1);

            if ((MenuOptions[Cursor] == null || MenuOptions[Cursor].SelectValidate(MenuOptions[Cursor].Data) == false))
                MoveToFirstValid();

            CMenuOption option = MenuOptions[Cursor];

            if (Cursor != previous)
            {
                CAudio.PlaySound("MenuMoveItem");

                option.Highlight(option.Data);
                option.Axis(option.Data, option.AxisValue);
            }

            if (Game.Input.IsKeyPressed(Keys.Enter) || Game.Input.IsPadConfirmPressedAny())
            {
                CAudio.PlaySound("MenuSelect");
                option.Select(option.Data);
            }

            if (Game.Input.IsKeyPressed(Keys.Left) || Game.Input.IsPadLeftPressedAny())
            {
                option.AxisValue -= 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                {
                    CAudio.PlaySound("MenuDowngrade");
                    option.Axis(option.Data, option.AxisValue);
                }
                else
                    option.AxisValue += 1;
            }

            if (Game.Input.IsKeyPressed(Keys.Right) || Game.Input.IsPadRightPressedAny())
            {
                option.AxisValue += 1;
                if (option.AxisValidate(option.Data, option.AxisValue))
                { 
                    CAudio.PlaySound("MenuUpgrade");
                    option.Axis(option.Data, option.AxisValue);
                }
                else
                    option.AxisValue -= 1;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            if (!Visible)
                return;

            float Spacing = 40.0f;
            Vector2 position = Position;
            foreach (CMenuOption option in VisibleMenuOptions)
            {
                if (option != null)
                {
                    bool valid = option.SelectValidate(option.Data);
                    bool selected = option == MenuOptions[Cursor];

                    if (option.PanelType != PanelType.None)
                    {
                        Texture2D texture = null;
                        if (valid)
                        {
                            if (selected)
                            {
                                texture = option.PanelType == PanelType.Small ? MenuItemSelectedTextureSmall : MenuItemSelectedTexture;
                            }
                            else
                            {
                                texture = option.PanelType == PanelType.Small ? MenuItemTextureSmall : MenuItemTexture;
                            }
                        }
                        else
                        {
                            texture = MenuItemInvalidTexture;
                        }

                        Color color = valid ? Color.White : Color.Gray;

                        Vector2 center = position + new Vector2(texture.Width, texture.Height) / 2.0f;
                        sprite_batch.Draw(texture, position, null, Color.White);
                        option.TextLabel.Draw(sprite_batch, Game.DefaultFont, center, color);
                        option.CustomRender(option.Data, sprite_batch, center);

                        position += Vector2.UnitY * 26.0f;
                    }
                    // NOTE: debug text menu! will not render with shadows or correct alignment
                    else
                    {
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

                        option.CustomRender(option.Data, sprite_batch, position);

                        if (option.SubText != null)
                        {
                            position += Vector2.UnitY * Spacing;
                            Color color = valid ? Color.LightGray : Color.Gray;
                            sprite_batch.DrawString(Game.DefaultFont, option.SubText, position + Vector2.UnitX * 10.0f, color);
                        }
                    }
                }
                position += Vector2.UnitY * Spacing;
            }
        }

        public int GetCursorInputOffset()
        {
            int offset = 0;
            if (Game.Input.IsKeyPressed(Keys.Down) || Game.Input.IsPadDownPressedAny())
            {
                offset += 1;
                while (Cursor + offset < MenuOptions.Count && (MenuOptions[Cursor + offset] == null || MenuOptions[Cursor + offset].SelectValidate(MenuOptions[Cursor + offset].Data) == false))
                    offset += 1;
            }
            if (Game.Input.IsKeyPressed(Keys.Up) || Game.Input.IsPadUpPressedAny())
            {
                offset -= 1;
                while (Cursor + offset > 0 && (MenuOptions[Cursor + offset] == null || MenuOptions[Cursor + offset].SelectValidate(MenuOptions[Cursor + offset].Data) == false))
                    offset -= 1;
            }
            return offset;
        }

        public void MoveToFirstValid()
        {
            int offset = 0;
            while (Cursor + offset < MenuOptions.Count && (MenuOptions[Cursor + offset] == null || MenuOptions[Cursor + offset].SelectValidate(MenuOptions[Cursor + offset].Data) == false))
                offset += 1;

            Cursor = Cursor + offset;
            Cursor = Math.Max(Cursor, 0);
            Cursor = Math.Min(Cursor, MenuOptions.Count - 1);
        }

        public void ForceRefresh()
        {
            CMenuOption option = MenuOptions[Cursor];
            option.Highlight(option.Data);
            option.Axis(option.Data, option.AxisValue);
        }

        public CVisual GetIcon(CMenuOption option)
        {
            if (option.IconName == null)
                return null;

            if (option.IconVisual != null)
                option.IconVisual = CVisual.MakeSpriteFromGame(Game, option.IconName, Vector2.One, Color.White);

            return option.IconVisual;
        }

        public void TryRenderMenuOption(CMenuOption option, Vector2 position, SpriteBatch sprite_batch)
        {
            CVisual icon = GetIcon(option);
            if (icon == null)
                return;

            icon.Draw(sprite_batch, position, 0.0f);
        }
    }
}
