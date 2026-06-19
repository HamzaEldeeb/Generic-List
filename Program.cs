namespace GenericList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("Hamza");
            list.Add("Eldeeb");
            list.Add("Bakry");

            list.Remove("Bakry");

        }
    }
}
