using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoXamarin
{
    public class Calculator 
    {
        static int counter = 0;
        static public int add()
        {
            return counter++;
        }
    }
}
