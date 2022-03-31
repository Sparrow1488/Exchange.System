namespace Exchange.System.Packages
{
    public class RequestBody<T>
    {
        public RequestBody(T content)
        {
            Content = content;
        }

        public T Content { get; private set; }
    }
}
