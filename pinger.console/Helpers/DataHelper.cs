using pinger.console.Models;

namespace pinger.console.Helpers;

public static class DataHelper
{
    public static CommandLineOptions ParseCommandLineArgs(string[] args)
    {
        var options = new CommandLineOptions();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "--hosts":
                case "-h":
                    if (i + 1 < args.Length)
                    {
                        options.Hosts = args[++i].Split(',')
                            .Select(h => h.Trim())
                            .Where(h => !string.IsNullOrEmpty(h))
                            .ToList();
                    }
                    else
                    {
                        Console.WriteLine("Error: --hosts requires a value");
                        return null;
                    }

                    break;

                case "--port":
                case "-p":
                    if (i + 1 < args.Length)
                    {
                        if (int.TryParse(args[++i], out int port) && port > 0 && port <= 65535)
                        {
                            options.Port = port;
                        }
                        else
                        {
                            Console.WriteLine("Error: --port must be a valid port number (1-65535)");
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: --port requires a value");
                        return null;
                    }

                    break;

                case "--interval":
                case "-i":
                    if (i + 1 < args.Length)
                    {
                        if (int.TryParse(args[++i], out int interval) && interval > 0)
                        {
                            options.Interval = interval;
                        }
                        else
                        {
                            Console.WriteLine("Error: --interval must be a positive integer");
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: --interval requires a value");
                        return null;
                    }

                    break;
                default:
                    Console.WriteLine($"Error: Unknown argument '{args[i]}'");
                    return null;
            }
        }

        // Validate required parameters
        if (options.Hosts.Count == 0)
        {
            Console.WriteLine("Error: --hosts is required");
            return null;
        }

        if (options.Port == 0)
        {
            Console.WriteLine("Error: --port is required");
            return null;
        }

        if (options.Interval == 0)
        {
            Console.WriteLine("Error: --interval is required");
            return null;
        }

        return options;
    }
}