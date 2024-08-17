namespace TNCSC.Hulling.Repository.Helpers
{
    public static class ResponseCode
    {
         

        //Login 10000-101000

        public const int LoginSucess = 10000;
        public const int LoginFailure = 10001;
        public const int LoginException = 10002;
        public const int InvalidUserToken = 10003;
         
        //User 11000-111000

        public const int UserCreatedSuccessfully = 11000;
        public const int UnableToCreateUser = 11001;
        public const int ExceptionOccursInUserCreation = 11002;

        public const int UserDetailsRetrivedSuccessfully = 11003;
        public const int NoUsersFound = 11004;
        public const int ExceptionOccursInGetUserDetails = 11005;

        public const int ActiveOrInactiveUserSuccessfully = 11006; 
        public const int ExceptionOccursActiveOrInactiveUser = 11007;

        //Godown 12000-121000

        public const int GodownAddedSuccessfully = 12000;
        public const int UnableToAddGodown = 12001;
        public const int ExceptionOccursInAddingGodown = 12003;

        public const int GodownDetailsRetrivedSuccessfully = 12003;
        public const int NoGodownsFound = 12004;
        public const int ExceptionOccursInGetGodownDetails = 12005;

        public const int GodownUpdatedSuccessfully = 12006;
        public const int UnableToUpdateGodown = 12007;
        public const int ExceptionOccursInUpdatingGodown = 12008;

        public const int ActiveOrInactiveGodownSuccessfully = 12009;
        public const int ExceptionOccursInActiveOrInactiveGodown = 12010;


        //Paddy 13000-131000

        public const int PaddyDetailsAddedSuccessfully = 13000;
        public const int UnableToAddPaddyDetails = 13001;
        public const int ExceptionOccursInAddingPaddyDetails = 13002;

        public const int PaddyDetailsRetrivedSuccessfully = 13003;
        public const int NoPaddyDetailsFound = 13004;
        public const int ExceptionOccursInGetPaddyDetails = 13005;

        public const int PaddyDetailsUpdatedSuccessfully = 13006;
        public const int UnableToUpdatePaddyDetails = 13007;
        public const int ExceptionOccursInUpdatingPaddyDetails = 13008;

        //Rice 14000-141000

        public const int RiceDetailsAddedSuccessfully = 14000;
        public const int UnableToAddRiceDetails = 14001;
        public const int ExceptionOccursInAddingRiceDetails = 14002;

        public const int RiceDetailsRetrivedSuccessfully = 14003;
        public const int NoRiceDetailsFound = 14004;
        public const int ExceptionOccursInGetRiceDetails = 14005;

        public const int RiceDetailsUpdatedSuccessfully = 14006;
        public const int UnableToUpdateRiceDetails = 14007;
        public const int ExceptionOccursInUpdatinRiceDetails = 14008;

        //Gunny 15000-151000

        public const int GunnyDetailsAddedSuccessfully = 15000;
        public const int UnableToAddGunnyDetails = 15001;
        public const int ExceptionOccursInAddingGunnyDetails = 15002;

        public const int GunnyDetailsRetrivedSuccessfully = 15003;
        public const int NoGunnyDetailsFound = 15004;
        public const int ExceptionOccursInGetGunnyDetails = 15005;

        public const int GunnyDetailsUpdatedSuccessfully = 15006;
        public const int UnableToUpdateGunnyDetails = 15007;
        public const int ExceptionOccursInUpdatingGunnyDetails = 15008;

        //FRK 16000-161000

        public const int FRKDetailsAddedSuccessfully = 16000;
        public const int UnableToAddFRKDetails = 16001;
        public const int ExceptionOccursInAddingFRKDetails = 16002;

        public const int FRKDetailsRetrivedSuccessfully = 16003;
        public const int NoFRKDetailsFound = 16004;
        public const int ExceptionOccursInGetFRKDetails = 16005;

        public const int FRKDetailsUpdatedSuccessfully = 16006;
        public const int UnableToUpdateFRKDetails = 16007;
        public const int ExceptionOccursInUpdatingFRKDetails = 16008;

        //Mill 17000-171000

        public const int MillDetailsAddedSuccessfully = 17000;
        public const int UnableToAddMillDetails = 17001;
        public const int ExceptionOccursInAddingMillDetails = 17002;
        public const int AlreadyExists = 17003;

        public const int MillDetailsRetrivedSuccessfully = 17004;
        public const int NoMillDetailsFound = 17005;
        public const int ExceptionOccursInGetMillDetails = 17006;

        public const int MillDetailsUpdatedSuccessfully = 17007;
        public const int UnableToUpdateMillDetails = 17008;
        public const int ExceptionOccursInUpdatingMillDetails = 17009;

        public const int ActiveOrInactiveMillSuccessfully = 17010; 
        public const int ExceptionOccursInActiveOrInactiveMill = 17011;

        //Mill 18000-181000
        public const int GCAddedOrUpdatedSuccessfully = 18000;
        public const int UnableToAddOrUpdateGC = 18001;
        public const int ExceptionOccursInAddingOrUpdatingGC = 18002;

        public const int VarietyAddOrUpdatedSuccessfully = 18003;
        public const int UnableToAddOrUpdateVariety = 18004;
        public const int ExceptionOccursInAddingOrUpdatingVariety = 18005;
         
        public const int GCsRetrivedSuccessfully = 18006;
        public const int NoGCsFound = 18007;
        public const int ExceptionOccursInGetGCs = 18008;

        public const int VarietyRetrivedSuccessfully = 18009;
        public const int NoVarietyFound = 18010;
        public const int ExceptionOccursInGetVariety = 18011;

        public const int RegionAddedOrUpdatedSuccessfully = 18012;
        public const int UnableToAddOrUpdateRegion = 18013;
        public const int ExceptionOccursInAddingOrUpdatingRegion = 18013;

        public const int RegionDetailsRetrivedSuccessfully = 18014;
        public const int NoRegionFound = 18015;
        public const int ExceptionOccursInGetRegionDetails = 18016;
    }
}
