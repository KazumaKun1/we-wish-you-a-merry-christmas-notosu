using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            OsbSprite back = GetLayer("white").CreateSprite("sb/white.png");
            back.Color(0, 0, 0, 0);
            back.ScaleVec(0, 854, 480);
            back.Fade(0, 172601, 1f, 1f);

            // var startGlow = -46;
            // var endGlow = 11854;
            // var intervalGlow = Math.Abs(1441 - startGlow);
            
            // var glowerLayer = GetLayer("Glows");

            // while (startGlow <= endGlow) {
            //     OsbSprite glow = glowerLayer.CreateSprite("sb/highlight.png");

            //     var x = Random(300, 800);
            //     var y = Random(100, 400);

            //     Log("x: " + x + " y: " + y);

            //     //var scale = Random(0.5, 2);
            //     var scale = 0.75;
            //     glow.Move(OsbEasing.None, startGlow, startGlow, x, x, y, y);

            //     glow.Scale(OsbEasing.InCirc, startGlow, startGlow + intervalGlow, scale - 0.5, scale);
            //     glow.Scale(OsbEasing.OutCirc, startGlow + intervalGlow, startGlow + intervalGlow * 2, scale, scale - 0.5);

            //     glow.Fade(OsbEasing.InCirc, startGlow, startGlow + intervalGlow, 0, 1);
            //     glow.Fade(OsbEasing.OutCirc, startGlow + intervalGlow, startGlow + intervalGlow * 2, 1, 0);

            //     glow.Additive(startGlow, startGlow);

            //     startGlow += 100;
            // }
        }
    }
}
