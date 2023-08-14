namespace Utilities.Builders
{
    public class RequestListBuilder
    {
        private readonly List<object> request;

        private RequestListBuilder()
        {
            this.request = new List<object>();
        }

        public static RequestListBuilder Init() => new RequestListBuilder();

        public RequestListBuilder Add(object value)
        {
            this.request.Add(value);
            return this;
        }

        public RequestListBuilder Add(Func<RequestBuilder, Dictionary<string, object>> storage)
        {
            this.request.Add(storage(RequestBuilder.Init()));
            return this;
        }

        public List<object> Build() => this.request;
    }
}
