using Best_Practices.Models.ModelBuilder;

namespace Best_Practices.Models.ModelFactory
{
    public class FordEscapeFactory : CarFactory
    {

        public override Vehicle Create()
        {
            return new CarModelBuilder()
                .setModel("Escape")
                .setColor("red")
                .Build();
        }

    }
}