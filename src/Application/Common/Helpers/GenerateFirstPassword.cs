using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers;

public static class GenerateFirstPassword
{
  
    public static string FirstPassword(string s) {
        //return example @Username1234
        Random rnd = new Random();
        int num = rnd.Next(1000,9999);
        return "@" + s + num;
    }
}
