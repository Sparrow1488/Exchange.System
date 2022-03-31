namespace Exchange.System.Packages
{
    public class Body<T>
    {
        public Body(T content)
        {
            Content = content;
        }

        public T Content { get; private set; }
    }
}
