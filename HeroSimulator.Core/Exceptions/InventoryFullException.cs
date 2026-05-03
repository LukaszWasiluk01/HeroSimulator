using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSimulator.Core.Exceptions
{
    public class InventoryFullException : Exception
    {
        public InventoryFullException(string message) : base(message)
        {
        }
    }
}
