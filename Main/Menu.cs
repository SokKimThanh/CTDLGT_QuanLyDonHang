using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Menu
{
    private int _id;
    private string _title;// chuc nang
    private string _description; // mo ta

    public Menu()
    {
        Random d = new Random();
        _id = d.Next();
        this._description = string.Empty;
        this._title = string.Empty;
    }

    // destructor
    ~Menu() { }
    public Menu(string title)
    {
        Random d = new Random();
        _id = d.Next();
        _title = title;
        _description = string.Empty;
    }

    public Menu(string title, string description)
    {
        Random d = new Random();
        _id = d.Next();
        _title = title;
        _description = description;
    }

    public string Title { get => _title; set => _title = value; }
    public string Description { get => _description; set => _description = value; }
    public int Id { get => _id; set => _id = value; }

    public string toString()
    {
        return $"{_title,-15}";
    }

}

