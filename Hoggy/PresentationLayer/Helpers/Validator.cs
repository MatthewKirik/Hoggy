using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Helpers
{
    public enum CheckType { Mail, Empty, LengthMoreThan, DateLessThan }
    
    class Validator
    {
        public static string Check<T>(CheckType checkType, object text, params T[] par)
        {
            string err = String.Empty;
            switch (checkType)
            {
                case CheckType.Mail:
                    Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = reg.Match((text as string));
                    if (!match.Success)
                        err = "Wrong mail!";
                    break;

                case CheckType.Empty:
                    if ((text as string).Length == 0)
                        err = "Empty field!";
                    break;
                case CheckType.LengthMoreThan:
                    if ((text as string).Length < Convert.ToInt32(par[0]))
                        err = "To short! Length must be not less " + par[0] + "!";
                    break;
                case CheckType.DateLessThan:
                    DateTime inputDate;
                    DateTime.TryParse(text.ToString(), out inputDate);
                    DateTime? checkDate = (par[0] as DateTime?);

                    if (inputDate <= checkDate)
                        err = "Expire date can't be less than current!";
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
