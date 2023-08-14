namespace Utilities.Builders
{
    public class RequestBuilder
    {
        private readonly Dictionary<string, object> request;

        private RequestBuilder()
        {
            this.request = new Dictionary<string, object>();
        }

        public static RequestBuilder Init() => new RequestBuilder();

        public RequestBuilder AddProperty(string attributeName, object value)
        {
            this.request.Add(attributeName, value);
            return this;
        }

        public RequestBuilder AddNestedProperty(string attributeName, Func<RequestBuilder, Dictionary<string, object>> storage)
        {
            this.request.Add(attributeName, storage(RequestBuilder.Init()));
            return this;
        }

        public RequestBuilder AddList(string attributeName, Func<RequestListBuilder, List<object>> storage)
        {
            this.request.Add(attributeName, storage(RequestListBuilder.Init()));
            return this;
        }

        public Dictionary<string, object> Build() => this.request;
    }
}
