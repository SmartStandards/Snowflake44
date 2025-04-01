using System;
using System.Windows.Forms;

namespace SmartStandards {

  internal static class Program {

    [STAThread]
    static void Main() {

      // ApplicationConfiguration.Initialize(); // generates build error
      // "error CS0103: The name 'ApplicationConfiguration' does not exist in the current context."

      // workaround...

      global::System.Windows.Forms.Application.EnableVisualStyles();
      global::System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
      global::System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);

      // ...workaround

      Application.Run(new MainForm());
    }
  }

}
