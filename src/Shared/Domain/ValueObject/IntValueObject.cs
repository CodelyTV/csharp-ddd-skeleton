namespace CodelyTv.Shared.Domain.ValueObject
{
    using System.Globalization;

    public class IntValueObject
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
    }
}