using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;
using Color = System.Drawing.Color;
using Image = System.Windows.Controls.Image;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для TagCloudWindow.xaml
    /// </summary>
    public partial class TagCloudWindow : Window
    {
        private Color backgroundColor = Color.AliceBlue;
        private IReader reader;
        private ITextParser parser;
        private IImageSaver saver;
        private Bitmap bitmap;
        private CloudCreater cloudCreater;
        private TagCloudVizualizer visualizer;
        private string inputFilename;
        private string outFilename;
        private int minFontSize = 10;
        private int maxFontSize = 25;
        public int wordsCount = 150;
        private string fontName = "Arial";

        public TagCloudWindow(CloudCreater cloudCreater, IReader reader, ITextParser parser, TagCloudVizualizer visualizer, IImageSaver saver)
        {
            this.cloudCreater = cloudCreater;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
            InitializeComponent();
            InputText.Tag = "Или вставить текст сюда...";
            InputText.Text = (string)InputText.Tag;
            MinFontSize.Text = "12";
            MaxFontSize.Text = "24";
            this.Show();
        }

        public TagCloudWindow() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog1 = new SaveFileDialog
            {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
            };
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                outFilename = openFileDialog1.FileName;
            saver.Save(bitmap, outFilename);
        }

        private void BackgroundColorSelect_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                backgroundColor = colorDialog.Color;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                inputFilename = openFileDialog1.FileName;
        }

        private void FontSelector_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog1 = new FontDialog();
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fontName = fontDialog1.Font.Name;
        }

        private void CreateCloud_Click(object sender, RoutedEventArgs e)
        {
            GetParams();
            var text = GetText();
            var words = parser.Parse(text);
            cloudCreater.Create(words, maxFontSize, minFontSize, wordsCount, fontName);
            bitmap = visualizer.Vizualize(cloudCreater.RectanglesCloud, backgroundColor);
            var hBitmap = bitmap.GetHbitmap();
            var cloudImage = new Image
            {
                Stretch = Stretch.Uniform,
                StretchDirection = StretchDirection.Both
            };
            cloudImage.BeginInit();
            cloudImage.Width = canvas.ActualWidth;
            cloudImage.Height = canvas.ActualHeight;
            cloudImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            cloudImage.EndInit();
            canvas.Children.Add(cloudImage);

        }
        private string GetText() => inputFilename != null ? reader.Read(inputFilename) : InputText.Text;

        private void GetParams()
        {
            minFontSize = int.Parse(MinFontSize.Text);
            maxFontSize = int.Parse(MaxFontSize.Text);
            wordsCount = int.Parse(WordsCount.Text);
        }
    }
}
