using System.Collections;

namespace CommonTestUtilities.InlineData
{
    public class CultureInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Yield é uma palavra que permite a sequência da execução mesmo tendo um return em seguida
            yield return new object[] { "en" };
            yield return new object[] { "pt-BR" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
