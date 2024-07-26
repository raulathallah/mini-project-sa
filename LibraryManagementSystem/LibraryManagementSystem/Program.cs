// See https://aka.ms/new-console-template for more information

List<Book> tempList = new List<Book>();
Library collections = new Library();
string title, author;
int publication_year;
int action = 0;

while (action != 4)
{
    Console.WriteLine("--- Library Management System ---");
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. Remove Book");
    Console.WriteLine("3. Display Book");
    Console.WriteLine("4. Exit\n");
    Console.WriteLine("Action number: ");
    action = int.Parse(Console.ReadLine());
    Console.Clear();
    switch (action)
    {
        case 1:
            Console.WriteLine("--- Library Management System ---");
            Console.WriteLine("!!! Add Book !!!\n");
            Console.Write("Input book title: ");
            title = Console.ReadLine();
            Console.Write("Input book author: ");
            author = Console.ReadLine();
            Console.Write("Input publication year: ");
            publication_year = int.Parse(Console.ReadLine());
            Book buku = new Book(title, author, publication_year);
            collections.AddBook(buku);
            break;

        case 2:
            Console.WriteLine("--- Library Management System ---");
            Console.WriteLine("!!! Remove Book !!!\n");
            collections.DisplayBook();
            Console.Write("Input book title: ");
            title = Console.ReadLine();
            collections.RemoveBook(title);
            break;
        case 3:
            collections.DisplayBook();
            break;
    }
}

//collections.RemoveBook("Buku Terbaik Vol.1");