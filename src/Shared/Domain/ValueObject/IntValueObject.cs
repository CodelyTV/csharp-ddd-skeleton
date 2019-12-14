namespace CodelyTv.Shared.Domain.ValueObject
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class IntValueObject : ValueObject
    {
        public int Value { get; private set; }

        public IntValueObject(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString(NumberFormatInfo.InvariantInfo);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as IntValueObject;
            if (item == null) return false;

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value);
        }
    }
}