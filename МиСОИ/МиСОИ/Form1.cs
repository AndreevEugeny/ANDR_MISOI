using Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace МиСОИ
{
    public partial class Form1 : Form
    {
        private IDictionary<string, IFilter> filters = new Dictionary<string, IFilter>()
        {
            {"Медианный", new MedianFilter(3)},
            {"Серый мир", new GrayWorldFilter()},
            {"Адаптивная бинаризация", new BinaryFilter() },
            {"Внешнее окунтирование", new BorderFilter()}
        };

        public Form1()
        {
            InitializeComponent();
            foreach (var filter in filters)
            {
                comboBox1.Items.Add(filter.Key);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var bitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filter = filters[comboBox1.SelectedItem.ToString()];
            pictureBox2.Image = TransformToBitmap(
                filter.Apply(
                TransformToColorArray((Bitmap)pictureBox1.Image)));
        }

        private Color[][] TransformToColorArray(Bitmap bitmap)
        {
            var colors = new Color[bitmap.Width][];
            for (int i = 0; i < bitmap.Width; i++)
            {
                colors[i] = new Color[bitmap.Height];
                for (int j = 0; j < bitmap.Height; j++)
                {
                    colors[i][j] = bitmap.GetPixel(i, j);
                }
            }
            return colors;
        }

        private Bitmap TransformToBitmap(Color[][] colors)
        {
            var bitmap = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 0; j < colors[0].Length; j++)
                {
                    bitmap.SetPixel(i, j, colors[i][j]);
                }
            }
            return bitmap;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }
    }
}
