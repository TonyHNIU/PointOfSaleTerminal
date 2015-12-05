using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal.DiscountCalculations
{
    public struct DiscountPoint
    {
        public DiscountPoint(double minimumTotal, int discountPercents) : this()
        {
            MinimumTotal = minimumTotal;
            DiscountPrecents = discountPercents;
        }

        public double MinimumTotal { get; set; }

        public int DiscountPrecents { get; set; }
    }
}
