namespace GenericList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenericList<string> list = new GenericList<string>;
            list.Add("Hamza");
            list.Add("Eldeeb");
            list.Add("Bakry");

            list.Remove("Bakry");

        }
    }
}
