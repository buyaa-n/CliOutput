using FluentAssertions;
using CliOutput.Help;
using OutputEngine.Targets;

namespace CliOutput.Test;

public class HelpTests
{
    [Fact]
    public void Outputs_description()
    {
        var command = new CliCommand("Hello", "World");
        var help = HelpLayout.Create(command);
        var description = help.Sections.OfType<HelpDescription>().First();
        var writer = new PlainTerminal(true);

        writer.Write(description);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Description:{Environment.NewLine}  World");
    }

    [Fact]
    public void Outputs_usage()
    {
        var command = new CliCommand("Hello", "World");
        var help = HelpLayout.Create(command);
        var usage = help.Sections.OfType<HelpUsage>().First();
        var writer = new PlainTerminal(true);

        writer.Write(usage);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Usage:{Environment.NewLine}  Hello");
    }

    [Fact]
    public void Outputs_usage_with_subcommands()
    {
        var command = new CliCommand("Hello", "World");
        var subcommand1 = new CliCommand("Welcome", "Welcome to the beach!");
        var subcommand2 = new CliCommand("Brrr", "Let's go skiing!");
        command.AddSubCommand(subcommand1);
        command.AddSubCommand(subcommand2);
        var help = HelpLayout.Create(command);
        var usage = help.Sections.OfType<HelpUsage>().First();
        var writer = new PlainTerminal(true);

        writer.Write(usage);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Usage:{Environment.NewLine}  Hello [command]");
    }

    [Fact]
    public void Outputs_usage_with_parents()
    {
        var command = new CliCommand("Hello", "World");
        var subcommand1 = new CliCommand("Welcome", "Welcome to the beach!");
        var subcommand2 = new CliCommand("Brrr", "Let's go skiing!");
        command.AddSubCommand(subcommand1);
        subcommand1.AddSubCommand(subcommand2);
        var help = HelpLayout.Create(subcommand2);
        var usage = help.Sections.OfType<HelpUsage>().First();
        var writer = new PlainTerminal(true);

        writer.Write(usage);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Usage:{Environment.NewLine}  Hello Welcome Brrr");
    }

    [Fact]
    public void Outputs_usage_with_arguments()
    {
        var command = new CliCommand("Hello", "World");
        command.Arguments.Add(new CliArgument("Morning", "It is the morning."));
        command.Arguments.Add(new CliArgument("Evening", "Now, it is the evening"));
        var help = HelpLayout.Create(command);
        var usage = help.Sections.OfType<HelpUsage>().First();
        var writer = new PlainTerminal(true);

        writer.Write(usage);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Usage:{Environment.NewLine}  Hello <MORNING><EVENING>");
    }

    [Fact]
    public void Outputs_usage_with_options()
    {
        var command = new CliCommand("Hello", "World");
        command.Options.Add(new CliOption("Morning", "It is the morning."));
        command.Options.Add(new CliOption("Evening", "Now, it is the evening"));
        var help = HelpLayout.Create(command);
        var usage = help.Sections.OfType<HelpUsage>().First();
        var writer = new PlainTerminal(true);

        writer.Write(usage);

        var result = writer.GetBuffer();
        result.Should()
            .Be($"Usage:{Environment.NewLine}  Hello [options]");
    }

    [Fact]
    public void Outputs_arguments()
    {
        var command = new CliCommand("Hello", "World");
        command.Arguments.Add(new CliArgument("Morning", "It is the morning."));
        command.Arguments.Add(new CliArgument("VerEarlyEvening", "Now, it is the evening"));
        var help = HelpLayout.Create(command);
        var arguments = help.Sections.OfType<HelpArguments>().First();
        var writer = new PlainTerminal(true);

        writer.Write(arguments);

        var result = writer.GetBuffer();
        // TODO: More fully test this output, possibly via Approvals testing
        result.Should()
            .StartWith($"Arguments:{Environment.NewLine}  ");
    }

    [Fact]
    public void Outputs_arguments_with_long_descrption()
    {
        var command = new CliCommand("Hello", "World");
        command.Arguments.Add(new CliArgument("EarlyEvening", "Now, it is the evening, with a very, very, very, very, very,  very, very,  very, very,  very, very,  very, very,  very, very,  very, very,  very, very,  very, very, long description"));
        command.Arguments.Add(new CliArgument("Morning", "It is the morning."));
        var help = HelpLayout.Create(command);
        var arguments = help.Sections.OfType<HelpArguments>().First();
        var writer = new PlainTerminal(true);

        writer.Write(arguments);

        var result = writer.GetBuffer();
        // TODO: More fully test this output, possibly via Approvals testing
        result.Should()
            .StartWith($"Arguments:{Environment.NewLine}  ");
    }

    [Fact]
    public void Outputs_options()
    {
        var command = new CliCommand("Hello", "World");
        command.Options.Add(new CliOption("Morning", "It is the morning."));
        command.Options.Add(new CliOption("VerEarlyEvening", "Now, it is the evening"));
        var help = HelpLayout.Create(command);
        var arguments = help.Sections.OfType<HelpOptions>().First();
        var writer = new PlainTerminal(true);

        writer.Write(arguments);

        var result = writer.GetBuffer();
        // TODO: More fully test this output, possibly via Approvals testing
        result.Should()
            .StartWith($"Options:{Environment.NewLine}  ");
    }

    [Fact]
    public void Outputs_subcommands()
    {
        var command = new CliCommand("Hello", "World");
        command.AddSubCommand(new CliCommand("Morning", "It is the morning."));
        command.AddSubCommand(new CliCommand("VerEarlyEvening", "Now, it is the evening"));
        var help = HelpLayout.Create(command);
        var arguments = help.Sections.OfType<HelpSubcommands>().First();
        var writer = new PlainTerminal(true);

        writer.Write(arguments);

        var result = writer.GetBuffer();
        // TODO: More fully test this output, possibly via Approvals testing
        result.Should()
            .StartWith($"Subcommands:{Environment.NewLine}  ");
    }

    [Fact]
    public void Outputs_help()
    {
        var command = new CliCommand("Hello", "World");
        command.Arguments.Add(new CliArgument("Morning", "It is the morning."));
        command.Arguments.Add(new CliArgument("VeryEarlyEvening", "Now, it is the evening."));
        command.Options.Add(new CliOption("Red", "The color is read."));
        command.Options.Add(new CliOption("Blue", "The color is blue."));
        command.AddSubCommand(new CliCommand("One", "The first subcommand."));
        command.AddSubCommand(new CliCommand("Two", "The second subcommand"));
        var help = HelpLayout.Create(command);
        var writer = new PlainTerminal(true);

        writer.Write(help);

        var result = writer.GetBuffer();
        // TODO: More fully test this output, possibly via Approvals testing
        result.Should()
            .StartWith($"Description:{Environment.NewLine}  ");
    }
}