namespace Sogetec.Chassis.Utilities;

public static class LinqExtensions
{
    extension<T>(ICollection<T> a)
    {
        public bool IsIn(ICollection<T> b)
        {
            return b != null && a.All(b.Contains);
        }

        public bool NotIn(ICollection<T> b) => !a.IsIn(b);

    }

    extension<T>(T item)
    {
        public bool IsIn(ICollection<T> collection)
        {
            return collection != null && collection.Contains(item);
        }

        public bool NotIn(ICollection<T> collection) => !item.IsIn(collection);
    }
}
