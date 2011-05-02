//
// TextLabel.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CTextLabel
    {
        public enum EAlignment
        {
            Left,
            Center,
            Right,
        }

        public EAlignment Alignment { get; set; }

        private object _Value;
        public object Value
        {
            get { return _Value; }
            set { SetValue(value); }
        }
        public string Text { get; private set; }

        public CTextLabel()
        {
            Text = "";
            Alignment = EAlignment.Center;
        }

        private void SetValue(object value)
        {
            if (_Value == value)
                return;

            if (value == null)
            {
                _Value = null;
                Text = "";
                return;
            }

            if (_Value != null)
            {
                Type lhs = _Value.GetType();
                Type rhs = value.GetType();
                if (lhs == rhs)
                {
                    if (lhs == typeof(int))
                    {
                        if ((int)_Value == (int)value)
                            return;
                    }
                    else if (lhs == typeof(float))
                    {
                        if ((float)_Value == (float)value)
                            return;
                    }
                }
            }

            _Value = value;
            Text = _Value.ToString();
        }

        public void Draw(SpriteBatch sprite_batch, SpriteFont font, Vector2 position, Color color)
        {
            Draw(sprite_batch, font, position, color, 1.0f);
        }

        public void Draw(SpriteBatch sprite_batch, SpriteFont font, Vector2 position, Color color, float scale)
        {
            Color drop_shadow_color = new Color(0.0f, 0.0f, 0.0f, 0.35f);
            Vector2 drop_shadow_offset = new Vector2(2.0f, 2.0f);
            switch (Alignment)
            {
                case EAlignment.Left:
                    sprite_batch.DrawStringAlignLeft(font, position + drop_shadow_offset, Text, drop_shadow_color, scale);
                    sprite_batch.DrawStringAlignLeft(font, position, Text, color, scale);
                    break;

                case EAlignment.Center:
                    sprite_batch.DrawStringAlignCenter(font, position + drop_shadow_offset, Text, drop_shadow_color, scale);
                    sprite_batch.DrawStringAlignCenter(font, position, Text, color, scale);
                    break;

                case EAlignment.Right:
                    sprite_batch.DrawStringAlignRight(font, position + drop_shadow_offset, Text, drop_shadow_color, scale);
                    sprite_batch.DrawStringAlignRight(font, position, Text, color, scale);
                    break;

            }
        }
    }
}
