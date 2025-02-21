using System;
using System.Windows.Forms;

namespace SmartStandards {

  internal static class Program {

    [STAThread]
    static void Main() {
      ApplicationConfiguration.Initialize();
      Application.Run(new MainForm());
    }
  }

}
