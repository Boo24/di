using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;
using TagsCloudVisualization.WordAnalyzer;

namespace ApplicationStart.UI
{
    public class ConsoleUi :IUI
    {
        private CloudCreator cloudCreator;
        private IReader reader;
        private ITextParser parser;
        private IImageSaver saver;
        public string[] Args { get; set; }
        private TagCloudVisualizer visualizer;
        public ConsoleUi(CloudCreator cloudCreator,IReader reader, ITextParser parser,TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
        }
        public void Run()
        {
            var options = new Options();
            CommandLine.Parser.Default.ParseArguments(Args, options);
            var text = reader.Read(options.InputFile);
            var words = parser.Parse(text);
            var useFilters = GetFiltersNames();
            var useConverters = GetConvertorsNames();
            cloudCreator.Create(words, options.MaxFontSize, options.MinFontSize, options.WordsCount, options.Font, useFilters, useConverters);
            var bitmap = visualizer.Vizualize(cloudCreator.RectanglesCloud, Color.AliceBlue);
            saver.Save(bitmap, options.OutputFile);
            Console.WriteLine($@"Image saved to {options.OutputFile}");
            Console.ReadKey();
        }

        private HashSet<string> GetFiltersNames()
        {
            var useFilters = new HashSet<string>();
            var filters = typeof(WordsAnalyzer).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IWordsFilter)));
            foreach (var filter in filters)
            {
                var filterName = ((IWordsFilter)Activator.CreateInstance(filter)).Name;
                if (GetAgr(filterName).Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    useFilters.Add(filterName);
            }
            return useFilters;
        }

        private HashSet<string> GetConvertorsNames()
        {
            var useConverters = new HashSet<string>();
            var converters = typeof(WordsAnalyzer).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IWordConverter)));
            foreach (var converter in converters)
            {
                var converterName = ((IWordConverter)Activator.CreateInstance(converter)).Name;
                if (GetAgr(converterName).Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    useConverters.Add(converterName);
            }
            return useConverters;
        }

        private string GetAgr(string arg)
        {
            Console.WriteLine($@"Use {arg}? (Y/N)");
            return Console.ReadLine();


        }
    }
}
