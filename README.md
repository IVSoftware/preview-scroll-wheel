# Preview Scroll Wheel

One approach is to set a hook in your `MainForm` using [IMessageFilter](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.imessagefilter?view=windowsdesktop-8.0_).

[![init][1]][1]

In this example, a `RichTextBox` is placed on `SplitContainer.Panel2`. When the mouse wheel is scrolled, the effect on the text box is suppressed. 

[![suppressed][2]][2]


You actually have to drag the `VScrollBar` to scroll the text box. But when the Control key is active, the size of the text box will scale.

[![scaling][3]][3]

```
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
```



  [1]: https://i.stack.imgur.com/0YCHI.png
  [2]: https://i.stack.imgur.com/YTu1E.png
  [3]: https://i.stack.imgur.com/Qcrwn.png