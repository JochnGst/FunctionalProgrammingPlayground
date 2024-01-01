using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingCSharpBookExamples
{
    internal class HigherOrderFunctions
    {

        public void Run()
        {
            var addTenFunction = MakeAddFunc(10);
            var answer = addTenFunction(5);
            Console.WriteLine(answer);
        }

        private Func<int, int> MakeAddFunc(int x) => y => x + y;
    }
}
