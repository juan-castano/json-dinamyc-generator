using Minio;
using Minio.DataModel;

namespace Storage
{
    public class ObjectStorage
    {
        public static async Task Load(string[] args)
        {
            Console.WriteLine("ObjectStorageApp-minio");

            var minioClient = new Minio.MinioClient()
                            .WithEndpoint("")
                            .WithCredentials("", "")
                            .WithRegion("")
                            .WithSSL()
                            .Build();

            if(GetBucketName(args, out string bucketName))
            {
                try
                {
                    Console.WriteLine($"\nCreating bucket {bucketName}...");
                    var bucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minioClient.MakeBucketAsync(bucketArgs);
                    Console.WriteLine($"\nBucket created: {bucketName}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Caught exception when creating a bucket:");
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("\nGetting a list of your buckets...");
            var listResponse = await MyListBucketsAsync(minioClient);
            Console.WriteLine($"Number of buckets: {listResponse.Buckets.Count}");
            foreach(Bucket b in listResponse.Buckets)
            {
                Console.WriteLine($"Name: {b.Name} - UTC: {b.CreationDateDateTime} - CreatedAt: {b.CreationDate}");
            }
        }

        private static bool GetBucketName(string[] args, out string bucketName)
        {
            bool retval = false;
            bucketName = string.Empty;

            if(args.Length == 0)
            {
                Console.WriteLine(@"\nNo arguments specified. Will simply list your Amazon S3 buckets.\nIf you wish to create a bucket, supply a valid, globally unique bucket name.");
                bucketName = String.Empty;
                retval = false;
            }
            else if(args.Length == 1)
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