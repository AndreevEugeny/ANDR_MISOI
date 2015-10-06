using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    public class MedianFilter : IFilter
    {
        public int Border { get; set; }

        public MedianFilter(int border)
        {
            Border = border;
        }

        public Color[][] Apply(Color[][] colors)
        {
            var _colors = new Color[colors.Length][];
            for (var i = 0; i < colors.Length; i++)
            {
                _colors[i] = new Color[colors[0].Length];
                for (var j = 0; j < colors[0].Length; j++)
                {
                    var list = new List<Color>();
                    for (var k = i - Border / 2; k <= i + Border / 2; k++)
                    {
                        for (var t = j - Border / 2; t <= j + Border / 2; t++)
                        {
                            var x = k;
                            var y = t;

                            if (x < 0)
                            {
                                x = 0;
                            }
                            else if (x > colors.Length - 1)
                            {
                                x = colors.Length - 1;
                            }
                            if (y < 0)
                            {
                                y = 0;
                            }
                            else if (y > colors[0].Length - 1)
                            {
                                y = colors[0].Length - 1;
                            }

                            list.Add(colors[x][y]);
                        }
                    }
                    _colors[i][j] = Transform(list);
                }
            }
            return _colors;
        }

        private Color Transform(List<Color> list)
        {
            list.Sort((x, y) => (x.R + x.G + x.B) - (y.R + y.G + y.B));
            return list.ElementAt(Border * Border / 2 + 1);
        }
    }
}
