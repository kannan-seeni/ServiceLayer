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
    }
}
