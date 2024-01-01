using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static FunctionalProgrammingCSharpBookExamples.FuncInEnumerables;

namespace FunctionalProgrammingCSharpBookExamples;

internal class PasswordValidation
{
    public void Run()
    {
        var password = "B3lz$xeml234";

        var imparativCheck = IsPasswordValidImperativ(password);
        var funcCheck = IsPasswordValidFunc(password);

        var checkWithExtention = password.IsValid(
            x => x.Length > 6,
            x => x.Length <= 20,
            x => x.Any(x => Char.IsLower(x)),
            x => x.Any(x => Char.IsUpper(x)),
            x => x.Any(x => Char.IsSymbol(x)),
            x => !x.Contains("Justin", StringComparison.OrdinalIgnoreCase) &&
                 !x.Contains("Bieber", StringComparison.OrdinalIgnoreCase)
            );

    }

    private bool IsPasswordValidImperativ(string password)
    {
        if (password.Length <= 6)
            return false;
        if (password.Length > 20)
            return false;
        if (!password.Any(x => Char.IsLower(x)))
            return false;
        if (!password.Any(x => Char.IsUpper(x)))
            return false;
        if (!password.Any(x => Char.IsSymbol(x)))
            return false;
        if (password.Contains("Justin", StringComparison.OrdinalIgnoreCase) &&
            password.Contains("Bieber", StringComparison.OrdinalIgnoreCase))
            return false;
        return true;
    }



    private bool IsPasswordValidFunc(string password) =>
        new Func<string, bool>[]
        {
            x => x.Length > 6,
            x => x.Length <= 20,
            x => x.Any(x => Char.IsLower(x)),
            x => x.Any(x => Char.IsUpper(x)),
            x => x.Any(x => Char.IsSymbol(x)),
            x => !x.Contains("Justin", StringComparison.OrdinalIgnoreCase) &&
                 !x.Contains("Bieber", StringComparison.OrdinalIgnoreCase)
        }.All(f => f(password));

}

internal static class PasswordValidationExtentions
{
    public static bool IsValid<T>(this T @this, params Func<T, bool>[] rules) =>
        rules.All(rule => rule(@this));
}
