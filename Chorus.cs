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
    public class Chorus : StoryboardObjectGenerator
    {
        public override bool Multithreaded => true;
        
        public override void Generate()
        {
		    var receptors = GetLayer("receptors");
            var notes = GetLayer("notes");

            // Define general values
            var starttime = 47555;
            var endtime = 83256;
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

            var flashLayer = GetLayer("flash2");
            var whiteBG = flashLayer.CreateSprite("sb/white.png", OsbOrigin.Centre);

            whiteBG.Scale(47555, 47555, 2, 2);
            whiteBG.Fade(47555, 47927, 1, 0);
            whiteBG.Additive(47555, 47927);

            Playfield field = new Playfield();
            field.initilizePlayField(receptors, notes, starttime, endtime, width, height, 50);
            field.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw = new DrawInstance(field, starttime, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            Playfield field2 = new Playfield();
            field2.initilizePlayField(receptors, notes, starttime, endtime, width, height, 50, true);
            field2.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw2 = new DrawInstance(field2, starttime, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            var startKick = 47555;
            var valueBeforeNextKick = 47927;
            var kickDuration = Math.Abs(startKick - valueBeforeNextKick);

            field.MoveColumnRelativeY(OsbEasing.OutCirc, startKick, startKick, 360, ColumnType.all);
            field.MoveColumnRelativeY(OsbEasing.OutCirc, startKick, startKick + kickDuration, -360, ColumnType.all);
            field2.MoveColumnRelativeY(OsbEasing.OutCirc, startKick, startKick, 360, ColumnType.all);
            field2.MoveColumnRelativeY(OsbEasing.OutCirc, startKick, startKick + kickDuration, -360, ColumnType.all);

            var startSwipe = 59827;
            var valueBeforeNextSwipe = 60571;
            var swipeDuration = Math.Abs(startSwipe - valueBeforeNextSwipe);

            var index = 0;

            Playfield field3 = new Playfield();
            field3.initilizePlayField(receptors, notes, startSwipe, 69868, width, height, 50);
            field3.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw3 = new DrawInstance(field3, startSwipe, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            Playfield field4 = new Playfield();
            field4.initilizePlayField(receptors, notes, startSwipe, 69868, width, height, 50, true);
            field4.initializeNotes(Beatmap.HitObjects.ToList(), bpm, offset, true, false);

            DrawInstance draw4 = new DrawInstance(field4, startSwipe, scrollSpeed, updatesPerSecond, OsbEasing.None, rotateNotesToFaceReceptor, fadeTime, fadeTime);

            field3.Resize(OsbEasing.None, startSwipe, startSwipe, ((width * 2) + 50) * -1, height);   
            field4.Resize(OsbEasing.None, startSwipe, startSwipe, ((width * 2) + 50) * -1, height);   
            field3.MoveColumnRelativeX(OsbEasing.None, startSwipe, startSwipe, 1500, ColumnType.all);
            field4.MoveColumnRelativeX(OsbEasing.None, startSwipe, startSwipe, 1500, ColumnType.all);

            GlowingResizableEffect(field, field2, 47555, 69868, kickDuration, width, height, true, startSwipe);
            GlowingResizableEffect(field3, field4, startSwipe, 69868, kickDuration, width, height, true, startSwipe);
            GlowingResizableEffect(field, field2, 71356, 83256, kickDuration, width, height, false, 0, true);

            while (startSwipe <= 69868) {   
                field.MoveColumnRelativeX(OsbEasing.OutCirc, startSwipe, startSwipe + swipeDuration - 100, index % 2 == 0 ? -750 : 750, ColumnType.all);
                field2.MoveColumnRelativeX(OsbEasing.OutCirc, startSwipe, startSwipe + swipeDuration - 100, index % 2 == 0 ? -750 : 750, ColumnType.all);

                field3.MoveColumnRelativeX(index % 2 == 0 ? OsbEasing.OutCirc : OsbEasing.OutSine, startSwipe, startSwipe + swipeDuration - 100, index % 2 == 0 ? -1500 : 1500, ColumnType.all);
                field4.MoveColumnRelativeX(index % 2 == 0 ? OsbEasing.OutCirc : OsbEasing.OutSine, startSwipe, startSwipe + swipeDuration - 100, index % 2 == 0 ? -1500 : 1500, ColumnType.all);

                startSwipe += swipeDuration;

                index += 1;
            }

            field.MoveColumnRelativeY(OsbEasing.OutCirc, 69868, 70240, 200, ColumnType.all);
            field.MoveColumnRelativeY(OsbEasing.InCirc, 70240, 71356, -200, ColumnType.all);
            field.Resize(OsbEasing.OutCirc, 69868, 70240, width - 250, height);
            field.Resize(OsbEasing.InCirc, 70240, 71356, width, height);

            field2.MoveColumnRelativeY(OsbEasing.OutCirc, 69868, 70240, 200, ColumnType.all);
            field2.MoveColumnRelativeY(OsbEasing.InCirc, 70240, 71356, -200, ColumnType.all);
            field2.Resize(OsbEasing.OutCirc, 69868, 70240, width - 250, height);
            field2.Resize(OsbEasing.InCirc, 70240, 71356, width, height);

            var startUpDownSwipe = 71727;
            var valueBeforeNextUpDown = 72471;

            var upDownDuration = Math.Abs(valueBeforeNextUpDown - startUpDownSwipe);

            var endUpDownSwipe = 83256;

            var index2 = 0;

            field.MoveColumnRelativeY(OsbEasing.OutExpo, 71356, startUpDownSwipe, 350, ColumnType.all);
            field2.MoveColumnRelativeY(OsbEasing.OutExpo, 71356, startUpDownSwipe, 350, ColumnType.all);
            field.Resize(OsbEasing.OutExpo, 71356, startUpDownSwipe, width, height * -1);
            field2.Resize(OsbEasing.OutExpo, 71356, startUpDownSwipe, width, height * -1);

            while (startUpDownSwipe <= endUpDownSwipe) {
                field.MoveColumnRelativeY(OsbEasing.OutCirc, startUpDownSwipe, startUpDownSwipe + upDownDuration, index2 % 2 == 0 ? -350 : 350, ColumnType.all);
                field2.MoveColumnRelativeY(OsbEasing.OutCirc, startUpDownSwipe, startUpDownSwipe + upDownDuration, index2 % 2 == 0 ? -350 : 350, ColumnType.all);

                field.Resize(OsbEasing.OutExpo, startUpDownSwipe, startUpDownSwipe + upDownDuration, 0, index2 % 2 == 0 ? height : height * -1);
                field2.Resize(OsbEasing.OutExpo, startUpDownSwipe, startUpDownSwipe + upDownDuration, 0, index2 % 2 == 0 ? height : height * -1);


                 startUpDownSwipe += upDownDuration;

                 index2 += 1;
            }

            draw.drawNotesByOriginToReceptor(duration, false);
            draw2.drawNotesByOriginToReceptor(duration, true);
            draw3.drawNotesByOriginToReceptor(10041, false);
            draw4.drawNotesByOriginToReceptor(10041, true);
        }

        public void GlowingResizableEffect(Playfield field, Playfield field2, int startKick, int endKick, int kickDuration, float width, float height, bool disableSwitch = false, int disableSwitchAt = 0, bool enablePhase2 = false) {
            var index = 0;
            var vectorIndex = 0;
            var moduloValue = 2;
            var indexCount = 0;
            var invertNow = false;
            var tempInvertNow = false;

            Vector2[] vectors = {new Vector2(0f, -75f), new Vector2(75f, 0f), new Vector2(0f, 75f), new Vector2(-75f, 0f)};
            Vector2[] originalVectors = {new Vector2(0f, 75f), new Vector2(-75f, 0f), new Vector2(0f, -75f), new Vector2(75f, 0f)};

            while (startKick <= endKick) {
                if (disableSwitch) {
                    field.Scale(OsbEasing.None, startKick, startKick, new Vector2(1.5f, 1.5f), true, CenterType.receptor);
                    field.Scale(index % 2 == 0 ? OsbEasing.OutCirc : OsbEasing.OutSine, startKick, startKick + kickDuration, new Vector2(0.5f), true, CenterType.receptor);
                    field2.Scale(OsbEasing.None, startKick, startKick, new Vector2(2.25f, 2.25f), true, CenterType.receptor);
                    field2.Scale(index % 2 == 0 ? OsbEasing.OutCirc : OsbEasing.OutSine, startKick, startKick + kickDuration, new Vector2(0.5f), true, CenterType.receptor);

                    if (!(startKick >= disableSwitchAt && disableSwitchAt <= endKick)) { 
                        Vector2 vector = vectors[vectorIndex];
                        Vector2 originalVector = originalVectors[vectorIndex];

                        field2.MoveColumnRelative(OsbEasing.OutCirc, startKick, startKick, vector, ColumnType.all);
                        field2.MoveColumnRelative(OsbEasing.OutCirc, startKick, startKick + kickDuration, originalVector, ColumnType.all);

                        if (enablePhase2) {
                            var startWidth = invertNow ? (width + 50) : (width + 50) * -1;
                            var endWidth = !invertNow ? (width + 50) : (width + 50) * -1;

                            if (tempInvertNow != invertNow) {
                                startWidth = tempInvertNow ? (width + 100) : (width + 100) * -1;
                                endWidth = !tempInvertNow ? (width + 100) : (width + 100) * -1;
                                tempInvertNow = invertNow;
                            }

                            field.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration - 100, index % 2 == 0 ? startWidth : endWidth, height);            
                            field2.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration- 100, index % 2 == 0 ? startWidth : endWidth, height);
                        } else {
                            field.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration - 100, index % 2 == 0 ? (width + 50) : (width + 50) * -1, height);            
                            field2.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration- 100, index % 2 == 0 ? (width + 50) : (width + 50) * -1, height);
                        }
                    }
                } else {
                    if (disableSwitchAt == 0) {
                        field.Scale(OsbEasing.None, startKick, startKick, new Vector2(1.5f, 1.5f), true, CenterType.receptor);
                        field.Scale(OsbEasing.OutCirc, startKick, startKick + kickDuration, new Vector2(0.5f), true, CenterType.receptor);
                        field2.Scale(OsbEasing.None, startKick, startKick, new Vector2(2.25f, 2.25f), true, CenterType.receptor);
                        field2.Scale(OsbEasing.OutCirc, startKick, startKick + kickDuration, new Vector2(0.5f), true, CenterType.receptor);

                        Vector2 vector = vectors[vectorIndex];
                        Vector2 originalVector = originalVectors[vectorIndex];

                        field2.MoveColumnRelative(OsbEasing.OutCirc, startKick, startKick, vector, ColumnType.all);
                        field2.MoveColumnRelative(OsbEasing.OutCirc, startKick, startKick + kickDuration, originalVector, ColumnType.all);

                        if (enablePhase2) {
                            var startWidth = invertNow ? (width + 50) : (width + 50) * -1;
                            var endWidth = !invertNow ? (width + 50) : (width + 50) * -1;

                            if (tempInvertNow != invertNow) {
                                startWidth = tempInvertNow ? (width + 50) : (width + 50) * -1;
                                endWidth = !tempInvertNow ? (width + 50) * -1 : (width + 50);
                                tempInvertNow = invertNow;
                            }

                            field.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration - 100, index % 2 == 0 ? startWidth : endWidth, height);            
                            field2.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration- 100, index % 2 == 0 ? startWidth : endWidth, height);
                        } else {
                            field.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration - 100, index % 2 == 0 ? (width + 50) : (width + 50) * -1, height);            
                            field2.Resize(OsbEasing.OutCirc, startKick, startKick + kickDuration- 100, index % 2 == 0 ? (width + 50) : (width + 50) * -1, height);
                        }
                    }
                }
                
                field.fadeAt(startKick, startKick, 0.75f);
                field2.fadeAt(startKick, startKick, 0.75f);

                startKick += kickDuration;
                
                index += 1;
                
                if (indexCount > 5) {
                    indexCount = 0;
                    invertNow = !invertNow;
                } else {
                    indexCount += 1;
                }

                if (vectorIndex < 3) {
                    vectorIndex++;
                } else {
                    vectorIndex = 0;
                }
            }
        }
    }
}
