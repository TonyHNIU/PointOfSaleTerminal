using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal.DiscountCalculations
{
    public class DiscountCard
    {
        private readonly List<DiscountPoint> points;
        private decimal total = 0.0M;

        public DiscountCard(int baseDiscount, IEnumerable<DiscountPoint> discountPoints)
        {
            Contract.Requires(baseDiscount >= 0);
            Contract.Requires(discountPoints.Zip(discountPoints.Skip(1), (a, b) => a.DiscountPrecents < b.DiscountPrecents && a.MinimumTotal < b.MinimumTotal).All(b => b),
                              "Discount points sequence should be monotonically increasing");

            points = (new[] { new DiscountPoint(0.0M, baseDiscount) }).Concat(discountPoints).Reverse().ToList();
        }

        public int DiscountPercents
        {
            get
            {
                return points.First(d => d.MinimumTotal <= total).DiscountPrecents;
            }
        }

        public void AddTotal(decimal sum)
        {
            Contract.Requires(sum >= 0.0M);

            total += sum;
        }
    }
}
