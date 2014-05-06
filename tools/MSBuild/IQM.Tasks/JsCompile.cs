using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Yahoo.Yui.Compressor;

namespace IQM.Tasks
{
    /// <summary>
    /// Compiles javascript files into single file and minifies it
    /// </summary>
    public class JsCompile : Task
    {
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public string Encoding { get; set; }

        /// <summary>
        /// Gets or sets the files to source-compress.
        /// </summary>
        [Required]
        public ITaskItem[] Files { get; set; }

        /// <summary>
        /// Gets or sets the exclude.
        /// </summary>
        /// <value>
        /// The exclude.
        /// </value>
        [Required]
        public ITaskItem[] ExcludeFiles { get; set; }

        /// <summary>
        /// Gets or sets the output file.
        /// </summary>
        /// <value>
        /// The output file.
        /// </value>
        [Required]
        public string OutputFile { get; set; }

        /// <summary>
        /// Gets or sets the output min file.
        /// </summary>
        /// <value>
        /// The output min file.
        /// </value>
        [Required]
        public string OutputMinFile { get; set; }

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            if (OutputFile == OutputMinFile)
            {
                Log.LogError("Min output filename cannot be the same as the output filename");
            }

            if (Files == null || Files.Length == 0)
            {
                Log.LogError("No Files");
                return true;
            }

            var excludeFiles = (ExcludeFiles ?? new ITaskItem[] {}).ToList();

            var includeFiles = Files.Except(excludeFiles).ToList().Where(x => x.ItemSpec != OutputFile || x.ItemSpec != OutputMinFile);

            Log.LogMessage("Include Count: {0}", includeFiles.Count());

            using (var file = File.Create(OutputFile))
            {
                using (var outputFileStream = new StreamWriter(file))
                {
                    foreach (var taskItem in includeFiles)
                    {
                        Log.LogMessage(MessageImportance.Normal, "Include: {0}", taskItem.ItemSpec);
                        using (var inputStream = new StreamReader(taskItem.ItemSpec))
                        {
                            while (!inputStream.EndOfStream)
                            {
                                outputFileStream.WriteLine(inputStream.ReadLine());
                            }
                        }
                    }
                    outputFileStream.Close();
                }
                file.Close();
            }

            Encoding encoding = null;
            string encodingName = Encoding ?? string.Empty;

            if (encodingName.Length > 0)
            {
                encoding = System.Text.Encoding.GetEncoding(encodingName);
            }

            using (StreamReader reader = encoding != null ?
                        new StreamReader(OutputFile, encoding) : new StreamReader(OutputFile))
            {
                if (encoding == null)
                {
                    encoding = reader.CurrentEncoding;
                }

                File.WriteAllText(OutputMinFile, JavaScriptCompressor.Compress(reader.ReadToEnd(), false, false, true, true, -1), encoding);
            }

            return !Log.HasLoggedErrors;
        }
    }
}