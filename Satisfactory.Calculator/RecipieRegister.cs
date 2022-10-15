using FluentAssertions;

namespace Satisfactory.Calculator
{
    public class RecipieRegister
    {
        private Dictionary<string, Recipie> _recipies = new Dictionary<string, Recipie>();

        public IEnumerable<Recipie> GetByOutputItem(string itemCode)
        {
            return _recipies
                .Values
                .Where(r => r.Output.Any(i => i.ItemCode == itemCode));
        }

        public IEnumerable<Recipie> GetByInputItem(string itemCode)
        {
            return _recipies
                .Values
                .Where(r => r.Input.Any(i => i.ItemCode == itemCode));
        }

        internal Recipie GetByRecipie(string recipieCode)
        {
            return _recipies
                .Values
                .Single(r => r.Code == recipieCode);
        }

        internal void Add(Recipie recipie)
        {
            _recipies.Add(recipie.Code, recipie);
        }
    }
}