namespace Domain
{
    public class ErrorMessage
    {
        public const string GetBookByIdNotFound = "Book with that id was not found!";
        public const string GetBookAvailabilityNotFound = "Book with that id was not found!";
        public const string GetBookReadRateNotFound = "Book with that id was not found!";
        public const string GetBookReadRateNoData = "There is no sufficient data to calculate read rate for this book!";
        public const string GetMostBorrowedBooksRangeLarge = "TopRange is too big!";
        public const string GetUsersWithMostRentsRangeLarge = "TopRange is too big!";
        public const string GetOtherBooksNoData = "No Data for this book!";
        public const string GetUserRentsNoData = "No Data for this user!";
    }
}
