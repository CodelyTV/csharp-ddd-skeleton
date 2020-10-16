using System;
using System.Collections.Generic;

namespace CodelyTv.Shared.Domain.ValueObject
{
    public class StringValueObject : ValueObject
    {
        public string Value { get; }

        public StringValueObject(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as StringValueObject;
            if (item == null) return false;

            return Value == item.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
