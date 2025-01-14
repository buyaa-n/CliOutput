﻿using OutputEngine;

namespace CliOutput.Help;

//// -s is --search
//// dotnet new blazorwasm -h -s auth
//// dotnet new blazorwasm -h --details pwa
//// Or instead of details, build decent URLs automatically from root. Build Website as well

public class HelpLayout : Layout
{
    public CliCommand Command { get; }

    private HelpLayout(CliCommand command, IEnumerable<HelpSection> sections)
        : base(sections)
    {
        Command = command;
    }

    public static HelpLayout Create(CliCommand command)
        => new HelpLayout(command,
            [
                new HelpDescription(command),
                new HelpUsage(command),
                new HelpExamples(command),
                new HelpArguments(command),
                new HelpOptions(command),
                new HelpSubcommands(command),
            ]);

}
//public Help(object source, HelpCommand helpCommand)
//{
//    Source = source;
//    RootHelpCommand = helpCommand;
//}

//public List<string> ParentCommands { get; } = new();

//public object Source { get; }
//public bool IncludeUsage { get; set; }
//public string? Notes { get; set; }
//}

//public abstract class HelpPart<T> : IEnumerable<T>
//    where T : HelpItem
//{
//    protected abstract T CreateItem(string name, string? description, params string[] aliases);

//    public HelpPart(string partName)
//    {
//        PartName = partName;
//    }

//    public string PartName { get; }

//    private readonly List<T> _items = new();
//    public IEnumerable<T> Items
//        => _items;

//    public T Add(string name, string? description, params string[] aliases)
//    {
//        var helpItem = CreateItem(name, description, aliases);
//        _items.Add(helpItem);
//        return helpItem;
//    }

//    public IEnumerator<T> GetEnumerator()
//    {
//        return ((IEnumerable<T>)_items).GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return ((IEnumerable)_items).GetEnumerator();
//    }
//}

//public class HelpOptions : HelpPart<HelpOption>
//{
//    public HelpOptions()
//       : base("Options")
//    { }

//    protected override HelpOption CreateItem(string name, string? description, params string[] aliases)
//        => new(name, description, aliases);
//}

//public class HelpCommands : HelpPart<HelpCommand>
//{
//    public HelpCommands()
//       : base("Commands")
//    { }

//    protected override HelpCommand CreateItem(string name, string? description, params string[] aliases)
//        => new(name, description, aliases);
//}

//public class HelpArguments : HelpPart<HelpArgument>
//{
//    public HelpArguments()
//        : base("Arguments")
//    { }

//    protected override HelpArgument CreateItem(string name, string? description, params string[] aliases)
//        => new(name, description);
//}

//public abstract class HelpItem
//{
//    public HelpItem(string name, string? description, params string[] aliases)
//    {
//        Name = name;
//        Description = description;
//        AddAliases(aliases);
//    }

//    private protected virtual string CreateDisplayName(string name)
//    {
//        return name;
//    }

//    private readonly List<string> _aliases = new();
//    public List<string> Aliases
//        => _aliases;

//    public string Name { get; set; }
//    public string DisplayName => CreateDisplayName(Name);

//    /// <summary>
//    /// The description that will be displayed in help. Supports Markdown.
//    /// </summary>
//    public string? Description { get; set; }

//    /// <summary>
//    /// Optional: Additional information on the item in Markdown. This is expected to be used
//    /// in the automatically created website, and possibly other uses.
//    /// </summary>
//    public string? Details { get; set; }

//    /// <summary>
//    /// If included, the footnote will appear below the help options and commands, 
//    /// and a ref number displayed in the item. If a footnote is repeated on multiple
//    /// items, it will reuse the same number.
//    /// </summary>
//    public HelpFootnote? Footnote { get; set; }

//    public Uri? Uri { get; set; }

//    public void AddAlias(string alias)
//        => _aliases.Add(alias);
//    public void AddAliases(params string[] aliases)
//        => _aliases.AddRange(aliases);
//}

//public class HelpOption : HelpItem
//{

//    public HelpOption(string name, string? description, params string[] aliases)
//         : base(name, description, aliases) { }

//    private readonly List<string> _choices = new();
//    public IEnumerable<string> Choices
//        => _choices;

//    public void AddChoice(string choice)
//         => _choices.Add(choice);
//    public void AddChoices(params string[] choices)
//        => _choices.AddRange(choices);


//}

//public class HelpArgument : HelpItem
//{

//    public HelpArgument(string name, string? description)
//         : base(name, description) { }

//    private readonly List<string> _choices = new();
//    public IEnumerable<string> Choices
//        => _choices;

//    public void AddChoice(string choice)
//         => _choices.Add(choice);

//    public void AddChoices(params string[] choices)
//        => _choices.AddRange(choices);

//    private protected override string CreateDisplayName(string name)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class HelpCommand : HelpItem
//{
//    public HelpCommand(string name, string? description, params string[] aliases)
//        : base(name, description, aliases) { }

//    public bool CanExecute { get; set; }
//    public IEnumerable<string>? ParentCommandNames { get; set; }

//    public HelpOptions Options { get; } = new();

//    public HelpArguments Arguments { get; } = new();

//    public HelpCommands SubCommands { get; } = new();

//}

//public class HelpFootnote
//{
//    public HelpFootnote(string text)
//        => Text = text;

//    public int Id { get; internal set; }

//    public string Text { get; }

//}
