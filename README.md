# Preview Scroll Wheel

One approach is to set a hook in your `MainForm` using [IMessageFilter](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.imessagefilter?view=windowsdesktop-8.0_). The advantage of doing it here is that it gives fine control over which specific controls, or control types, will receive special treatment of the mouse wheel events, and provides this at the application level without having to make unnecessary subclasses or custom controls.


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
                    // Return here to suppress further actions ONLY
                    // for the ModifierKeys == Control case.
                    // m.Result = (IntPtr)1;
                    // return true;
                }
                // Or here to suppress ALL normal functioning of the scroll wheel.
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
    private const int WM_MOUSEWHEEL = 0x020A;
}
```
___

**Example**

[![init][1]][1]

In this example, a `RichTextBox` is placed on `SplitContainer.Panel2`. When the mouse wheel is scrolled, the effect on the text box is suppressed. You actually have to drag the `VScrollBar` to scroll the text box. 

[![suppressed][2]][2]


But when the Control key is active, the size of the text box will scale.

[![scaling][3]][3]


  [1]: https://i.stack.imgur.com/0YCHI.png
  [2]: https://i.stack.imgur.com/YTu1E.png
  [3]: https://i.stack.imgur.com/Qcrwn.png