namespace Exchange.System.Packages
{
    public class Response<T> : Response
    {
        public Response(T content) =>
            Content = content;

        public T Content { get; set; }
    }

    public class Response
    {

    }
}
