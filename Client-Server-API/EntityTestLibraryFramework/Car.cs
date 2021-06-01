namespace EntityTestLibraryFramework
{
    public class Car
    {
        public Car() { }
        public Car(string model, int num)
        {
            Model = model;
            SerialNumber = num;
        }
        public int Id { get; set; }
        public string Model { get; set; }
        public int SerialNumber { get; set; }
    }
}
