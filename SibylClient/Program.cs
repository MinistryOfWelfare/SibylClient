using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WotoGUI.Client;
using osu.Framework;
using osu.Framework.Development;
using osu.Framework.Logging;
using osu.Framework.Platform;
using SibylClient.Client;
using SibylClient.IPC;

namespace SibylClient
{
	class Program
	{
		[STAThread]
		public static int Main(string[] args)
		{
			
			using (var provider = new AppHostProvider("SibylClient"))
			{
				// Back up the cwd before app client changes it
				var cwd = Environment.CurrentDirectory;
				var hasArgs = args != null && args.Length > 0;
				var mode = ClientStartMode.Normal;
				/*
				if (hasArgs && args[0] == "--other-mode")
				{
					mode = ClientStartMode.OtherMode;
				}
				*/
				if (!provider.IsPrimaryInstance)
				{
					if (hasArgs && args[0].Contains('.')) // easy way to check for a file import in args
					{
						var importer = new ArchiveImportIPCChannel(AppClient.DHost);

						foreach (var file in args)
						{
							Console.WriteLine(@"Importing {0}", file);
							if (!importer.ImportAsync(Path.GetFullPath(file, cwd)).Wait(3000))
								throw new TimeoutException(@"IPC took too long to send");
						}

						return 0;
					}
					// we want to allow multiple instances to be started when in debug.
					if (!provider.IsDebugBuild)
					{
						return 0;
					}
				}
				
				using (var client = WotoClient.GetWotoClient(provider))
				{
					client.Run(mode);
				}

				return 0;
			}
		}
	}
}
