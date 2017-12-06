using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using TagsCloudVisualization;
using TagsCloudVisualization.WordAnalyzer;


namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Settings settings;
        private Dictionary<string, FilterType> filtersTypes = new Dictionary<string, FilterType>();
        private Dictionary<string, WordsConverterType> converterTypes = new Dictionary<string, WordsConverterType>();
        public SettingsWindow(Window main, Settings settings)
        {
            this.settings = settings;
            DataContext = settings;
            InitializeComponent();
            CreateFiltersCheckBox();
            CreateConvertersCheckBox();
            FontExample.FontFamily = new FontFamily(settings.FontName);
            FontExample.Text = settings.FontName;
            //  Closing += (sender, args) => main.Show();
        }

        private void FontSelector_Click(object sender, RoutedEventArgs e)
        {
            var fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                settings.FontName = fontDialog.Font.Name;
            FontExample.FontFamily = new FontFamily(settings.FontName);
            FontExample.Text = settings.FontName;
        }

        private void BackgroundColorSelect_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                settings.BackgroundColor = colorDialog.Color;
        }

        private void CreateFiltersCheckBox()
        {
            filtersTypes = Enum.GetValues(typeof(FilterType))
                .Cast<FilterType>()
                .ToDictionary(x => x.ToString(), y => y);
            foreach (var filter in filtersTypes)
            {
                var filterName = filter.Key;
                var cb = new System.Windows.Controls.CheckBox() { Content = filterName };
                cb.Checked += FiltersCheckBox_Checked;
                cb.Unchecked += FiltersCheckBox_Unchecked;
                FiltersPanel.Children.Add(cb);
                if (settings.UseFilters.Contains(filter.Value))
                    cb.IsChecked = true;

            }
        }
        private void CreateConvertersCheckBox()
        {
            converterTypes = Enum.GetValues(typeof(WordsConverterType))
                .Cast<WordsConverterType>()
                .ToDictionary(x => x.ToString(), y => y);
            foreach (var converter in converterTypes)
            {
                var converterName = converter.Key;
                var cb = new System.Windows.Controls.CheckBox() { Content = converterName };
                cb.Checked += ConvertersCheckBox_Checked;
                cb.Unchecked += ConvertersCheckBox_Unchecked;
                ConvertersPanel.Children.Add(cb);
                if (settings.UseConverters.Contains(converter.Value))
                    cb.IsChecked = true;

            }
        }
        private void ConvertersCheckBox_Unchecked(object sender, RoutedEventArgs e) =>
            settings.UseConverters.Remove(converterTypes[((System.Windows.Controls.CheckBox)sender).Content.ToString()]);
        private void ConvertersCheckBox_Checked(object sender, RoutedEventArgs e) =>
            settings.UseConverters.Add(converterTypes[((System.Windows.Controls.CheckBox)sender).Content.ToString()]);

        private void FiltersCheckBox_Unchecked(object sender, RoutedEventArgs e) =>
            settings.UseFilters.Remove(filtersTypes[((System.Windows.Controls.CheckBox)sender).Content.ToString()]);

        private void FiltersCheckBox_Checked(object sender, RoutedEventArgs e) =>
            settings.UseFilters.Add(filtersTypes[((System.Windows.Controls.CheckBox)sender).Content.ToString()]);

        private void Save_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
