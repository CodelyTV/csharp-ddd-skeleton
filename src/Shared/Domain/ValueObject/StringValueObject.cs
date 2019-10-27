namespace CodelyTv.Shared.Domain.ValueObject
{
    using System.Collections.Generic;

    public class StringValueObject : ValueObject
    {
        public string Value { get; private set; }

        public StringValueObject(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}