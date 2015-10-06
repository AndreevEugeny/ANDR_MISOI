using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    public class GrayWorldFilter : IFilter
    {
        public Color[][] Apply(Color[][] colors)
        {
            var _colors = new Color[colors.Length][];

            var rAvrg = GetRAvrg(colors);
            var gAvrg = GetGAvrg(colors);
            var bAvrg = GetBAvrg(colors);
            var avrg = (rAvrg + gAvrg + bAvrg) / 3;

            for (var i = 0; i < colors.Length; i++)
            {
                _colors[i] = new Color[colors[0].Length];
                for (var j = 0; j < colors[0].Length; j++)
                {
                    var r = colors[i][j].R * avrg / rAvrg > 255 ? 255 : colors[i][j].R * avrg / rAvrg;
                    var g = colors[i][j].G * avrg / gAvrg > 255 ? 255 : colors[i][j].G * avrg / gAvrg;
                    var b = colors[i][j].B * avrg / bAvrg > 255 ? 255 : colors[i][j].B * avrg / bAvrg;
                    _colors[i][j] = Color.FromArgb((byte)r, (byte)g, (byte)b);
                }
            }

            return _colors;
        }

        private double GetRAvrg(Color[][] colors)
        {
            double result = 0;
            foreach (var x in colors)
            {
                foreach (var y in x)
                {
                    result += y.R;
                }
            }
            return result / (colors.Length * colors[0].Length);
        }

        private double GetGAvrg(Color[][] colors)
        {
            double result = 0;
            foreach (var x in colors)
            {
                foreach (var y in x)
                {
                    result += y.G;
                }
            }
            return result / (colors.Length * colors[0].Length);
        }

        private double GetBAvrg(Color[][] colors)
        {
            double result = 0;
            foreach (var x in colors)
            {
                foreach (var y in x)
                {
                    result += y.B;
                }
            }
            return result / (colors.Length * colors[0].Length);
        }
    }
}
