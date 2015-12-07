using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(int itemsCount, decimal discountRate);
    }
}
