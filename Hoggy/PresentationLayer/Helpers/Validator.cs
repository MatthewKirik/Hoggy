using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Helpers
{
    public enum CheckType { Mail, Empty, LengthMoreThan }
    class Validator
    {
        public static string Check(CheckType checkType, string text, params int[] par)
        {
            string err = String.Empty;
            switch (checkType)
            {
                case CheckType.Mail:
                    Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = reg.Match(text);
                    if (!match.Success)
                        err = "Wrong mail!";
                    break;

                case CheckType.Empty:
                    if (text.Length == 0)
                        err = "Empty field!";
                    break;
                case CheckType.LengthMoreThan:
                    if (text.Length < par[0])
                        err = "To short! Length must be not less " + par[0] + "!";
                    break;
                default:
                    break;
            }
            return err;
        }

        public static bool EmptyStrings(params string[]par)
        {
            foreach (string err in par)
            {
                if (err.Length > 0)
                    return false;
            }
            return true;
        }


    }
}
