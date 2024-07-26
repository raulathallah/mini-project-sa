using System;

public class Library
{
    private Book[] book_collections;
    public Library()
    {
        this.book_collections = [];
    }

    public Library(Book[] book_collections)
    {
        this.book_collections = book_collections;
    }

    public Book[] Book_collections { get => book_collections; set => book_collections = value; }


    public void AddBook(Book book)
    {
        List<Book> list = new List<Book>(book_collections);
        if (list.Find(x => x.Title == book.Title) != null)
        {
            Console.WriteLine("\n...Book already in the collections!\n");
        }
        else
        {
            list.Add(book);
            this.book_collections = list.ToArray();
            Console.Clear();
            Console.WriteLine("...Book added!\n");
        }

    }

    public void RemoveBook(string Title)
    {
        List<Book> list = new List<Book>(book_collections);
        int idx_list = list.FindIndex(x => x.Title == Title);
        if (idx_list == -1)
        {
            Console.WriteLine("\n...No book available!\n");
        }
        else
        {
            list.RemoveAt(idx_list);
            this.book_collections = list.ToArray();
            Console.WriteLine("\n...Book removed!\n");
        }
    }

    public void DisplayBook()
    {

        if(book_collections.Length == 0)
        {
            Console.WriteLine("...Bookshelf is empty!");
        }
        else
        {
            Console.WriteLine("!!! Bookshelf !!!");
            Console.WriteLine("=================\n");
            foreach (Book book in this.book_collections)
            {
                Console.WriteLine("Title: {0}", book.Title);
                Console.WriteLine("Author: {0}", book.Author);
                Console.WriteLine("Publication Year: {0}", book.Publication_year);
                Console.WriteLine("\n");
            }
            Console.WriteLine("=================");
        }
        

    }
}