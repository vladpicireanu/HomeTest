namespace Domain
{
    public class ErrorCode
    {
        public const string GetBookByIdNotFound = "2000";
        public const string GetBookAvailabilityNotFound = "2001";
        public const string GetBookReadRateNotFound = "2002";
        public const string GetBookReadRateNoData = "2003";
        public const string GetMostBorrowedBooksRangeLarge = "2004";
        public const string GetUsersWithMostRentsRangeLarge = "2005"; 
        public const string GetOtherBooksNoData = "2006";
        public const string GetUserRentsNoData = "2007";
    }
}
