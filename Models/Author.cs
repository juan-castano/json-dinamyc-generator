using ObjectStorageApp.Attributes;

namespace ObjectStorageApp.Models
{
    public class Author
    {

        [IsEmpty(ErrorMessage = "Should not be null or empty")]
        public string Name {get;set;}
    }
}