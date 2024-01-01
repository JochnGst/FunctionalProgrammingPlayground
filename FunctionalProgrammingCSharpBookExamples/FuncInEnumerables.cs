using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingCSharpBookExamples;

internal class FuncInEnumerables
{
    private IEnumerable<Employee> employees = new[]
    {
        new Employee
        {
            FirstName = "Toni",
            MiddleName = ["Herbert", "Franz"],
            LastName = "Maier"
        },
        new Employee
        {
            FirstName = "Jürgen",
            MiddleName = ["Lukas", "Max"],
            LastName = "Schmitt"
        }
    };
    public void Run()
    {

        IEnumerable<Func<Employee, string>> descriptors =
        [
            x => $"First Name = {x.FirstName}",
            x => $"Middle Names = {string.Join(", ", x.MiddleName)}",
            x => $"Last Name = {x.LastName}",
            x => ""
        ];
        var outputs = employees.Select(emp =>
                string.Join(Environment.NewLine, descriptors.Select(func => func(emp)))
            );

        foreach (var output in outputs)
        {
            Console.WriteLine(output);
        }

        // Output:
        //
        // First Name = Toni
        // Middle Names = Herbert, Franz
        // Last Name = Maier
        //
        // First Name = Jürgen
        // Middle Names = Lukas, Max
        // Last Name = Schmitt
        //
    }

    internal class Employee
    {
        public string FirstName { get; set; }
        public IEnumerable<string> MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
