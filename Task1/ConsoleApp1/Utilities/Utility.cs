using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

public static class Utility
{
    public enum MessageType
    {
        ERROR,
        WARNING,
        INFO
    }

    public static bool EmptyInput(string input)
    {
        // empty
        if (input != null && input.Length == 0)
        {
            StyledMessage("You cannot enter an empty string!");
            return true;
        }

        return false;
    }

    public static void StyledMessage(string message, MessageType type = MessageType.WARNING)
    {
        switch (type)
        {
            case MessageType.ERROR:
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case MessageType.WARNING:
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                break;
            default:
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }

        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ThrowCustomException(string message)
    {
        StyledMessage(message, MessageType.ERROR);
        throw new Exception(message);
    }
}
