using System;
using System.Drawing;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public class RandomColorSelector : IFontColorSelector
    {
        private readonly Brush[] defaultPalette = {Brushes.CornflowerBlue, Brushes.DarkGoldenrod, Brushes.DarkKhaki, Brushes.Blue};

        private readonly Random rnd;
        public RandomColorSelector() => rnd = new Random();
        public Brush GetColorFor(Word word) => defaultPalette[rnd.Next(0, defaultPalette.Length - 1)];

    }
}
