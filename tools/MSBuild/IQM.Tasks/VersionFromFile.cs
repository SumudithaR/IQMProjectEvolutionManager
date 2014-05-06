using System;
using System.IO;

using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;


namespace IQM.Tasks
{
    /// <summary>
    /// Version From File Task
    /// </summary>
    public class VersionFromFile : Task
    {
        /// <summary>
        /// Gets or sets the input file.
        /// </summary>
        /// <value>The input file.</value>
        [Required]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets or sets the output file.
        /// </summary>
        /// <value>The output file.</value>
        [Required]
        public string OutputFile { get; set; }
        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            if (File.Exists(InputFile))
            {
                var versionText = File.ReadAllText(InputFile);
                var lines = new[]
                                {
                                    "using System.Reflection;",
                                    string.Empty,
                                    "[assembly: AssemblyVersion(\"" + versionText + "\")]",
                                    "[assembly: AssemblyFileVersion(\"" + versionText + "\")]"
                                };
                if (File.Exists(OutputFile))
                {
                    File.Delete(OutputFile);
                }

                File.WriteAllLines(OutputFile, lines);
            }

            return true;
        }
    }
}
