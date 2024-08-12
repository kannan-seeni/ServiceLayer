namespace TNCSC.Hulling.Repository.Helpers
{
    /// <summary>
    /// IDGeneration
    /// </summary>
    public static class IdGeneration
    {
        #region GetDistrictCode
        public static string GetDistrictCode(string sName)
        {
            switch (sName)
            {
                case "Thiruvallur":
                    return "101_001_";
                case "Chennai":
                    return "101_002_";
                case "Kancheepuram":
                    return "101_003_";
                case "Vellore":
                    return "101_004_";
                case "Tiruvannamalai":
                    return "101_005_";
                case "Viluppuram":
                    return "101_006_";
                case "Salem":
                    return "101_007_";
                case "Namakkal":
                    return "101_008_";
                case "Erode":
                    return "101_009_";
                case "The Nilgiris":
                    return "101_010_";
                case "Dindigul":
                    return "101_011_";
                case "Karur":
                    return "101_012_";
                case "Tiruchirappalli":
                    return "101_013_";
                case "Perambalur":
                    return "101_014_";
                case "Ariyalur":
                    return "101_015_";
                case "Cuddalore":
                    return "101_016_";
                case "Nagapattinam":
                    return "101_017_";
                case "Thiruvarur":
                    return "101_018_";
                case "Thanjavur":
                    return "101_019_";
                case "Pudukkottai":
                    return "101_020_";
                case "Sivaganga":
                    return "101_021_";
                case "Madurai":
                    return "101_022_";
                case "Theni":
                    return "101_023_";
                case "Virudhunagar":
                    return "101_024_";
                case "Ramanathapuram":
                    return "101_025_";
                case "Thoothukkudi":
                    return "101_026_";
                case "Tirunelveli":
                    return "101_027_";
                case "Kanniyakumari":
                    return "101_028_";
                case "Dharmapuri":
                    return "101_029_";
                case "Krishnagiri":
                    return "101_030_";
                case "Coimbatore":
                    return "101_031_";
                case "Tiruppur":
                    return "101_032_";
                default:
                    return "101_000_";

            }
        }

        #endregion


        public static string FormatMonthandYear(DateTime dateTime)
        {
            if (dateTime > DateTime.MinValue)
            {
                string format = "MMM-yy";

                string formattedMonth = dateTime.ToString(format);
                return formattedMonth;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
