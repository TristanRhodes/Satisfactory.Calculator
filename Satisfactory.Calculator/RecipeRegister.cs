namespace Satisfactory.Calculator
{
    public class RecipeRegister
    {
        private Dictionary<string, Recipe> _Recipes = new Dictionary<string, Recipe>();

        public IEnumerable<Recipe> GetByOutputItem(string itemCode)
        {
            return _Recipes
                .Values
                .Where(r => r.Output.Any(i => i.ItemCode == itemCode));
        }

        public IEnumerable<Recipe> GetByInputItem(string itemCode)
        {
            return _Recipes
                .Values
                .Where(r => r.Input.Any(i => i.ItemCode == itemCode));
        }

        public Recipe GetByRecipe(string RecipeCode)
        {
            return _Recipes
                .Values
                .Single(r => r.Code == RecipeCode);
        }

        public void Add(Recipe Recipe)
        {
            _Recipes.Add(Recipe.Code, Recipe);
        }
    }
}