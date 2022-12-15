using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using SEGP7;

// Author: Sebastian Amyotte
// A class that represents a single entry in the journal

public class JournalEntry
{
    public String entry;
    public String rating;
    
    public JournalEntry(String entry, String rating)
    {
        this.entry = entry;
        this.rating = rating;
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