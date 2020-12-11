using System;
using System.Linq;
using System.Collections.Generic;

namespace day7
{
    public class ColoredBag : IEquatable<ColoredBag>
    {
        public readonly static ColoredBag NoBag = new ColoredBag(String.Empty, null);

        public string Color { get; }

        public (string color, int count)[] ContainableBags { get; }

        public ColoredBag(string color, (string bags, int count)[] containableBags = null)
        {
            this.Color = color?.ToUpperInvariant();
            this.ContainableBags = containableBags;
        }

        public override string ToString()
        {
            string ContainedToString((string color, int count) bag) => $"{bag.count} {bag.color}";
            string bags =
                this.ContainableBags.Count() == 0
                ? "NO BAGS"
                : this.ContainableBags.Select(ContainedToString).Aggregate((a, b) => $"{a}, {b}");
            return $"{this.Color} Bags contain {bags}";
        }

        public static bool operator ==(ColoredBag lhs, ColoredBag rhs)
        {
            if (Object.ReferenceEquals(lhs, null) ^ Object.ReferenceEquals(rhs, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
            {
                return true;
            }

            return lhs.Equals(rhs);
        }
        public static bool operator !=(ColoredBag lhs, ColoredBag rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => this.Equals(obj as ColoredBag);

        public override int GetHashCode() => HashCode.Combine(Color);

        public bool Equals(ColoredBag bag) => Object.ReferenceEquals(this, bag) || (!Object.ReferenceEquals(bag, null) && String.Equals(this.Color, bag.Color, StringComparison.InvariantCultureIgnoreCase));
    }
}