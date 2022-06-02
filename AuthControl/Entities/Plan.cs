using Microsoft.EntityFrameworkCore;

namespace AuthControl.Entities
{
    public class Plans
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<string> Robots { get; set; }

        public static List<string> GetRobotsBaseOnPlan(string planName, ApplicationContext context)
        {

            var plans = context.Set<Plans>().FirstOrDefault(x => x.Name == planName);

            return plans.Robots;

        }
    }
}
