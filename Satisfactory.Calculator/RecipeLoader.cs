namespace Satisfactory.Calculator
{
    public static class RecipeLoader
    {
        public static RecipeRegister GetRegister()
        {
            var register = new RecipeRegister();

            // Raw Materials
            register.Add(Recipes.CopperOre);
            register.Add(Recipes.IronOre);
            register.Add(Recipes.RawQuartz);

            // Ingots
            register.Add(Recipes.CopperIngot);
            register.Add(Recipes.IronIngot);
            register.Add(Recipes.QuartzCrystal);

            // Other
            register.Add(Recipes.Wire);
            register.Add(Recipes.IronWire);
            register.Add(Recipes.IronPlate);
            register.Add(Recipes.Cable);
            register.Add(Recipes.IronRods);
            register.Add(Recipes.Screws);
            register.Add(Recipes.CastScrews);
            register.Add(Recipes.ReinforcedIronPlate);
            register.Add(Recipes.BoltedReinforcedIronPlate);
            register.Add(Recipes.CrystalOscillator);

            return register;
        }
    }
}