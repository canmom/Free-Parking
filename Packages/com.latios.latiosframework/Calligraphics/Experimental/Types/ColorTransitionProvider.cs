using Latios.Kinemation.TextBackend;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Latios.Calligraphics
{
    public struct ColorTransitionProvider : ITransitionProvider
    {
        public void Initialize(ref TextAnimationTransition transition, ref Rng.RngSequence rng, GlyphMapper glyphMapper)
        {
            if (transition.currentLoop > 0 && (transition.endBehavior & TransitionEndBehavior.KeepFinalValue) == TransitionEndBehavior.KeepFinalValue)
            {
                (transition.startValueBlColor, transition.endValueBlColor) = (transition.endValueBlColor, transition.startValueBlColor);
                (transition.startValueTrColor, transition.endValueTrColor) = (transition.endValueTrColor, transition.startValueTrColor);
                (transition.startValueBrColor, transition.endValueBrColor) = (transition.endValueBrColor, transition.startValueBrColor);
                (transition.startValueTlColor, transition.endValueTlColor) = (transition.endValueTlColor, transition.startValueTlColor);
            }
        }

        public void SetValue(ref DynamicBuffer<RenderGlyph> renderGlyphs, TextAnimationTransition transition, GlyphMapper glyphMapper, int startIndex, int endIndex, float normalizedTime)
        {
            Color32 blValue = transition.startValueBlColor;
            Color32 trValue = transition.startValueTrColor;
            Color32 brValue = transition.startValueBrColor;
            Color32 tlValue = transition.startValueTlColor;

            if (transition.currentTime >= transition.transitionTimeOffset)
            {
                blValue = Interpolation.Interpolate(transition.startValueBlColor, transition.endValueBlColor, normalizedTime, transition.interpolation);
                trValue = Interpolation.Interpolate(transition.startValueTrColor, transition.endValueTrColor, normalizedTime, transition.interpolation);
                brValue = Interpolation.Interpolate(transition.startValueBrColor, transition.endValueBrColor, normalizedTime, transition.interpolation);
                tlValue = Interpolation.Interpolate(transition.startValueTlColor, transition.endValueTlColor, normalizedTime, transition.interpolation);
                
                //Apply new values
                for (int i = startIndex; i <= math.min(endIndex, renderGlyphs.Length - 1); i++)
                {
                    var renderGlyph = renderGlyphs[i];
                    SetColorValue(ref renderGlyph, blValue, brValue, tlValue, trValue);
                    renderGlyphs[i] = renderGlyph;
                }
            }
        }
        
        public void SetColorValue(ref RenderGlyph glyph, Color32 blColor, Color32 brColor, Color32 tlColor, Color32 trColor)
        {
            glyph.blColor = blColor;
            glyph.brColor = brColor;
            glyph.tlColor = tlColor;
            glyph.trColor = trColor;
        }
    }
}