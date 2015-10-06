using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    public class BinaryFilter : IFilter
    {
        /// <summary>
        /// Конструктор BinaryFilter.
        /// </summary>
        /// <param name="Radius">Радиус окрестности пикселя, для вычисления порога бинаризации.</param>
        /// <param name="Const">Дельта смещения первичного порога бинаризации.</param>
        public BinaryFilter(int Radius, int Const)
        {
            this.Const = Const;
            this.Radius = Radius;
        }
        /// <summary>
        /// Конструктор BinaryFilter.
        /// </summary>
        public BinaryFilter()
        {
            this.Radius = 10;
            this.Const = -10;
        }
        /// <summary>
        /// Радиус окрестности пикселя, для вычисления порога бинаризации.
        /// </summary>
        public int Radius { get; set; }
        /// <summary>
        /// Дельта смещения первичного порога бинаризации.
        /// </summary>
        public int Const { get; set; }
        /// <summary>
        /// Производит адаптивную бинаризацию изображения.
        /// </summary>
        /// <param name="colors"> Исходный миассив пикселей.</param>
        /// <returns>Итоговый массив пикселей.</returns>
        public Color[][] Apply(Color[][] colors)
        {
            var w_brightness = new byte[colors.Length][];
            var w_colors = new Color[colors.Length][];

            for (int i = 0; i < w_colors.Length; i++)
            {
                w_colors[i] = new Color[colors[i].Length];
                w_brightness[i] = new byte[colors[i].Length];

                for (int j = 0; j < w_brightness[i].Length; j++)
                {
                    float tmp = colors[i][j].GetBrightness()*255;
                    w_brightness[i][j] = Convert.ToByte(colors[i][j].GetBrightness() > 1 ? 255 : colors[i][j].GetBrightness()*255);
                }
            }
            
            for (int x = 0; x < w_brightness.Length; x++)
            {
                for (int y = 0; y < w_brightness[x].Length; y++)
                {
                    byte min_bright = 255, max_bright = 0;
                    for (int i = (x - Radius) > 0 ? x - Radius : 0; i < ((x + Radius) < w_brightness.Length ? x + Radius : w_brightness.Length); i++)
                    {
                        for (int j = (y - Radius) > 0 ? y - Radius : 0; j < ((y + Radius) < w_brightness[i].Length ? y + Radius : w_brightness[i].Length); j++)
                        {
                            max_bright = max_bright < w_brightness[i][j] ? w_brightness[i][j] : max_bright;
                            min_bright = min_bright > w_brightness[i][j] ? w_brightness[i][j] : min_bright;
                        }
                    }
                    w_colors[x][y] = w_brightness[x][y] > (((max_bright + min_bright) / 2) + Const) ? Color.White : Color.Black;
                }
            }
            return w_colors;
        }
    }
}
