using UnityEngine;

namespace SBF.Extentions.Colors
{
    public struct ColorHSV
    {
        private float hue, saturation, value, alpha;
        public float h
        {
            get => hue;
            set => hue = Mathf.Repeat(value, 1f);
        }

        public float s
        {
            get => saturation;
            set => saturation = Mathf.Clamp01(value);
        }

        public float v
        {
            get => value;
            set => this.value = Mathf.Clamp01(value);
        }

        public float a
        {
            get => alpha;
            set => alpha = Mathf.Clamp01(value);
        }

        /// <summary>
        /// Color from Hue Saturation, Value and Alpha
        /// All parameters are expected to be between 0 and 1
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        /// <param name="a">Alpha</param>
        public ColorHSV(float h, float s, float v, float a)
        {
            this.hue = Mathf.Repeat(h, 1f);
            this.saturation = Mathf.Clamp01(s);
            this.value = Mathf.Clamp01(v);
            this.alpha = Mathf.Clamp01(a);
        }

        /// <summary>
        /// Color from Hue Saturation and Value
        /// All parameters are expected to be between 0 and 1
        /// Alpha will be 1
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        public ColorHSV(float h, float s, float v)
        {
            this.hue = Mathf.Clamp01(h);
            this.saturation = Mathf.Clamp01(s);
            this.value = Mathf.Clamp01(v);
            alpha = 1f;
        }

        /// <summary>
        /// Color from Hue Saturation, Value and Alpha
        /// Hue between 0 ~ 360
        /// Saturation between 0 ~ 100
        /// Value between 0 ~ 100
        /// Alpha between 0 ~ 100
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        /// <param name="a">Alpha</param>
        public ColorHSV(int h, int s, int v, int a)
        {
            this.hue = Mathf.Repeat(h / 360f, 1f);
            this.saturation = Mathf.Clamp01(s / 100f);
            this.value = Mathf.Clamp01(v / 100f);
            this.alpha = Mathf.Clamp01(a / 100f);
        }

        /// <summary>
        /// Color from Hue Saturation and Value
        /// Hue between 0 ~ 360
        /// Saturation between 0 ~ 100
        /// Value between 0 ~ 100
        /// Alpha is by default 100
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        public ColorHSV(int h, int s, int v)
        {
            this.hue = Mathf.Repeat(h / 360f, 1f);
            this.saturation = Mathf.Clamp01(s / 100f);
            this.value = Mathf.Clamp01(v / 100f);
            alpha = 1f;
        }

        public ColorHSV(Color c)
        {
            Color.RGBToHSV(c, out hue, out saturation, out value);
            alpha = c.a;
        }

        public static implicit operator Color(ColorHSV c)
        {
            var col = Color.HSVToRGB(c.h, c.s, c.v);
            col.a = c.a;
            return col;
        }

        public static ColorHSV Lerp(ColorHSV c1, ColorHSV c2, float t)
        {
            float h, s, v;
            if (c1.h < c2.h)
            {
                if (c2.h - c1.h > (c1.h + 1) - c2.h)
                {
                    c1.h += 1;
                }
            }
            else
            {
                if (c1.h - c2.h > (c2.h + 1) - c1.h)
                {
                    c2.h += 1;
                }
            }
            h = Mathf.Repeat(Mathf.Lerp(c1.h, c2.h, t), 1f);
            s = Mathf.Lerp(c1.s, c2.s, t);
            v = Mathf.Lerp(c1.v, c2.v, t);

            return new ColorHSV(h, s, v);
        }

        public static ColorHSV Lerp(Color c1, Color c2, float t)
        {
            return Lerp(new ColorHSV(c1), new ColorHSV(c2), t);
        }
    }
}

