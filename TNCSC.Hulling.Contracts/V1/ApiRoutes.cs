namespace TNCSC.Hulling.Contracts.V1
{
    /// <summary>
    /// ApiRoutes
    /// </summary>
    public static class ApiRoutes
    {

        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        #region  Authenticate
        public static class Authenticate
        {

            public const string login = Base + "/Authentication/Login";

        }
        #endregion

        #region User
        public static class User
        {

            public const string createUser = Base + "/User/Create";

            public const string getAllUsers = Base + "/User";

            public const string getUserDetails = Base + "/User/{id}";

            public const string getUserByMillId = Base + "/User/mill/{millId}";

        }
        #endregion

        #region Godown
        public static class Godown
        {

            public const string addNewGodwon = Base + "/Godown/Add";

            public const string getAllGodwons = Base + "/Godown";

            public const string getGodwonDetails = Base + "/Godown/{id}";

            public const string getGodwonByMillId = Base + "/Godown/mill/{millId}/{typeId}";

            public const string updateGodwonDetails = Base + "/Godown/Update";


        }
        #endregion

        #region Paddy
        public static class Paddy
        {

            public const string addNewPaddy = Base + "/Paddy/Add";

            public const string getAllPaddyDetails = Base + "/Paddy";

            public const string getPaddyDetails = Base + "/Paddy/{id}";

            public const string getPaddyByMillId = Base + "/Paddy/mill/{millId}";

            public const string updatePaddyDetails = Base + "/Paddy/Update";

            public const string monthlyReport = Base + "/Paddy/{millId}/{month}";


        }
        #endregion

        #region Rice
        public static class Rice
        {

            public const string addNewRice = Base + "/Rice/Add";

            public const string getAllRiceDetails = Base + "/Rice";

            public const string getRiceDetails = Base + "/Rice/{id}";

            public const string getRiceByMillId = Base + "/Rice/mill/{millId}";

            public const string updateRiceDetails = Base + "/Rice/Update";

            public const string monthlyReport = Base + "/Rice/{millId}/{month}";


        }
        #endregion

        #region Gunnys
        public static class Gunnys
        {

            public const string addNewGunny = Base + "/Gunnys/Add";

            public const string getAllGunnyDetails = Base + "/Gunnys";

            public const string getGunnyDetails = Base + "/Gunnys/{id}";

            public const string getGunnyByMillId = Base + "/Gunnys/mill/{millId}";

            public const string updateGunnyDetails = Base + "/Gunnys/Update";

          //  public const string addGunnyCondition = Base + "/GunnysCondition/Add";

        }
        #endregion

        #region FRK
        public static class FRK
        {

            public const string addNewFRK = Base + "/FRK/Add";

            public const string getAllFRKDetails = Base + "/FRK";

            public const string getFRKDetails = Base + "/FRK/{id}";

            public const string getFRKByMillId = Base + "/FRK/mill/{millId}";

            public const string updateFRKDetails = Base + "/FRK/Update";


        }
        #endregion

        #region Mill
        public static class Mill
        {

            public const string addNewmill = Base + "/Mill/Add";

            public const string getAllMillDetails = Base + "/Mill";

            public const string getMillDetails = Base + "/Mill/{id}";
             
            public const string updateMillDetails = Base + "/Mill/Update";


        }
        #endregion

        #region MasterData
        public static class MasterData
        {

            public const string addGC = Base + "/MasterData/Add";

            public const string  gunnyCondition = Base + "/MasterData/GC/Add";

            public const string getAllGCDetails = Base + "/MasterData/GunnyCondition";

            public const string addVariety = Base + "/MasterData/Variety";

            public const string getAllVarietyDetails = Base + "/MasterData/Variety";


        }
        #endregion


    }
}
