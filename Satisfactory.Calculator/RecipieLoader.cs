namespace Satisfactory.Calculator
{
    public static class RecipieLoader
    {
        public static RecipieRegister GetRegister()
        {
            var register = new RecipieRegister();

            // Raw Materials
            register.Add(Recipies.CopperOre);
            register.Add(Recipies.IronOre);
            register.Add(Recipies.RawQuartz);

            // Ingots
            register.Add(Recipies.CopperIngot);
            register.Add(Recipies.IronIngot);
            register.Add(Recipies.QuartzCrystal);

            // Other
            register.Add(Recipies.Wire);
            register.Add(Recipies.IronWire);
            register.Add(Recipies.IronPlate);
            register.Add(Recipies.Cable);
            register.Add(Recipies.IronRods);
            register.Add(Recipies.Screws);
            register.Add(Recipies.CastScrews);
            register.Add(Recipies.ReinforcedIronPlate);
            register.Add(Recipies.BoltedReinforcedIronPlate);
            register.Add(Recipies.CrystalOscillator);

            return register;
        }
    }
}