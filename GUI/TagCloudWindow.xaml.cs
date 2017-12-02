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
    /// <inheritdoc />
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
        public int MinFontSize { get; set; } = 12;
        public int MaxFontSize { get; set; } = 24;
        public int WordsCount { get; set; } = 150;
        private string fontName = "Arial";

        public TagCloudWindow(CloudCreater cloudCreater, IReader reader, ITextParser parser,
            TagCloudVizualizer visualizer, IImageSaver saver)
        {
            this.cloudCreater = cloudCreater;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
            InitializeComponent();
            InputText.Text = "Или вставить текст сюда...";
            DataContext = this;
            this.Show();
        }

        public TagCloudWindow() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                outFilename = openFileDialog.FileName;
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
            var openFileDialog = new OpenFileDialog {Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                inputFilename = openFileDialog.FileName;
        }

        private void FontSelector_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                fontName = fontDialog.Font.Name;
        }

        private void CreateCloud_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            cloudCreater.Clear();
            var text = GetText();
            var words = parser.Parse(text);
            cloudCreater.Create(words, MaxFontSize, MinFontSize, WordsCount, fontName);
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
            cloudImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            cloudImage.EndInit();
            canvas.Children.Add(cloudImage);
        }

        private string GetText() => inputFilename != null ? reader.Read(inputFilename) : InputText.Text;
    }
}