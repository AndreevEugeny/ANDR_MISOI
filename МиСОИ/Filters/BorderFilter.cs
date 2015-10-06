using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Filters
{
    public class BorderFilter : IFilter
    {
        /// <summary>
        /// Производит внешнее окунтирование исходного изображения.
        /// </summary>
        /// <param name="colors"> Исходный бинаризированный миассив пикселей.</param>
        /// <returns>Итоговый массив пикселей.</returns>
        public Color[][] Apply(Color[][] colors)
        {
            var w_colors = new Color[colors.Length][];

            for (int i = 0; i < colors.Length; i++)
            {
                w_colors[i] = new Color[colors[i].Length];
            }

            for (int i = 0; i < w_colors.Length-1; i++)
            {
                for (int j = 0; j < w_colors[i].Length-1; j++)
                {
                     w_colors[i][j] = ((colors[i][j].ToArgb().Equals(Color.Black.ToArgb()))&&(colors[ i > 0 ? i-1: i][j].ToArgb().Equals(Color.Black.ToArgb()))&&(colors[i < colors.Length-1 ? i + 1 : i][j].ToArgb().Equals(Color.Black.ToArgb())) && (colors[i][j > 0 ? j - 1 : j].ToArgb().Equals(Color.Black.ToArgb())) && (colors[i][j < colors[i].Length - 1 ? j + 1 : j].ToArgb().Equals(Color.Black.ToArgb()))) ? Color.White : colors[i][j];
                }
            }
            
            return w_colors;
        }
    }
}
