namespace Utilities.Searcher
{
    public class DataSearcher
    {
        private readonly Dictionary<string, object> tree;
        private DataSearcher(Dictionary<string, object> tree)
        {
            this.tree = tree;
        }

        public static DataSearcher Load(Dictionary<string, object> tree)
        {
            return new DataSearcher(tree);
        }

        public void Find(string path)
        {
            var pathSections = path.Split(".").AsEnumerable();

            var enumerator = pathSections.GetEnumerator();

            Dictionary<string, object> copyTree = new(this.tree);

            while(enumerator.MoveNext())
            {
                var section = enumerator.Current;

                bool hasValue = copyTree.TryGetValue(section, out var item),
                    isSameTypeOf = hasValue && item.GetType().ToString().Equals(typeof(Dictionary<string, object>).ToString());
                if(isSameTypeOf)
                {
                    Console.WriteLine($"HasValue: {hasValue} - SameTypeOf: {isSameTypeOf}");
                    Console.WriteLine($"Key: {section} - Value: {item}");
                    copyTree = new Dictionary<string, object>((Dictionary<string, object>)item);
                }
                else
                {
                    Console.WriteLine($"No se puede recuperar la clave {section} del path {path}");
                }
            }

            Console.WriteLine(copyTree.Keys.ToString());
        }
    }
}
