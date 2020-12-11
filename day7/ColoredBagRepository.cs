using System;
using System.Collections.Generic;
using System.Linq;

namespace day7
{
    public class ColoredBagRepository
    {
        protected Dictionary<string, ColoredBag> datastore = new Dictionary<string, ColoredBag>();

        public void Store(ColoredBag bag)
        {
            if (bag == null)
            {
                bag = ColoredBag.NoBag;
            }
            this.datastore[bag.Color.ToUpperInvariant()] = bag;
        }

        public ColoredBag GetBag(string color)
        {
            if (datastore.TryGetValue(color?.ToUpperInvariant(), out ColoredBag bag))
            {
                return bag;
            }
            return ColoredBag.NoBag;
        }

        public string[] GetAllBagKeys() => this.datastore.Keys.ToArray();
    }
}