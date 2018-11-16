using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ImageResizer;
//using System.Configuration;
//using ImageResizer.ExtensionMethods;




namespace ImgTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync(
            [BlobTrigger("samples-workitems/source/{name}", Connection = "")]Stream myBlob, 
            string name,
            [Blob("samples-workitems/destination/{name}", FileAccess.Write)]Stream outputBlob,
            TraceWriter log,
            CancellationToken token)
        {
            const int maxWidth = 500;  // in pixels
            const int maxHeight = 500;
            const int jpgQuality = 85; // in percent

            // Types of files to be resized
            string imgRegex = @"([^\s]+(\.(?i)(jpg|png|gif|bmp))$)";


            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            // If the file maches a supported image type, then run ImageBuilder on it 
            if (Regex.IsMatch(name, imgRegex))
            {
                var instructions = new Instructions
                {
                    Width = maxWidth,  // maximum width
                    Height = maxHeight,  // maximum height
                    Mode = FitMode.Max,  // reduce file dimension to be no larger than the width and height parameters
                    Scale = ScaleMode.DownscaleOnly, // only reduce file dimension, do not enlarge them. Keep aspect ratio
                    JpegQuality = jpgQuality       // reduce quality of jpgs to same file space.
                };
                log.Info($"Process {name} - resizing if too large.");
                try
                {
                    ImageBuilder.Current.Build(new ImageJob(myBlob, outputBlob, instructions));
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
            }
            else
            {
                // If the file is not a supported image type, then copy files directly to the destination without any changes.
                log.Info($"Copy {name} directly to storage");
                try
                {
                    //await myBlob.CopyToAsync(outputBlob, 4096, token);
                    await myBlob.CopyToAsync(outputBlob);
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
            }
        }
    }
}
