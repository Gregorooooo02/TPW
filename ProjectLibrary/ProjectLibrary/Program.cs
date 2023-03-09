namespace ProjectLibrary
{
    public class Program
    {   
        public int Subtraction(int a, int b)
        {
            return a - b;
        }

        public static void Main()
        {
            Program prog = new Program();
            Console.WriteLine(prog.Subtraction(5, 4));
        }

    }
}