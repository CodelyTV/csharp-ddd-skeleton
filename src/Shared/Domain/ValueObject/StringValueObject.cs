namespace Shared.Domain.ValueObject
{
    public class StringValueObject
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
    }
}