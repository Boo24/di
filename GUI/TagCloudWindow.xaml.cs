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
using TagsCloudVisualization.WordAnalyzer;
using Image = System.Windows.Controls.Image;

namespace GUI
{
    public partial class TagCloudWindow : Window
    {
        private IReader reader;
        private ITextParser parser;
        private IImageSaver saver;
        private Bitmap bitmap;
        private CloudCreator cloudCreator;
        private TagCloudVisualizer visualizer;
        private Settings settings = new Settings();
        private string inputFilename;
        private string outFilename;
        private WordsAnalyzer analyzer;

        public TagCloudWindow(CloudCreator cloudCreator, WordsAnalyzer analyzer, IReader reader, ITextParser parser,
            TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
            this.analyzer = analyzer;
            InitializeComponent();
            DataContext = this;
            this.Show();
        }

        public TagCloudWindow() => InitializeComponent();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = @"Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            outFilename = openFileDialog.FileName;
            saver.Save(bitmap, outFilename);
            ResultInformation.Text = $"Image saved to {outFilename}";
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                inputFilename = openFileDialog.FileName;
        }

        private void CreateCloud_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
            cloudCreator.Clear();
            GetText()
            .Then(parser.Parse)
            .Then(w => analyzer.Analyze(w, settings.WordsCount, settings.UseFilters, settings.UseConverters))
            .Then(w => cloudCreator.Create(w, settings.MaxFontSize, settings.MinFontSize, settings.FontName))
            .Then(rc => visualizer.Vizualize(rc, settings.BackgroundColor))
            .Then(PutCloudOnCanvas)
            .OnFail(r => ResultInformation.Text = r);
        }

        private Result<bool> PutCloudOnCanvas(Bitmap btm)
        {
            bitmap = btm;
            var hBitmap = bitmap.GetHbitmap();
            var cloudImage = new Image
            {
                Stretch = Stretch.Uniform,
                StretchDirection = StretchDirection.Both
            };
            cloudImage.BeginInit();
            cloudImage.Width = Canvas.ActualWidth;
            cloudImage.Height = Canvas.ActualHeight;
            cloudImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            cloudImage.EndInit();
            Canvas.Children.Add(cloudImage);
            return Result.Ok(true);
        }


        private Result<string> GetText() => inputFilename!=null ? reader.Read(inputFilename) : Result.Ok("");

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(this, settings);
            settingsWindow.Show();
        }
    }
}