using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Mapset;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;
using System.Linq;
using System.Drawing;
using StorybrewCommon.Util;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class Image : StoryboardObjectGenerator
    {
        [Configurable]
        public string ImagePath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 1;

        [Configurable]
        public Vector2 StartImagePosition = new Vector2(320, 240);

        [Configurable]
        public Vector2 EndImagePosition = new Vector2(320, 300);

        [Configurable]
        public float Rotation = 0;

        [Configurable]
        public Vector2 Scale = new Vector2(1, 1);

        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;

        [Configurable]
        public OsbEasing Easing = OsbEasing.None;

        public override void Generate()
        {
            var bitmap = GetMapsetBitmap(ImagePath);
            var img = GetLayer("").CreateSprite(ImagePath, Origin);

            var spriteRotation = MathHelper.DegreesToRadians(Rotation);

            var startPosition = StartImagePosition;
            var endPosition = EndImagePosition;

            if (Scale.X != 1 || Scale.Y != 1)
                {
                    if (Scale.X != Scale.Y)
                        img.ScaleVec(StartTime, Scale.X, Scale.Y);
                    else img.Scale(StartTime, Scale.X);
                }
            if (spriteRotation != 0)
                    img.Rotate(StartTime, spriteRotation);
            img.Fade(StartTime - 500, StartTime, 0, Opacity);
            img.Fade(EndTime, EndTime + 500, Opacity, 0);
            img.Move(Easing, 0, EndTime, startPosition, endPosition);
        }
    }
}