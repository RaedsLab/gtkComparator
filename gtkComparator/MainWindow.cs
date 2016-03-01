using System;
using Gtk;
using System.Diagnostics;
using System.IO;

public partial class MainWindow: Gtk.Window
{	

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void clickBtn (object sender, EventArgs e)
	{

		var realUri = realValuesFileInput.Filename;
		var expectedUri = expectedValuesFileInput.Filename;

		try{
			/*
			 * @TODO: move python file in same directory 
			 * */
			run_cmd("/home/r3d/Code/PFE/sourceFiles/resTest.py", realUri+" "+expectedUri);
		}catch(Exception ex){
			Console.WriteLine (ex.Message);
		}
	}

	private void run_cmd(string cmd, string args)
	{
		ProcessStartInfo start = new ProcessStartInfo();
		start.FileName = "/usr/bin/python";
		start.Arguments = string.Format("{0} {1}", cmd, args);
		start.UseShellExecute = false;
		start.RedirectStandardOutput = true;
		using(Process process = Process.Start(start))
		{
			using(StreamReader reader = process.StandardOutput)
			{
				string result = reader.ReadToEnd();

				textview2.Buffer.Text = result;
				Console.Write(result);
			}
		}
	}
}

