using System;

public class Book
{
    private string title;
    private string author;
    private int publication_year;

    public Book()
    {
        this.title = "";
        this.author = "";
        this.publication_year = 0;
    }
    public Book(string title, string author, int publication_year)
    {
        this.title = title;
        this.author = author;
        this.publication_year = publication_year;
    }

    public string Title { get => title; set => title = value; }
    public string Author { get => author; set => author = value; }
    public int Publication_year { get => publication_year; set => publication_year = value; }
}