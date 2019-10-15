namespace SharedTest.src.Domain
{
    public class WordMother
    {
        public static string Random()
        {
            return MotherCreator.Random().Word();
        }
    }
}