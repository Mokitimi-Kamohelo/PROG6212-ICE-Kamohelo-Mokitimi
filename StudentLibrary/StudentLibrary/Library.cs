namespace StudentLibrary
{
    public class Library
    {
        public StudentRecord? Student { get; private set; }

        public List<Book> Books { get; private set; }

        // Constructor
        public Library()
        {
            Books = new List<Book>();
        }

        // Register a student
        public void RegisterStudent(StudentRecord student)
        {
            ArgumentNullException.ThrowIfNull(student);

            if (string.IsNullOrWhiteSpace(student.StudentNumber))
            {
                throw new ArgumentException(
                    "Student number cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                throw new ArgumentException(
                    "Student name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(student.Course))
            {
                throw new ArgumentException(
                    "Course cannot be empty.");
            }

            Student = student;
        }

        // Add a book
        public void AddBook(Book book)
        {
            ArgumentNullException.ThrowIfNull(book);

            if (Books.Any(existing => existing.BookId == book.BookId))
            {
                throw new InvalidOperationException(
                    "A book with this ID already exists.");
            }

            Books.Add(book);
        }

        // Remove a book by its ID
        public bool RemoveBook(int bookId)
        {
            Book? book = SearchBook(bookId);

            if (book == null)
            {
                return false;
            }

            Books.Remove(book);
            return true;
        }

        // Search for a book by its ID
        public Book? SearchBook(int bookId)
        {
            return Books.FirstOrDefault(
                book => book.BookId == bookId);
        }

        // Display all books
        public void DisplayBooks()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("No books have been added.");
                return;
            }

            Console.WriteLine("\nBOOK COLLECTION");
            Console.WriteLine("------------------------------");

            foreach (Book book in Books)
            {
                Console.WriteLine(book);
            }
        }

        // Calculate the total borrowing fee
        public decimal CalculateTotalBorrowingFee()
        {
            decimal totalFee = 0m;

            foreach (Book book in Books)
            {
                if (!book.IsAvailable)
                {
                    totalFee += book.DailyFee;
                }
            }

            return totalFee;
        }

        // Display the borrowed books
        public void DisplayBorrowedBooks()
        {
            List<Book> borrowedBooks = Books
                .Where(book => !book.IsAvailable)
                .ToList();

            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No books are currently borrowed.");
                return;
            }

            Console.WriteLine("\nBorrowed Books");

            foreach (Book book in borrowedBooks)
            {
                Console.WriteLine(
                    $"{book.Title} R{book.DailyFee:F2}");
            }
        }
    }
}