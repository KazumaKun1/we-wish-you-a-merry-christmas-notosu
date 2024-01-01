using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using StorybrewCommon.Animations;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace StorybrewScripts
{
    public class Bridge : StoryboardObjectGenerator
    {
        public override bool Multithreaded => true;

                    // Playfield dimensions
            float width = 250f;
            float height = 400;
        public override void Generate()
        {
            var receptors = GetLayer("receptors");
            var notes = GetLayer("notes");

            // Define general values
            var starttime = 83256;
            var endtime = 120444;
            var duration = endtime - starttime;

            // Note initialization parameters
            var bpm = 161.34f;
            var offset = -46f;

            // DrawInstance configuration
            var updatesPerSecond = 100;
            var scrollSpeed = 1250f;
            var rotateNotesToFaceReceptor = false;
            var fadeTime = 60;

            var receptorBitmap = GetMapsetBitmap("sb/sprites/receiver.png"); // Receptor sprite
            var receptorWidth = receptorBitmap.Width;

            Playfield field = new Playfield();
            field.initilizePlayField(receptors, notes, starttime, endtime, width, height, 50);
            field.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw = new DrawInstance(field, starttime, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            var flashLayer = GetLayer("flash2");
            var whiteBG = flashLayer.CreateSprite("sb/white.png", OsbOrigin.Centre);

            whiteBG.Scale(83256, 83256, 2, 2);
            whiteBG.Fade(83256, 83628, 1, 0);
            whiteBG.Additive(83256, 83256);
            
            List<ColumnType[]> switchEffects = new List<ColumnType[]>{
                new ColumnType[] { ColumnType.one, ColumnType.two, ColumnType.three, ColumnType.four },
                new ColumnType[] { ColumnType.one, ColumnType.three, ColumnType.two, ColumnType.four },
                new ColumnType[] { ColumnType.one, ColumnType.four, ColumnType.two, ColumnType.three },
                new ColumnType[] { ColumnType.two, ColumnType.one, ColumnType.four, ColumnType.two }
            };

            YAxisDropSwitchEffect(field, starttime, 101106, Math.Abs(83442 - starttime), switchEffects);

            SlowResizeEffect(field, starttime, 101106, Math.Abs(89113 - starttime));
            
            var index = 0;
            var start = 101106;
            var beatDuration = (101478 - 101106);

            var value = 10;

            field.Resize(OsbEasing.OutCirc, 101106, 101106 + 1000, 0.1f, height, true);

            field.MoveColumnRelative(OsbEasing.OutCirc, start, start + beatDuration, new Vector2(-13, -75), ColumnType.one);
            field.MoveColumnRelative(OsbEasing.OutCirc, start, start + beatDuration, new Vector2(0, 5), ColumnType.two);
            field.MoveColumnRelative(OsbEasing.OutCirc, start, start + beatDuration, new Vector2(0, 5), ColumnType.three);
            field.MoveColumnRelative(OsbEasing.OutCirc, start, start + beatDuration, new Vector2(13, -75), ColumnType.four);

            while (start <= 118957) {
                Vector2 position = field.GetReceptorPositionOf(ColumnType.one, start + beatDuration);
                Vector2 position2 = field.GetReceptorPositionOf(ColumnType.two, start + beatDuration);
                Vector2 position3 = field.GetReceptorPositionOf(ColumnType.three, start + beatDuration);
                Vector2 position4 = field.GetReceptorPositionOf(ColumnType.four, start + beatDuration);

                Vector2 point = Utility.PivotPoint(position, new Vector2(320, 240), Math.PI / 15);
                Vector2 point2 = Utility.PivotPoint(position2, new Vector2(320, 240), Math.PI / -15);
                Vector2 point3 = Utility.PivotPoint(position3, new Vector2(320, 240), Math.PI / 15);
                Vector2 point4 = Utility.PivotPoint(position4, new Vector2(320, 240), Math.PI / -15);

                point.X -= position.X;
                point.Y -= position.Y;

                point2.X -= position2.X;
                point2.Y -= position2.Y;

                point3.X -= position3.X;
                point3.Y -= position3.Y;

                point4.X -= position4.X;
                point4.Y -= position4.Y;

                field.RotateColumn(OsbEasing.None, start, start + beatDuration, Math.PI / value, ColumnType.one, CenterType.receptor);
                field.RotateColumn(OsbEasing.None, start, start + beatDuration, Math.PI / value * -1, ColumnType.two, CenterType.receptor); 
                field.RotateColumn(OsbEasing.None, start, start + beatDuration, Math.PI / value, ColumnType.three, CenterType.receptor); 
                field.RotateColumn(OsbEasing.None, start, start + beatDuration, Math.PI / value * -1, ColumnType.four, CenterType.receptor);

                start += beatDuration;                
            
                index++;
            }

            field.moveFieldY(OsbEasing.OutCirc, 101106, 101106 + 1000, 170);

            NoteRubberScaleEffect(field, 101106, 118957, beatDuration);

            field.Scale(OsbEasing.InCirc, 118957, 120444, new Vector2(1.5f));
            
            field.RotateColumn(OsbEasing.None, 118957, 120444, Math.PI / 0.5, ColumnType.one, CenterType.receptor);
            field.RotateColumn(OsbEasing.None, 118957, 120444, Math.PI / -0.5, ColumnType.two, CenterType.receptor); 
            field.RotateColumn(OsbEasing.None, 118957, 120444, Math.PI / 0.5, ColumnType.three, CenterType.receptor); 
            field.RotateColumn(OsbEasing.None, 118957, 120444, Math.PI / -0.5, ColumnType.four, CenterType.receptor);

            draw.drawNotesByOriginToReceptor(duration, false);

            var flashLayer1 = GetLayer("flash");
            var whiteBG1 = flashLayer1.CreateSprite("sb/white.png", OsbOrigin.Centre);

            whiteBG1.Scale(118957, 118957, 2, 2);
            whiteBG1.Fade(118957, 120444, 0, 1);
        }

        public void YAxisDropSwitchEffect(Playfield field, int start, int end, int effectDuration, List<ColumnType[]> switchEffects) {
            var startBeat = start;

            var index = 0;
            var iterateIndex = 0;
            var effectIndex = 0;
            var effectCount = switchEffects.Count;
            
            var shouldStartNegativeY = false;
            var switchColumn = false;

            while (startBeat <= end) {
                var currentEffectList = switchEffects[effectIndex];
                var value = shouldStartNegativeY ? -75 : 75;

                if (switchColumn) {
                    field.MoveColumnRelativeY(OsbEasing.OutCirc, startBeat, startBeat + effectDuration, value, currentEffectList[2]);
                    field.MoveColumnRelativeY(OsbEasing.OutCirc, startBeat, startBeat + effectDuration, value, currentEffectList[3]);
                } else {
                    field.MoveColumnRelativeY(OsbEasing.OutCirc, startBeat, startBeat + effectDuration, value, currentEffectList[0]);
                    field.MoveColumnRelativeY(OsbEasing.OutCirc, startBeat, startBeat + effectDuration, value, currentEffectList[1]);
                }

                startBeat += effectDuration;
                index += 1;

                if (iterateIndex == 1) {
                    iterateIndex = 0;
                    switchColumn = !switchColumn;
                } else {
                    iterateIndex++;
                }

                if (index % 16 == 0) {
                    if (effectIndex < effectCount - 1) { 
                        effectIndex++;
                    } else {
                        effectIndex = 0;
                    }
                }

                shouldStartNegativeY = !shouldStartNegativeY;
            }
        }
        public void SlowResizeEffect(Playfield field ,int startScale, int endScale, int beatDuration) {
            var index = 0;

            while (index < 3) {
                field.Resize(OsbEasing.InOutCirc, startScale, startScale + beatDuration, index % 2 == 0 ? width + 300 : (width * -1) - 300, height);

                startScale += beatDuration;
                
                index += 1;
            }
        }

        public void NoteRubberScaleEffect(Playfield field ,int startScale, int endScale, int beatDuration) {
            var index = 0;

            while (startScale <= endScale) {
                field.Scale(OsbEasing.None, startScale, startScale, new Vector2(1f), true);
                field.Scale(OsbEasing.OutCirc, startScale, startScale + beatDuration, new Vector2(0.5f), true);

                startScale += beatDuration;
                
                index += 1;
            }
        }
    }
}
