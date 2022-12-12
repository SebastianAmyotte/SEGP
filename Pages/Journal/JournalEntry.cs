using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using SEGP7;

public class JournalEntry
{
    String entry;
    String rating;
    DateTime date;

    public JournalEntry(String entry, String rating, DateTime date)
    {
        this.entry = entry;
        this.rating = rating;
        this.date = date;
    }

    public String GetEntry()
    {
        return entry;
    }

    public void SaveEntry(String entry)
    {
        this.entry = entry;
    }
    public String GetRating()
    {
        return rating;
    }
    public void SaveRating(String rating)
    {
        this.rating = rating;
    }

    
}