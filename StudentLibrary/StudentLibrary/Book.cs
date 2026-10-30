namespace StudentLibrary
{
    public class Book
    {
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public BookCategory Category { get; private set; }
        public decimal DailyFee { get; private set; }
        public bool IsAvailable { get; private set; }

        // Constructor
        public Book(
            int bookId,
            string title,
            string author,
            BookCategory category,
            decimal dailyFee,
            bool isAvailable = true)
        {
            if (bookId <= 0)
            {
                throw new ArgumentException(
                    "Book ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(
                    "Book title cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException(
                    "Author name cannot be empty.");
            }

            if (!Enum.IsDefined(typeof(BookCategory), category))
            {
                throw new ArgumentException(
                    "Invalid book category.");
            }

            if (dailyFee < 0)
            {
                throw new ArgumentException(
                    "Daily fee cannot be negative.");
            }

            BookId = bookId;
            Title = title;
            Author = author;
            Category = category;
            DailyFee = dailyFee;
            IsAvailable = isAvailable;
        }

        // Update book availability
        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }

        // Display book information
        public override string ToString()
        {
            string availability = IsAvailable
                ? "Available"
                : "Borrowed";

            return $"ID: {BookId} | Title: {Title} | " +
                   $"Author: {Author} | Category: {Category} | " +
                   $"Daily Fee: R{DailyFee:F2} | " +
                   $"Status: {availability}";
        }

        // + Operator: Add the daily fees of two books
        public static decimal operator +(Book first, Book second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            return first.DailyFee + second.DailyFee;
        }

        // == Operator: Compare books by BookId
        public static bool operator ==(Book first, Book second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return first.BookId == second.BookId;
        }

        // != Operator: Compare books by BookId
        public static bool operator !=(Book first, Book second)
        {
            return !(first == second);
        }

        // > Operator: Compare daily fees
        public static bool operator >(Book first, Book second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            return first.DailyFee > second.DailyFee;
        }

        // < Operator: Compare daily fees
        public static bool operator <(Book first, Book second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            return first.DailyFee < second.DailyFee;
        }

        // Override Equals()
        public override bool Equals(object? obj)
        {
            return obj is Book other && BookId == other.BookId;
        }

        // Override GetHashCode()
        public override int GetHashCode()
        {
            return BookId.GetHashCode();
        }
    }
}