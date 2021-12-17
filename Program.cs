using Amplifier;
using System;

namespace OpenCL_Test
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var compiler = new OpenCLCompiler();
            Console.WriteLine("\nList Devices----");
            foreach (var item in compiler.Devices)
            {
                Console.WriteLine(item);
            }

            compiler.UseDevice(0);
            compiler.CompileKernel(typeof(SimpleKernels));

            Console.WriteLine("\nList Kernels----");
            foreach (var item in compiler.Kernels)
            {
                Console.WriteLine(item);
            }

            //Create variable x, y with sample data
            Array x = new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Array y = new float[9];
            //Get the execution engine
            var exec = compiler.GetExec();
            //Execute fill kernel method to fill the y with constant value 0.5
            exec.Fill(y, 0.5f);
            //Execute SAXPY kernel method
            exec.SAXPY(x, y, 2f);

            exec.AddData(x, x, x);
            //Print the result
            Console.WriteLine("\nResult----");
            for (int i = 0; i < y.Length; i++)
            {
                Console.Write(y.GetValue(i) + " ");
            }
        }

       
    }
}
