
namespace ConvertTagWF
{
    internal static class Program
    {

        

        [STAThread]
        static void Main(string[] args)
        {

            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());   
            


        }
    }
}