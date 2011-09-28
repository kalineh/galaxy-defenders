//
// ShaderSceneries.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CShaderScenery
        : CScenery
    {
        public CShaderScenery(CWorld world)
            : base(world)
        {
            ReloadShaders();
        }

        public override void Update()
        {
            base.Update();

            if (CInput.IsRawKeyDown(Keys.LeftShift) && CInput.IsRawKeyPressed(Keys.R))
            {
                ReloadShaders();
            }
        }

        protected virtual void ReloadShaders()
        {
        }
    }
}

