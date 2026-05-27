namespace Sogetec.Chassis.Extensions;

public static class SortOrderChain
{
    public static void PlaceInChain<T>(T item, IEnumerable<T> siblings, int? targetOrder = null) where T : class, ISortableEntity
    {
        var chain = siblings
            .Where(s => s is not null)
            .Distinct()
            .OrderBy(s => s.SortOrder)
            .ToList();

        chain.Remove(item);

        var order = targetOrder ?? chain.Count + 1;
        var index = Math.Clamp(order - 1, 0, chain.Count);

        chain.Insert(index, item);
        AssignSequentialOrder(chain);
    }

    public static void Normalize<T>(IEnumerable<T> siblings) where T : ISortableEntity
    {
        AssignSequentialOrder(siblings.OrderBy(s => s.SortOrder));
    }

    private static void AssignSequentialOrder<T>(IEnumerable<T> siblings) where T : ISortableEntity
    {
        var index = 1;
        foreach (var sibling in siblings)
        {
            sibling.SortOrder = index++;
        }
    }
}
