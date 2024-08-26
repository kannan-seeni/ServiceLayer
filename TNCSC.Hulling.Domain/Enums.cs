using System.Runtime.Serialization;

namespace TNCSC.Hulling.Domain
{
    /// <summary>
    /// Enums
    /// </summary>
    public class Enums
    {
        #region GodownType
        public enum GodownType
        {
            Paddy = 1,
            Rice = 2,
            Others = 3,
            All = 0
        }
        #endregion

        #region GunnyCondition
        public enum GunnyCondition
        {
            NB = 1,
            ONB = 2,
            SS = 3,
            SWP = 4,
            US = 5
        }
        #endregion

        #region Variety
        public enum Variety
        {
            ADT = 1,
            CR = 2
        }
        #endregion

        #region Grade
        public enum Grade
        {
            ADT36,
            ADT37,
            CR1006,
            CR1007
        }
        #endregion

        public enum Role : int
        {
            [EnumMember(Value = "Super Admin")]
            SuperAdmin = 1,
            [EnumMember(Value = "Admin")]
            Admin,
            [EnumMember(Value = "User")] 
            User,
            [EnumMember(Value = "Viewer")]
            Viewer
        }
    }
}
