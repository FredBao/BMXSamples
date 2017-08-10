namespace UnitTestDemo
{
    using System.Collections.Generic;
    using System.Linq;

    public class ProductCollection
    {
        public ProductCollection(IDistribute distribute)
        {
            this._distribute = distribute;
        }

        public List<Product> Products { get; set; }

        private IDistribute _distribute { get; set; }

        public List<Product> DistributeProduct(List<int> pIds)
        {
            if (pIds == null || pIds.Count == 0)
            {
                return new List<Product>();
            }

            var result = from item in this.Products where pIds.Contains(item.PId) select item;
            this._distribute.ToNotice(string.Empty);
            return result.ToList();
        }
    }
}