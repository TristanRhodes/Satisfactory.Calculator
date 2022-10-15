using FluentAssertions;

namespace Satisfactory.Calculator
{
    public class RecipieRegister
    {
        private List<Recipie> _recipies = new List<Recipie>();

        public IEnumerable<Recipie> GetByOutputItem(string itemCode)
        {
            return _recipies
                .Where(r => r.Output.ItemCode == itemCode);   
        }

        public IEnumerable<Recipie> GetByInputItem(string itemCode)
        {
            return _recipies
                .Where(r => r.Input.Any(i => i.ItemCode == itemCode));
        }

        internal Recipie GetByRecipie(string recipieCode)
        {
            return _recipies
                .Single(r => r.Code == recipieCode);
        }

        internal void Add(Recipie recipie)
        {
            _recipies.Add(recipie);
        }
    }
}