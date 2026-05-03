using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroSimulator.Core.Exceptions
{
    public class NotEnoughEnergyException : Exception
    {
        public NotEnoughEnergyException(string message) : base(message)
        {
        }
    }
}
