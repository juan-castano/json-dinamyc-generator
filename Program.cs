namespace ObjectStorageApp {
  using Minio;
  using Minio.DataModel;
  using Newtonsoft.Json;
    using ObjectStorageApp.Models;

    public class Program {

    public static async Task Main(string[] args)
    {
        var author = new Author
        {
            Name = "Juan"
        };
        Console.WriteLine(author.Name);

        var author1 = new Author();
        Console.WriteLine(author1.Name);

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

        var searcher = Search.Load(dictionary);
        searcher.Find("data.nestedAttributes");
    }
        // public static async Task Main(string[] args)
        // {
        //     Console.WriteLine("ObjectStorageApp-minio");

        //     var minioClient = new Minio.MinioClient()
        //                     .WithEndpoint("s3.bhs.io.cloud.ovh.net")
        //                     .WithCredentials("727745e688124f91be2f0ccbddae7de5", "bbbeb8bf36094beba600d8702a495920")
        //                     .WithRegion("bhs")
        //                     .WithSSL()
        //                     .Build();

        //     if (GetBucketName(args, out string bucketName))
        //     {
        //         try
        //         {
        //             Console.WriteLine($"\nCreating bucket {bucketName}...");
        //             var bucketArgs = new MakeBucketArgs().WithBucket(bucketName);
        //             await minioClient.MakeBucketAsync(bucketArgs);
        //             Console.WriteLine($"\nBucket created: {bucketName}");
        //         } catch (Exception ex)
        //         {
        //             Console.WriteLine("Caught exception when creating a bucket:");
        //             Console.WriteLine(ex.Message);
        //         }
        //     }

        //     Console.WriteLine("\nGetting a list of your buckets...");
        //     var listResponse = await MyListBucketsAsync(minioClient);
        //     Console.WriteLine($"Number of buckets: {listResponse.Buckets.Count}");
        //     foreach(Bucket b in listResponse.Buckets)
        //     {
        //         Console.WriteLine($"Name: {b.Name} - UTC: {b.CreationDateDateTime} - CreatedAt: {b.CreationDate}");
        //     }
        // }

        private static bool GetBucketName(string[] args, out string bucketName)
        {
            bool retval = false;
            bucketName = string.Empty;

            if (args.Length == 0)
            {
                Console.WriteLine(@"\nNo arguments specified. Will simply list your Amazon S3 buckets.\nIf you wish to create a bucket, supply a valid, globally unique bucket name.");
                bucketName = String.Empty;
                retval = false;
            }
            else if (args.Length == 1)
            {
                bucketName = args[0];
                retval = true;
            }
            else
            {
                Console.WriteLine("\nToo many arguments specified." +
                "\n\ndotnet_tutorials - A utility to list your Amazon S3 buckets and optionally create a new one." +
                "\n\nUsage: S3CreateAndList [bucket_name]" +
                "\n - bucket_name: A valid, globally unique bucket name." +
                "\n - If bucket_name isn't supplied, this utility simply lists your buckets.");
                Environment.Exit(1);
            }
            return retval;
        }

        private static async Task<ListAllMyBucketsResult> MyListBucketsAsync(MinioClient s3Client)
        {
            return await s3Client.ListBucketsAsync();
        }
    }
}