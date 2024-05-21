namespace api.Helpers
{
    public static class PersonHelper
    {
        public static string SexStringByInt(int sexInt)
        {
            string sexString;

            switch (sexInt)
                {
                    case 0:
                        {
                            sexString = "Woman";
                            break;
                        }
                    case 1:
                        {
                            sexString = "Man";
                            break;
                        }
                    default:
                        {
                            throw new Exception("Sex is incorrect!");
                        }
                }

            return sexString;
        }
    }
}