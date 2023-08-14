namespace ObjectStorageApp
{
    using Newtonsoft.Json;
    using Utilities.Builders;
    using Utilities.Searcher;

    public class Program
    {

        public static Task Main(string[] args)
        {
            var dictionary = RequestBuilder
            .Init()
            .AddProperty("property1", false)
            .AddProperty("property2", 1543.4)
            .AddProperty("property3", 432)
            .AddNestedProperty("data", data =>
                data
                    .AddProperty("nested1", 123)
                    .AddProperty("nested2", "jkwef")
                    .AddNestedProperty("nestedAttributes", nestedAttribute =>
                        nestedAttribute
                            .AddProperty("data1", 3424234)
                            .Build()
                    )
                    .AddList("newList", newList =>
                        newList
                            .Add(123123)
                            .Add(x =>
                                x.AddProperty("my-info", "Juan")
                                .Build()
                            )
                            .Build()
                    )
                    .Build()
            )
            .Build();

            var list = RequestListBuilder
                .Init()
                .Add(312)
                .Add(articles =>
                    articles
                        .AddProperty("item", "book1")
                        .Build()
                )
                .Build();

            var serialized = JsonConvert.SerializeObject(dictionary);
            var serializedList = JsonConvert.SerializeObject(list);

            Console.WriteLine(serialized);
            Console.WriteLine(serializedList);

            var searcher = DataSearcher.Load(dictionary);
            searcher.Find("data.nestedAttributes");
            return Task.CompletedTask;
        }

        
    }
}