namespace AuthControl.Entities
{
    public class Plans
    {
        public static List<string> GetRobotsBaseOnPlan(string planName)
        {


            if (planName == "Completo")
            {
                return new List<string> {"1", "2", "3", "4", "5", "6", "7"};
            }else if (planName == "Inicial")
            {
                return new List<string> { "2", "3"};
            }
            else if (planName == "Intermediário")
            {
                return new List<string> {"4", "5", "6", "7"};
            }
            else
            {
                return new List<string> { "2", "3" };
            }

        }
    }
}
