using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGridPage;

public class MainModel : ViewModel
{
    public MainModel()
    {
        _persons = GenerateData().ToList();
        _count = _persons.Count;
        HomeCommand.Execute(null);
    }

    private readonly List<Person> _persons;

    private int _pageSize = 50;
    private int _count;


    private int _index = 1;

    public int Index
    {
        get => _index;
        set => SetField(ref _index, value);
    }

    private ObservableCollection<Person> _items;

    public ObservableCollection<Person> Items
    {
        get => _items;
        private set => SetField(ref _items, value);
    }

    public Command HomeCommand => GetCommand(() =>
    {
        Index = 1;

        Items = new ObservableCollection<Person>(_persons.Take(_pageSize));
    });

    public Command EndCommand => GetCommand(() =>
    {
        Index = _count / _pageSize;
        var skip = (Index - 1) * _pageSize;
        var take = _count - skip;
        Items = new(_persons.Skip(skip).Take(take));
    });

    public Command PreviousCommand => GetCommand(() =>
    {
        switch (Index)
        {
            case 1:
                return;
            case 2:
                Items = new(_persons.Take(_pageSize));
                break;
            default:
                Items = new(_persons.Skip((Index - 2) * _pageSize).Take(_pageSize));
                break;
        }

        Index--;
    });

    public Command NextCommand => GetCommand(() =>
    {
        if (Index == _count / _pageSize)
        {
            return;
        }

        Items = new(_persons.Skip(Index * _pageSize).Take(_pageSize));
        Index++;
    });

    private IEnumerable<Person> GenerateData()
    {
        for (var i = 0; i < 1000; i++)
        {
            var person = new Person
            {
                Id = i + 1,
                Star = i % 60 == 0 ? i + i / 70 : i % 60 + new Random(20).Next(10),
                Job = $"W{i + 3}ha{i - 2}tev{i - 23}er",
                Name = GenerateName(i % 15)
            };
            yield return person;
        }
    }

    private static string GenerateName(int len)
    {
        var r = new Random();
        string[] consonants =
        {
            "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w",
            "x"
        };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        var name = "";
        name += consonants[r.Next(consonants.Length)].ToUpper();
        name += vowels[r.Next(vowels.Length)];
        //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        var b = 5;
        while (b < len)
        {
            name += consonants[r.Next(consonants.Length)];
            b++;
            name += vowels[r.Next(vowels.Length)];
            b++;
        }

        return name;
    }
}