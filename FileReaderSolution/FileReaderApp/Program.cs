using FileReaderLibrary;
using FileReaderLibrary.Encryption;
using FileReaderLibrary.Enums;
using FileReaderLibrary.Interfaces;
using FileReaderLibrary.Security;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║       File Reader CLI Application    ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine();

while (true)
{
    // ── 1. Choose file type ─────────────────────────────────────────────
    EFileType fileType = PromptFileType();

    // ── 2. Encryption? ──────────────────────────────────────────────────
    bool useEncryption = PromptYesNo("Use encryption? (y/n): ");

    IEncryptionAlgorithm? encryptionAlgorithm = null;
    if (useEncryption)
        encryptionAlgorithm = ChooseEncryptionAlgorithm();

    // ── 3. Role-based security? ─────────────────────────────────────────
    bool useSecurity = PromptYesNo("Use role-based security? (y/n): ");

    ISecurityContext? securityContext = null;
    string role = string.Empty;
    if (useSecurity)
    {
        securityContext = new SimpleSecurityContext();
        role = PromptString("Enter your role (Admin / Manager / User): ");
    }

    // ── 4. Build the reader ─────────────────────────────────────────────
    IFileReader reader;
    try
    {
        var builder = new FileReaderBuilder()
            .WithFileType(fileType);

        if (encryptionAlgorithm is not null)
            builder.WithEncryption(encryptionAlgorithm);

        if (securityContext is not null)
            builder.WithSecurity(securityContext, role);

        reader = builder.Build();
    }
    catch (NotSupportedException ex)
    {
        WriteError($"Configuration error: {ex.Message}");
        Console.WriteLine();
        continue;
    }

    // ── 5. File path ────────────────────────────────────────────────────
    string filePath = PromptString("Enter the file path: ");

    // ── 6. Read and display ─────────────────────────────────────────────
    Console.WriteLine();
    Console.WriteLine("─── File Content ───────────────────────");
    try
    {
        string content = reader.Read(filePath);
        Console.WriteLine(content);
    }
    catch (UnauthorizedAccessException ex)
    {
        WriteError($"Access denied: {ex.Message}");
    }
    catch (FileNotFoundException)
    {
        WriteError($"File not found: {filePath}");
    }
    catch (Exception ex)
    {
        WriteError($"Error reading file: {ex.Message}");
    }
    Console.WriteLine("────────────────────────────────────────");
    Console.WriteLine();

    // ── 7. Continue? ────────────────────────────────────────────────────
    if (!PromptYesNo("Read another file? (y/n): "))
        break;

    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine("Goodbye!");

// ── Helpers ─────────────────────────────────────────────────────────────

static EFileType PromptFileType()
{
    while (true)
    {
        Console.WriteLine("Select file type:");
        Console.WriteLine("  1) TEXT");
        Console.WriteLine("  2) XML");
        Console.WriteLine("  3) JSON");
        Console.Write("Choice [1-3]: ");
        string? input = Console.ReadLine()?.Trim();
        switch (input)
        {
            case "1": return EFileType.Text;
            case "2": return EFileType.Xml;
            case "3": return EFileType.Json;
            default:
                WriteError("Invalid choice. Please enter 1, 2, or 3.");
                break;
        }
    }
}

static IEncryptionAlgorithm ChooseEncryptionAlgorithm()
{
    Console.WriteLine("Select encryption algorithm:");
    Console.WriteLine("  1) Reverse (demo)");
    Console.Write("Choice [1]: ");
    Console.ReadLine();
    return new ReverseEncryptionAlgorithm();
}

static bool PromptYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? answer = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (answer is "y" or "yes") return true;
        if (answer is "n" or "no")  return false;
        WriteError("Please enter y or n.");
    }
}

static string PromptString(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? value = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(value)) return value;
        WriteError("Input cannot be empty.");
    }
}

static void WriteError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}