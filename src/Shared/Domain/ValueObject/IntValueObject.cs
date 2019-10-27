namespace CodelyTv.Shared.Domain.ValueObject
{
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
    }
}