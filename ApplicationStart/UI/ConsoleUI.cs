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
        private WordsAnalyzer analyzer;
        public ConsoleUi(CloudCreator cloudCreator, IReader reader, ITextParser parser, WordsAnalyzer analyzer, TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
            this.analyzer = analyzer;
        }
        public void Run()
        {
            var options = new Options();
            CommandLine.Parser.Default.ParseArguments(Args, options);
            var useFilters = GetFiltersNames();
            var useConverters = GetConvertorsNames();
            var createCloud = reader.Read(options.InputFile)
                .Then(parser.Parse)
                .Then(r => analyzer.Analyze(r, options.WordsCount, useFilters, useConverters))
                .Then(r => cloudCreator.Create(r, options.MaxFontSize, options.MinFontSize, options.Font))
                .Then(rc => visualizer.Vizualize(rc, Color.AliceBlue))
                .Then(r => Result.Ok(saver.Save(r, options.OutputFile)))
                .OnFail(Console.WriteLine);
            if (createCloud.IsSuccess)
            {
                saver.Save(createCloud.Value, options.OutputFile);
                Console.WriteLine($@"Image saved to {options.OutputFile}");
            }
            Console.ReadKey();
        }

        private IEnumerable<FilterType> GetFiltersNames()
        {
            return Enum.GetValues(typeof(FilterType))
                .Cast<FilterType>()
                .Where(t => GetAgr(t.ToString()).Equals("Y", StringComparison.InvariantCultureIgnoreCase)).ToList();

        }
        private IEnumerable<WordsConverterType> GetConvertorsNames()
        {
            return Enum.GetValues(typeof(WordsConverterType))
                .Cast<WordsConverterType>()
                .Where(c => GetAgr(c.ToString()).Equals("Y", StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private string GetAgr(string arg)
        {
            Console.WriteLine($@"Use {arg}? (Y/N)");
            return Console.ReadLine();
        }
    }
}
