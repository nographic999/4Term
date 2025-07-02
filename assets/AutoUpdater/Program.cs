using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    /*----------------------------------------------------------
     * CheckGitHubConnection
     * 
     * Attempts to ping github.com up to a specified number of times.
     * Returns true if a successful ping reply is received, otherwise false.
     * Includes retry delays and logs each attempt to the console.
     *---------------------------------------------------------*/
    static bool CheckGitHubConnection()
    {
        string githubHost = "github.com";
        int maxAttempts = 3;

        /* Attempt to ping GitHub */
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                Console.WriteLine($"Pinging {githubHost}... Attempt {attempt} of {maxAttempts}");

                /* Create a new Ping instance and send a ping request to GitHub */
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(githubHost, 1000);
                    if (reply.Status == IPStatus.Success)
                    {
                        /* Ping succeeded, GitHub is reachable */
                        Console.WriteLine($"Received reply from {githubHost} — GitHub is reachable.");
                        return true;
                    }
                    else
                    {
                        /*  Ping sent but no successful reply received */
                        Console.WriteLine($"Ping to {githubHost} failed.");
                    }
                }
            }
            catch
            {
                /* Exception occurred while trying to ping (network issues, permissions, etc.) */
                Console.WriteLine($"Ping to {githubHost} failed.");
            }

            /* If this is not the last attempt, wait before retrying */
            if (attempt < maxAttempts)
            {
                Console.WriteLine("Retrying in 3 seconds...");
                Thread.Sleep(3000);
            }
        }

        /* All attempts failed; GitHub is unreachable */
        Console.WriteLine($"Failed to reach {githubHost} after {maxAttempts} attempts.");
        return false;
    }

    /*----------------------------------------------------------
     * GetRemoteVersionAsync
     * 
     * Asynchronously downloads the content of the specified version URL.
     * Returns the trimmed version string on success, or an empty string on failure.
     *---------------------------------------------------------*/
    static async Task<string> GetRemoteVersionAsync(string versionUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                return (await client.GetStringAsync(versionUrl)).Trim();
            }
            catch
            {
                return "none";
            }
        }
    }

    /*----------------------------------------------------------
     * DownloadFileAsync
     * 
     * Asynchronously downloads a file from the specified URL to the given
     * destination path. Displays a progress bar in the console during the download.
     * Prints success or error messages accordingly.
     *---------------------------------------------------------*/
    static async Task DownloadFileAsync(string url, string destinationPath)
    {
        /* Print header with separators */
        Console.WriteLine(new string('-', 100));
        Console.WriteLine($"Downloading file: {destinationPath}");
        Console.WriteLine(new string('-', 100));

        /* Create HttpClient for downloading */
        using (HttpClient client = new HttpClient())
        {
            try
            {
                /* Send GET request, only reading headers initially */
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    /* Throw if HTTP response is unsuccessful */
                    response.EnsureSuccessStatusCode();

                    /* Get total content length (if available) to report progress */
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var canReportProgress = totalBytes != -1;

                    /* Open streams: content stream for reading and file stream for writing */
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        var totalRead = 0L;              /* Track total bytes read so far */
                        var buffer = new byte[8192];    /* Buffer for reading chunks */
                        int bytesRead;                  /* Bytes read in current read */
                        int lastProgress = 0;           /* Track last reported progress percent */
                        const int barSize = 72;         /* Width of progress bar */

                        /* Read from content stream until no more data */
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            /* Write downloaded bytes to file */
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            /* If we know total size, update progress bar */
                            if (canReportProgress)
                            {
                                int progress = (int)((totalRead * 100) / totalBytes);

                                /* Only update progress if percent changed */
                                if (progress != lastProgress)
                                {
                                    lastProgress = progress;

                                    /* Calculate hashes and spaces for progress bar */
                                    int hashes = (int)((progress / 100.0) * barSize);
                                    int spaces = barSize - hashes;

                                    /* Print progress bar with cyan-colored hashes */
                                    Console.Write("\r[");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write(new string('#', hashes));
                                    Console.ResetColor();
                                    Console.Write(new string(' ', spaces));
                                    Console.Write($"] {progress}%   ");
                                }
                            }
                        }
                    }

                    /* Download finished successfully */
                    Console.WriteLine("\nDownload completed successfully!");
                }
            }
            catch (Exception ex)
            {
                /* Handle any errors during download */
                Console.WriteLine($"\nDownload failed: {ex.Message}");
            }
        }
    }

    /*----------------------------------------------------------
     * ExtractWith7zDecExe
     * 
     * Extracts the specified archive using 7zDec.exe located
     * in the application base directory. Displays extraction
     * progress and errors in real time.
     *---------------------------------------------------------*/
    static bool ExtractWith7zDecExe(string archivePath)
    {
        /* Get the full path to 7zDec.exe in the application directory */
        string sevenZipDecExe = Path.Combine(AppContext.BaseDirectory, "7zDec.exe");

        /* Check if 7zDec.exe exists */
        if (!File.Exists(sevenZipDecExe))
        {
            Console.WriteLine("Missing: 7zDec.exe");
            return false;
        }

        /* Setup ProcessStartInfo to run 7zDec.exe with arguments */
        var psi = new ProcessStartInfo
        {
            FileName = sevenZipDecExe,
            Arguments = $"x \"{archivePath}\"",        /* Extract archive with full paths */
            RedirectStandardOutput = true,             /* Redirect stdout to capture output */
            RedirectStandardError = true,              /* Redirect stderr to capture errors */
            UseShellExecute = false,                    /* Required for redirection */
            CreateNoWindow = true                       /* Do not create a console window */
        };

        try
        {
            /* Start the extraction process */
            using (var process = Process.Start(psi))
            {
                if (process == null)
                {
                    Console.WriteLine("Could not start 7zDec process.");
                    return false;
                }

                /* Capture and print standard output asynchronously */
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        Console.WriteLine(e.Data);
                };

                /* Capture and print standard error asynchronously */
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        Console.WriteLine("Error: " + e.Data);
                };

                /* Begin asynchronous reading of output and error streams */
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                /* Wait for the extraction process to finish */
                process.WaitForExit();

                /* Check exit code to determine success */
                if (process.ExitCode == 0)
                {
                    Console.WriteLine("Extraction successful.");
                    return true;
                }

                Console.WriteLine($"7zDec failed (exit code {process.ExitCode})");
                return false;
            }
        }
        catch (Exception ex)
        {
            /* Handle exceptions during process start or execution */
            Console.WriteLine("Extraction error: " + ex.Message);
            return false;
        }
    }


    /*----------------------------------------------------------
     * WelcomeMsg
     * 
     * Displays the ASCII art title and version information for
     * the 4Term Auto-Updater. Each line of the banner is printed
     * with a rotating console text color for visual emphasis.
     *---------------------------------------------------------*/
    static void WelcomeMsg()
    {
        /* ASCII art banner representing the application */
        string[] asciiLines = new string[]
            {
                "                                Launching 4Term Auto-Updater...",
                "  ____________/\\\\\\_____/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\__________________________________________________",
                "   __________/\\\\\\\\\\____\\///////\\\\\\/////___________________________________________________",
                "    ________/\\\\\\/\\\\\\__________\\/\\\\\\________________________________________________________",
                "     ______/\\\\\\/\\/\\\\\\__________\\/\\\\\\___________/\\\\\\\\\\\\\\\\___/\\\\/\\\\\\\\\\\\\\_____/\\\\\\\\\\__/\\\\\\\\\\___",
                "      ____/\\\\\\/__\\/\\\\\\__________\\/\\\\\\_________/\\\\\\/////\\\\\\_\\/\\\\\\/////\\\\\\__/\\\\\\///\\\\\\\\\\///\\\\\\_",
                "       __/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\_______\\/\\\\\\________/\\\\\\\\\\\\\\\\\\\\\\__\\/\\\\\\___\\///__\\/\\\\\\_\\//\\\\\\__\\/\\\\\\_",
                "        _\\///////////\\\\\\//________\\/\\\\\\_______\\//\\\\///////___\\/\\\\\\_________\\/\\\\\\__\\/\\\\\\__\\/\\\\\\_",
                "         ___________\\/\\\\\\__________\\/\\\\\\________\\//\\\\\\\\\\\\\\\\\\\\_\\/\\\\\\_________\\/\\\\\\__\\/\\\\\\__\\/\\\\\\_",
                "          ___________\\///___________\\///__________\\//////////__\\///__________\\///___\\///___\\///__",
                "                                                                           Version: 1.0.1-beta x64\n"
            };

        /* Array of colors used to cycle through for each line */
        ConsoleColor[] colors = new ConsoleColor[]
        {
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.Red,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan
        };

        /* Print each line of the banner with a rotating color */
        for (int i = 0; i < asciiLines.Length; i++)
        {
            Console.ForegroundColor = colors[i % colors.Length];
            Console.WriteLine(asciiLines[i]);
        }

        /* Reset the console text color to default */
        Console.ResetColor();
    }

    const string versionFile = "version.txt";
    const string GitHubUrl = "https://raw.githubusercontent.com/nographic999/4Term/main/";
    static string versionContent = "none";
    static string remoteVersion = "none";

    /*----------------------------------------------------------
     * Main
     * 
     * The program's entry point.
     * - Displays welcome message.
     * - Checks internet connectivity.
     * - Reads local version file or downloads if missing.
     * - Retrieves remote version from GitHub.
     * - Compares local and remote versions.
     * - Downloads and extracts update files if a new version is found.
     * - Reports status messages throughout the process.
     *---------------------------------------------------------*/
        static int Main()
    {
        /* Show program welcome message */
        WelcomeMsg();

        Console.WriteLine(new string('-', 100));

        /* Check if internet connection is available */
        if (!CheckGitHubConnection())
            return 1;

        Console.WriteLine(new string('-', 100));

        /* Check if version file exist */
        if (File.Exists(versionFile))
        {
            versionContent = File.ReadAllText(versionFile).Trim();
            Console.WriteLine($"{versionFile} found.");
        }
            
        /* Download if it doesn't exist */
        else
        {
            Console.WriteLine($"{versionFile} missing, downloading...");
        }

        Console.WriteLine(new string('-', 100));

        /* Read and show version file content */

        Console.WriteLine($"Version content: {versionContent}");

        /* Get and show remote version */
        remoteVersion = GetRemoteVersionAsync(GitHubUrl + versionFile).GetAwaiter().GetResult();
        Console.WriteLine($"Remote version: {remoteVersion}");

        Console.WriteLine(new string('-', 100));

        /* Simple string match */
        if (versionContent == remoteVersion)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Local version is up to date.");
            Console.ResetColor();
            Console.WriteLine(new string('-', 100));
        }
        else
        {
            Console.WriteLine("New version detected. Downloading update files...");
            DownloadFileAsync(GitHubUrl + remoteVersion, remoteVersion).GetAwaiter().GetResult();
            DownloadFileAsync(GitHubUrl + versionFile, versionFile).GetAwaiter().GetResult();
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("Update files downloaded successfully.");
            Console.WriteLine(new string('-', 100));

            if (!ExtractWith7zDecExe(remoteVersion))
                return 1;
            Console.WriteLine(new string('-', 100));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Application updated successfully.");
            Console.ResetColor();
            Console.WriteLine(new string('-', 100));
            File.Delete(remoteVersion);
        }
        return 0;
    }
}
