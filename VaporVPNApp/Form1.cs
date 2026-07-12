using System.Drawing;

namespace VaporVPNApp;

public partial class Form1 : Form
{
    private readonly Label _statusLabel;
    private readonly Button _connectButton;
    private readonly Button _disconnectButton;

    public Form1()
    {
        InitializeComponent();

        Text = "VaporVPN";
        Size = new Size(620, 360);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(10, 16, 28);
        ForeColor = Color.White;

        var titleLabel = new Label
        {
            Text = "VaporVPN",
            AutoSize = true,
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(24, 24)
        };

        var subtitleLabel = new Label
        {
            Text = "Fast, private, and ready for secure browsing.",
            AutoSize = true,
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(180, 210, 255),
            Location = new Point(24, 72)
        };

        _statusLabel = new Label
        {
            Text = "Disconnected",
            AutoSize = true,
            Font = new Font("Segoe UI", 13, FontStyle.Bold),
            ForeColor = Color.FromArgb(255, 214, 102),
            Location = new Point(24, 140)
        };

        _connectButton = new Button
        {
            Text = "Connect",
            Size = new Size(140, 42),
            Location = new Point(24, 188),
            BackColor = Color.FromArgb(77, 166, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        _connectButton.Click += HandleConnect;

        _disconnectButton = new Button
        {
            Text = "Disconnect",
            Size = new Size(140, 42),
            Location = new Point(184, 188),
            BackColor = Color.FromArgb(70, 90, 120),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        _disconnectButton.Click += HandleDisconnect;

        Controls.Add(titleLabel);
        Controls.Add(subtitleLabel);
        Controls.Add(_statusLabel);
        Controls.Add(_connectButton);
        Controls.Add(_disconnectButton);
    }

    private void HandleConnect(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Connected to VaporVPN secure tunnel";
        _statusLabel.ForeColor = Color.FromArgb(102, 255, 153);
    }

    private void HandleDisconnect(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Disconnected";
        _statusLabel.ForeColor = Color.FromArgb(255, 214, 102);
    }
}
