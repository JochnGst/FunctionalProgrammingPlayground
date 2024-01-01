using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingCSharpBookExamples
{
    internal class DoSomething_S35
    {
        private IList<string> c = new List<string>();
        public void Run()
        {
            int[] input = [75,22,36];
            var output = input
                .Select(x => DoSomethingOne(x))
                .Select(x => DoSomethingTwo(x))
                .Select(x => DoSomethingThree(x))
                .ToArray();

            var temp1 = input.Select(x => AddOne(x))
                .Select(x => MultiplyTwo(x))
                .Select(x => SubtractThree(x));

            var temp2 = input.Where((x,index) => index % 2 == 0)
                .Select((x,index) => $"{index} - {x}")
                .ToArray();
            foreach ( var x in temp2)
                Console.WriteLine(x);

            temp1.ToArray();

            foreach (var x in c)
                Console.WriteLine(x);

            // create a list of tuple with the 5x5 grid coordinates
            var coords = Enumerable.Range(1, 5)
                .SelectMany(x => Enumerable.Range(1, 5)
                    .Select(y => (X: x, Y: y)));
            var coordString = string.Join(", ", coords);
            Console.WriteLine(coordString);


            // count backwards with Repeat and select with index reference
            var coordsback = Enumerable.Repeat(5,5).Select((value, index) => value - index)
                .SelectMany(x => Enumerable.Repeat(5, 5).Select((value, index) => value - index)
                    .Select(y => (X: x, Y: y)));
            var coordsbackString = string.Join(", ", coordsback);
            Console.WriteLine(coordsbackString);

            // Same as above but with Range and Reverse instead of Repeat and Select(with index)
            var coords3 = Enumerable.Range(1,5).Reverse()
                .SelectMany(x => Enumerable.Range (1, 5).Reverse()
                    .Select(y => (X: x, Y: y)));
            var coordString3 = string.Join(", ", coords3);
            Console.WriteLine(coordString3);


        }



        public int DoSomethingOne(int x)
        {
            c.Add($"{DateTime.Now} - DoSomethingOne({x})");
            return x;
        }

        public int DoSomethingTwo(int x)
        {
            c.Add($"{DateTime.Now} - DoSomethingTwo({x})");
            return x;
        }
        public int DoSomethingThree(int x)
        {
            c.Add($"{DateTime.Now} - DoSomethingThree({x})");
            return x;
        }

        public int AddOne(int x)
        {
            
            c.Add($"{DateTime.Now} - DoSomethingOne({x+1})");
            return x+1;
        }

        public int MultiplyTwo(int x)
        {
            c.Add($"{DateTime.Now} - DoSomethingTwo({x*2})");
            return x*2;
        }
        public int SubtractThree(int x)
        {
            c.Add($"{DateTime.Now} - DoSomethingThree({x-3})");
            return x-3;
        }
    }
}
