namespace ObjectStorageApp.Models
{
    public class RequestBuilder
    {
        private readonly Dictionary<string, object> request;

        private RequestBuilder()
        {
            this.request = new Dictionary<string, object>();
        }

        public static RequestBuilder Init() => new RequestBuilder();

        public RequestBuilder AddProperty(string attributeName, object value) {
            this.request.Add(attributeName, value);
            return this;
        }

        public RequestBuilder AddNestedProperty(string attributeName, Func<RequestBuilder, Dictionary<string, object>> storage) {
            this.request.Add(attributeName, storage(RequestBuilder.Init()));
            return this;
        }

        public RequestBuilder AddList(string attributeName, Func<RequestListBuilder, List<object>> storage) {
            this.request.Add(attributeName, storage(RequestListBuilder.Init()));
            return this;
        }

        public Dictionary<string, object> Build() => this.request;
    }

    public class RequestListBuilder
    {

        private readonly List<object> request;

        private RequestListBuilder() {
            this.request = new List<object>();
        }

        public static RequestListBuilder Init() => new RequestListBuilder();

        public RequestListBuilder Add(object value) {
            this.request.Add(value);
            return this;
        }

        public RequestListBuilder Add(Func<RequestBuilder, Dictionary<string, object>> storage) {
            this.request.Add(storage(RequestBuilder.Init()));
            return this;
        }

        public List<object> Build() => this.request;
    }
}