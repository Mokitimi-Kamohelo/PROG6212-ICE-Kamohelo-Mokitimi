using System;
using StudentLibrary;

namespace StudentLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            bool running = true;

            Console.WriteLine("   UNIVERSITY LIBRARY SYSTEM");

            while (running)
            {
                DisplayMenu();

                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RegisterStudent(library);
                            break;

                        case "2":
                            AddBook(library);
                            break;

                        case "3":
                            library.DisplayBooks();
                            break;

                        case "4":
                            SearchBook(library);
                            break;

                        case "5":
                            RemoveBook(library);
                            break;

                        case "6":
                            CalculateFees(library);
                            break;

                        case "7":
                            CompareBooks(library);
                            break;

                        case "0":
                            running = false;
                            Console.WriteLine(
                                "Thank you for using the library system.");
                            break;

                        default:
                            Console.WriteLine(
                                "Invalid option. Please try again.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Operation error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        // Displays the main menu
        static void DisplayMenu()
        {
            Console.WriteLine("\n========== MAIN MENU ==========");
            Console.WriteLine("1. Register Student");
            Console.WriteLine("2. Add Book");
            Console.WriteLine("3. Display Books");
            Console.WriteLine("4. Search Book");
            Console.WriteLine("5. Remove Book");
            Console.WriteLine("6. Calculate Total Borrowing Fee");
            Console.WriteLine("7. Compare Two Books");
            Console.WriteLine("0. Exit");
            Console.WriteLine("===============================");
        }

        // Register student details
        static void RegisterStudent(Library library)
        {
            Console.WriteLine("\nREGISTER STUDENT");

            Console.Write("Student Number: ");
            string studentNumber = ReadRequiredText();

            Console.Write("Full Name: ");
            string fullName = ReadRequiredText();

            Console.Write("Course: ");
            string course = ReadRequiredText();

            StudentRecord student = new StudentRecord(
                studentNumber,
                fullName,
                course);

            library.RegisterStudent(student);

            Console.WriteLine("Student registered successfully.");
        }

        // Add a new book
        static void AddBook(Library library)
        {
            Console.WriteLine("\nADD BOOK");

            int bookId = ReadPositiveInteger("Book ID: ");

            Console.Write("Title: ");
            string title = ReadRequiredText();

            Console.Write("Author: ");
            string author = ReadRequiredText();

            Console.WriteLine("\nBook Categories:");

            foreach (BookCategory category in Enum.GetValues<BookCategory>())
            {
                Console.WriteLine($"{(int)category}. {category}");
            }

            int categoryNumber = ReadInteger("Select category number: ");

            if (!Enum.IsDefined(typeof(BookCategory), categoryNumber))
            {
                throw new ArgumentException(
                    "Please select a valid book category.");
            }

            BookCategory selectedCategory =
                (BookCategory)categoryNumber;

            decimal dailyFee = ReadNonNegativeDecimal(
                "Daily borrowing fee (R): ");

            bool isAvailable = ReadYesNo(
                "Is the book available? (Y/N): ");

            Book book = new Book(
                bookId,
                title,
                author,
                selectedCategory,
                dailyFee,
                isAvailable);

            library.AddBook(book);

            Console.WriteLine("Book added successfully.");
        }

        // Search for a book
        static void SearchBook(Library library)
        {
            Console.WriteLine("\nSEARCH BOOK");

            int bookId = ReadPositiveInteger(
                "Enter the book ID to search for: ");

            Book? book = library.SearchBook(bookId);

            if (book != null)
            {
                Console.WriteLine("Book found:");
                Console.WriteLine(book);
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Remove a book
        static void RemoveBook(Library library)
        {
            Console.WriteLine("\nREMOVE BOOK");

            int bookId = ReadPositiveInteger(
                "Enter the book ID to remove: ");

            bool removed = library.RemoveBook(bookId);

            if (removed)
            {
                Console.WriteLine("Book removed successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // Calculate and display borrowing fees
        static void CalculateFees(Library library)
        {
            Console.WriteLine("\nBORROWING FEE SUMMARY");

            if (library.Student != null)
            {
                Console.WriteLine(
                    $"Student Number : {library.Student.StudentNumber}");

                Console.WriteLine(
                    $"Student Name : {library.Student.FullName}");

                Console.WriteLine(
                    $"Course : {library.Student.Course}");
            }
            else
            {
                Console.WriteLine("No student has been registered.");
            }

            library.DisplayBorrowedBooks();

            decimal totalFee =
                library.CalculateTotalBorrowingFee();

            Console.WriteLine(
                $"\nTotal Borrowing Fee : R{totalFee:F2}");
        }

        // Compare two books using overloaded operators
        static void CompareBooks(Library library)
        {
            Console.WriteLine("\nCOMPARE TWO BOOKS");

            int firstBookId = ReadPositiveInteger(
                "Enter the first book ID: ");

            int secondBookId = ReadPositiveInteger(
                "Enter the second book ID: ");

            Book? firstBook = library.SearchBook(firstBookId);
            Book? secondBook = library.SearchBook(secondBookId);

            if (firstBook == null || secondBook == null)
            {
                Console.WriteLine(
                    "Both books must exist before they can be compared.");

                return;
            }

            Console.WriteLine(
                $"\nBook 1: {firstBook.Title} (R{firstBook.DailyFee:F2})");

            Console.WriteLine(
                $"Book 2: {secondBook.Title} (R{secondBook.DailyFee:F2})");

            // Demonstrate == and !=
            Console.WriteLine(
                $"Book 1 == Book 2 : {firstBook == secondBook}");

            Console.WriteLine(
                $"Book 1 != Book 2 : {firstBook != secondBook}");

            // Demonstrate > and <
            Console.WriteLine(
                $"Book 1 > Book 2 : {firstBook > secondBook}");

            Console.WriteLine(
                $"Book 1 < Book 2 : {firstBook < secondBook}");

            // Demonstrate +
            decimal combinedFee = firstBook + secondBook;

            Console.WriteLine(
                $"Combined Daily Fee : R{combinedFee:F2}");

            // Demonstrate Equals() and GetHashCode()
            Console.WriteLine(
                $"Equals() : {firstBook.Equals(secondBook)}");

            Console.WriteLine(
                $"Book 1 Hash Code : {firstBook.GetHashCode()}");

            Console.WriteLine(
                $"Book 2 Hash Code : {secondBook.GetHashCode()}");
        }

        // Read required text
        static string ReadRequiredText()
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(
                    "This field cannot be empty.");
            }

            return input.Trim();
        }

        // Read an integer
        static int ReadInteger(string prompt)
        {
            Console.Write(prompt);

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                throw new ArgumentException(
                    "Please enter a valid whole number.");
            }

            return value;
        }

        // Read a positive integer
        static int ReadPositiveInteger(string prompt)
        {
            int value = ReadInteger(prompt);

            if (value <= 0)
            {
                throw new ArgumentException(
                    "The number must be greater than zero.");
            }

            return value;
        }

        // Read a non-negative decimal
        static decimal ReadNonNegativeDecimal(string prompt)
        {
            Console.Write(prompt);

            if (!decimal.TryParse(
                    Console.ReadLine(),
                    out decimal value) || value < 0)
            {
                throw new ArgumentException(
                    "Please enter a valid fee of zero or more.");
            }

            return value;
        }

        // Read a yes/no answer
        static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                string? answer = Console.ReadLine();

                if (string.Equals(
                    answer?.Trim(), "Y",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (string.Equals(
                    answer?.Trim(), "N",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Console.WriteLine(
                    "Please enter Y for Yes or N for No.");
            }
        }
    }
}