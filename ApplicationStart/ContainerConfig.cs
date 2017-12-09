using System.Collections.Generic;
using System.IO;
using ApplicationStart.UI;
using Autofac;
using TagsCloudVisualization.WordAnalyzer;

namespace ApplicationStart
{
    public class ContainerConfig
    {
        public IContainer GetContainer(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(WordsAnalyzer).Assembly)
                .WithParameter(
                "excludedPartsOfSpeech",
                new List<PartsOfSpeech>()
                    {
                        PartsOfSpeech.Adjective,
                        PartsOfSpeech.Pronoun,
                        PartsOfSpeech.Interjection,
                        PartsOfSpeech.Particle,
                        PartsOfSpeech.Verb,
                        PartsOfSpeech.Preposition
                    })
                .WithParameter("badWords", File.ReadAllLines("BoringWords.txt"))
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterType<Gui>().AsSelf();
            builder.RegisterType<ConsoleUi>().AsSelf().WithProperty("Args", args);
            return builder.Build();
        }
    }
}
