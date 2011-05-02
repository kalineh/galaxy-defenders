//
// ScorePanel.cs
//

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Galaxy
{
    public class CScorePanel
    {
        public CGalaxy Game { get; set; }
        public Vector2 BasePosition { get; set; }
        public int Counter { get; set; }
        public CVisual Header { get; set; }
        public CVisual Item { get; set; }
        public CVisual Footer { get; set; }
        public CVisual Highlight { get; set; }
        public CTextLabel StageHeader { get; set; }
        public CTextLabel ScoreHeader { get; set; }
        public CTextLabel MedalHeader { get; set; }
        public List<CTextLabel> Indexes { get; set; }
        public List<CTextLabel> Scores { get; set; }
        public List<CVisual> Medals { get; set; }
        public CTextLabel ContinueLabel { get; set; }
        public CVisual ContinueButton { get; set; }
        public bool ShowContinue { get; set; }
        public int HighlightIndex { get; set; }

        public const int DisplayInterval = 2;

        public CScorePanel(CGalaxy game)
        {
            Game = game;
            Counter = -1;
            HighlightIndex = -1;

            Header = CVisual.MakeSpriteCached1(Game, "Textures/UI/ScoreboardHeader");
            Item = CVisual.MakeSpriteCached1(Game, "Textures/UI/ScoreboardItem");
            Footer = CVisual.MakeSpriteCached1(Game, "Textures/UI/ScoreboardFooter");
            Highlight = CVisual.MakeSpriteCached1(Game, "Textures/UI/ScoreboardHighlight");

            StageHeader = new CTextLabel() { Value = "Stage", Alignment = CTextLabel.EAlignment.Center };
            ScoreHeader = new CTextLabel() { Value = "Best Score", Alignment = CTextLabel.EAlignment.Center };
            MedalHeader = new CTextLabel() { Value = "Rank", Alignment = CTextLabel.EAlignment.Center };

            Indexes = new List<CTextLabel>();
            Scores = new List<CTextLabel>();
            Medals = new List<CVisual>();

            SProfileGameData data = CSaveData.GetCurrentGameData(Game);

            for (int i = 0; i < 12; ++i)
            {
                int score = data.StageScores[i];

                string medal_type = null;
                switch (data.StageMedals[i])
                {
                    case 1: medal_type = "Clear"; break;
                    case 2: medal_type = "Bronze"; break;
                    case 3: medal_type = "Silver"; break;
                    case 4: medal_type = "Gold"; break;
                    case 5: medal_type = "Platinum"; break;
                }

                Indexes.Add(new CTextLabel() { Value = String.Format("{0}", i + 1), Alignment = CTextLabel.EAlignment.Center });
                Scores.Add(new CTextLabel() { Value = String.Format("{0}", score), Alignment = CTextLabel.EAlignment.Center });

                if (String.IsNullOrEmpty(medal_type))
                    Medals.Add(null);
                else
                    Medals.Add(CVisual.MakeSpriteUncached(Game, "Textures/UI/Medal" + medal_type));
            }

            ShowContinue = true;
            ContinueLabel = new CTextLabel() { Value = "Continue", Alignment = CTextLabel.EAlignment.Left };
            ContinueButton = CVisual.MakeSpriteCached1(Game, "Textures/UI/Xbox/xboxControllerButtonX");

            BasePosition = new Vector2(1920.0f / 2.0f + 8.0f, 140.0f);
        }

        public void Update()
        {
            if (Counter == -1)
                return;

            Counter += 1;
        }

        public void SetVisible(bool visible)
        {
            Counter = visible ? 0 : -1;    
        }

        public bool IsVisible()
        {
            return Counter != -1;    
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            if (Counter == -1)
                return;

            Vector2 base_ = BasePosition;
            float step = 36.0f;

            Header.Draw(sprite_batch, base_, 0.0f);
            StageHeader.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(-202.0f, -7.0f), Color.White);
            ScoreHeader.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(0.0f, -7.0f), Color.White);
            MedalHeader.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(206.0f, -7.0f), Color.White);

            for (int i = 0; i < 12; ++i)
            {
                Item.Draw(sprite_batch, base_ + new Vector2(0.0f, step * (i + 1)), 0.0f);

                if (i == HighlightIndex && (Game.GameFrame % 30 > 10))
                    Highlight.Draw(sprite_batch, base_ + new Vector2(0.0f, step * (i + 1)), 0.0f);
            }

            if (ShowContinue)
                Footer.Draw(sprite_batch, base_ + new Vector2(0.0f, step * 13.0f + 8.0f), 0.0f);

            for (int i = 0; i < 12; ++i)
            {
                if (Counter < i * DisplayInterval)
                    break;

                CTextLabel index = Indexes[i];
                CTextLabel score = Scores[i];
                CVisual medal = Medals[i];

                float y = step * (i + 1) - 2.0f;

                index.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(-202.0f, y), Color.White);

                if (medal != null)
                {
                    score.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(0.0f, y), Color.White);
                    medal.Draw(sprite_batch, base_ + new Vector2(206.0f, y - 1.0f), 0.0f, 0.5f);
                }
            }

            if (ShowContinue)
            {
                if (Counter > DisplayInterval * 13)
                {
                    float scale = 1.0f + (float)Math.Sin(Counter * 0.1f) * 0.01f;
                    ContinueLabel.Draw(sprite_batch, Game.GameRegularFont, base_ + new Vector2(68.0f, step * 13.0f + 8.0f), Color.LightGray, scale);
                    ContinueButton.Draw(sprite_batch, base_ + new Vector2(48.0f, step * 13.0f + 8.0f), 0.0f);
                }
            }
        }
    }
}
