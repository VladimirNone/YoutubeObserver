using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTelegramBot.Infrastructure
{
    public class ControllerHelper
    {
        public static string[] ParseTelegramInput(string text)
        {
            var res = new string[2];

            if (text.StartsWith('/'))
            {
                var splittedText = text.Split(' ', 2);

                if(splittedText.Length == 1)
                {
                    res[0] = splittedText[0];
                }
                else if (splittedText.Length == 2)
                {
                    res = splittedText;
                }
            }
            else
            {
                res[0] = text;
            }

            return res;
        }
    }
}
