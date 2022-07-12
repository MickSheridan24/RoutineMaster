using System.Diagnostics.CodeAnalysis;

namespace RoutineMaster.Models.Entities
{
    public class Tag : BaseUserDependentEntity
    {
        public string Name { get; set; }

        public ICollection<ExpenseTag> ExpenseTags { get; set; } = new List<ExpenseTag>();
    }

    public class TagComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag? x, Tag? y)
        {
            return x.Id == y.Id || x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }
}