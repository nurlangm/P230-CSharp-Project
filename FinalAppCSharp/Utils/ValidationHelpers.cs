using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp.Utils
{
    public class ValidationHelpers
    {
        public static bool ValidateEmail(string email)
        {
            return email.Contains("@");
        }

        public static bool ValidatePass(string pass)
        {
            return pass.Length > 8 && pass.Any(s => { return char.IsUpper(s); }) &&
                   pass.Any(s => { return char.IsLower(s); });
        }
        public static bool ValidateName(string name) // TODO
        {
            return name.Length > 3;
        }
    }
}
