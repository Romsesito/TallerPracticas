using Best_Practices.Models.ModelBuilder;

namespace Best_Practices.Models.ModelFactory
{
    public class FordExplorerFactory : CarFactory
    {
        public override Vehicle Create()
        {
            return new CarModelBuilder()
                .setModel("Explorer")
                .setColor("black")
                .Build();
        }
    }
}