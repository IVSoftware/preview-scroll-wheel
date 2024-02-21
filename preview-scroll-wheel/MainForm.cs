

namespace prewiew_scroll_wheel
{
    public partial class MainForm : Form , IMessageFilter
    {
        public MainForm()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            Disposed += (sender, e) =>Application.RemoveMessageFilter(this);
        }

        int _debugCount = 1;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                // 'Generically' for SplitContainer or by any control placed within one.
                if (Control.FromHandle(m.HWnd) is Control control && localIsDescOfSplitContainer(control))
                {
#if DEBUG
                    if (control is RichTextBox logger)
                    {
                        logger.AppendText($"Count : {_debugCount++}{Environment.NewLine}");
                        logger.ScrollToCaret();
                    }
#endif
                    if (ModifierKeys == Keys.Control)
                    {
                        int delta = (int)m.WParam >> 16;
                        if (delta > 0) control.Scale(new SizeF(1.1f, 1.1f));
                        else control.Scale(new SizeF(0.9f, 0.9f));
                    }
                    m.Result = (IntPtr)1;
                    return true;
                }

                bool localIsDescOfSplitContainer(Control? aspirant)
                {
                    while (aspirant != null)
                    {
                        if (aspirant is SplitContainer) return true;
                        aspirant = aspirant.Parent;
                    }
                    return false;
                }
            }
            base.WndProc(ref m);
            return false;
        }
        private const int WM_MOUSEWHEEL = 0x020A;
    }
}
