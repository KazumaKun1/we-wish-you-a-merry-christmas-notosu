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
    public class Intro : StoryboardObjectGenerator
    {
public override bool Multithreaded => true;

        public override void Generate()
        {
            var receptors = GetLayer("receptors");
            var notes = GetLayer("notes");

            // Define general values
            var starttime = 0;
            var endtime = 47555;
            var duration = endtime - starttime;

            // Playfield dimensions
            var width = 250f;
            var height = 400;

            // Note initialization parameters
            var bpm = 161.34f;
            var offset = -46f;

            // DrawInstance configuration
            var updatesPerSecond = 100;
            var scrollSpeed = 1000f;
            var rotateNotesToFaceReceptor = false;
            var fadeTime = 60;

            var receptorBitmap = GetMapsetBitmap("sb/sprites/receiver.png"); // Receptor sprite
            var receptorWidth = receptorBitmap.Width;

            Playfield field = new Playfield();
            field.initilizePlayField(receptors, notes, starttime, endtime, width, height, 50);
            field.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw = new DrawInstance(field, starttime, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            field.Scale(OsbEasing.InQuad, starttime, starttime, new Vector2(0.01f));
            field.Scale(OsbEasing.InQuad, starttime, 11854, new Vector2(0.5f));
            field.Resize(OsbEasing.InQuad, starttime, starttime, -250, height);
            field.Resize(OsbEasing.InQuad, starttime, 11854, width, height);

            var startEffect = 12040;
            var valueBeforeNextBeat = 12412;
            var beatDuration = Math.Abs(startEffect - valueBeforeNextBeat);

            var endRotate = 47555;
            var startRotate = 35655;

            var endEffect = startRotate;
            
            var index = 0;

            var moduloValue = 2;

            var moveValue = 65;

            while (startEffect <= startRotate) {
                if (startEffect >= 23754 && startEffect <= startRotate) {
                    field.Resize(OsbEasing.OutCirc, startEffect, startEffect, width + 50, height);
                    field.Resize(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, width, height);
                } 
                
                field.Scale(OsbEasing.None, startEffect, startEffect, index % 2 == 0 ? new Vector2(1f, 0.1f) : new Vector2(0.1f, 1f), true);
                field.Scale(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, new Vector2(0.5f), true);

                field.MoveColumnRelativeX(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.one);
                field.MoveColumnRelativeX(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.two);

                field.MoveColumnRelativeX(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.three);
                field.MoveColumnRelativeX(OsbEasing.OutCirc, startEffect, startEffect + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.four);

                startEffect += beatDuration;
                
                index += 1;
            }

            index = 0;

            field.MoveColumnRelative(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0 % moduloValue == 0 ? moveValue : moveValue * -1, 0), ColumnType.one);
            field.MoveColumnRelative(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0 % moduloValue == 0 ? moveValue * -1 : moveValue, 0), ColumnType.two);

            field.MoveColumnRelative(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0 % moduloValue == 0 ? moveValue : moveValue * -1, 0), ColumnType.three);
            field.MoveColumnRelative(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0 % moduloValue == 0 ? moveValue * -1 : moveValue, 0), ColumnType.four);

            moveValue = 45;

            Playfield field2 = new Playfield();
            field2.initilizePlayField(receptors, notes, startRotate, endRotate, width, height, 50);
            field2.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw2 = new DrawInstance(field2, startRotate, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);
            
            while (startRotate <= endRotate) {
                field.Scale(OsbEasing.None, startRotate, startRotate, index % 2 == 0 ? new Vector2(1f, 0.1f) : new Vector2(0.1f, 1f), true);
                field.Scale(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0.5f), true);

                field.Rotate(OsbEasing.OutCirc, startRotate, startRotate + 300, index % 2 == 0 ? Math.PI / -6 : Math.PI / 6);
                field.Rotate(OsbEasing.OutCirc, startRotate + 300, startRotate + beatDuration, index % 2 == 0 ? Math.PI / 6 : Math.PI / -6);

                field.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.one);
                field.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.two);

                field.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.three);
                field.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.four);

                field2.Scale(OsbEasing.None, startRotate, startRotate, index % 2 == 0 ? new Vector2(1f, 0.1f) : new Vector2(0.1f, 1f), true);
                field2.Scale(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, new Vector2(0.5f), true);

                field2.Rotate(OsbEasing.OutCirc, startRotate, startRotate + 300, index % 2 == 1 ? Math.PI / -6 : Math.PI / 6);
                field2.Rotate(OsbEasing.OutCirc, startRotate + 300, startRotate + beatDuration, index % 2 == 1 ? Math.PI / 6 : Math.PI / -6);

                field2.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.one);
                field2.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.two);

                field2.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue : moveValue * -1, ColumnType.three);
                field2.MoveColumnRelativeY(OsbEasing.OutCirc, startRotate, startRotate + beatDuration, index % moduloValue == 0 ? moveValue * -1 : moveValue, ColumnType.four);

                field.fadeAt(startRotate, startRotate + beatDuration, index % 4 == 0 ? 0f : 1f);
                field2.fadeAt(startRotate, startRotate + beatDuration, index % 4 == 0 ? 1f : 0f);

                startRotate += beatDuration;
                
                index += 1;
            }

            field.moveFieldY(OsbEasing.OutCirc, 35655, 35655 + beatDuration, 100);
            field2.moveFieldY(OsbEasing.OutCirc, 35655, 35655 + beatDuration, 100);

            var startZoom = 46253;
            
            var flashLayer = GetLayer("flash");
            var whiteBG = flashLayer.CreateSprite("sb/white.png", OsbOrigin.Centre);

            whiteBG.Scale(startZoom, startZoom, 2, 2);
            whiteBG.Fade(startZoom, endRotate, 0, 1);

            field.Scale(OsbEasing.None, startZoom, endRotate, new Vector2(1.5f), true);
            field2.Scale(OsbEasing.None, startZoom, endRotate, new Vector2(1.5f), true);

            draw.drawNotesByOriginToReceptor(duration, false);

            draw2.drawNotesByOriginToReceptor(11900, false);
        }
    }
}
