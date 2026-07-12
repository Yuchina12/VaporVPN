using System.Drawing;

namespace VaporVPNApp;

public partial class Form1 : Form
{
    private readonly Label _statusLabel;
    private readonly ComboBox _providerBox;
    private readonly ComboBox _locationBox;
    private readonly Button _connectButton;
    private readonly Button _disconnectButton;

    public Form1()
    {
        InitializeComponent();

        Text = "VaporVPN v1.1";
        Size = new Size(760, 500);
        MinimumSize = new Size(720, 480);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(8, 16, 31);
        ForeColor = Color.White;

        var headerPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100,
            BackColor = Color.FromArgb(12, 24, 44)
        };

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
            Text = "Private browsing, fast routing, and secure access across North America.",
            AutoSize = true,
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(180, 210, 255),
            Location = new Point(24, 62)
        };

        var versionLabel = new Label
        {
            Text = "Version 1.1",
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 230, 255),
            Location = new Point(620, 34)
        };

        headerPanel.Controls.Add(titleLabel);
        headerPanel.Controls.Add(subtitleLabel);
        headerPanel.Controls.Add(versionLabel);

        var bodyPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24),
            BackColor = Color.Transparent
        };

        var introLabel = new Label
        {
            Text = "Choose a secure gateway and connect in one click.",
            AutoSize = true,
            Font = new Font("Segoe UI", 13),
            ForeColor = Color.FromArgb(215, 232, 255),
            Location = new Point(24, 24)
        };

        var providerLabel = new Label
        {
            Text = "VPN Provider",
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(24, 84)
        };

        _providerBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 11),
            Width = 240,
            Height = 32,
            Location = new Point(24, 108)
        };
        _providerBox.Items.AddRange(new object[] { "Bell / Rogers", "Telus", "Shaw", "Freedom Mobile" });
        _providerBox.SelectedIndex = 0;

        var locationLabel = new Label
        {
            Text = "VPN Location",
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(300, 84)
        };

        _locationBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 11),
            Width = 240,
            Height = 32,
            Location = new Point(300, 108)
        };
        _locationBox.Items.AddRange(new object[] { "Toronto", "Montreal", "Vancouver", "Calgary" });
        _locationBox.SelectedIndex = 0;

        _statusLabel = new Label
        {
            Text = "Disconnected",
            AutoSize = true,
            Font = new Font("Segoe UI", 15, FontStyle.Bold),
            ForeColor = Color.FromArgb(255, 214, 102),
            Location = new Point(24, 188)
        };

        _connectButton = new Button
        {
            Text = "Connect",
            Size = new Size(160, 46),
            Location = new Point(24, 232),
            BackColor = Color.FromArgb(77, 166, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        _connectButton.Click += HandleConnect;

        _disconnectButton = new Button
        {
            Text = "Disconnect",
            Size = new Size(160, 46),
            Location = new Point(208, 232),
            BackColor = Color.FromArgb(70, 90, 120),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        _disconnectButton.Click += HandleDisconnect;

        var infoPanel = new Panel
        {
            Size = new Size(660, 130),
            Location = new Point(24, 300),
            BackColor = Color.FromArgb(18, 35, 58),
            Padding = new Padding(16)
        };

        var infoLabel = new Label
        {
            Text = "Status: Ready\nProvider: Bell / Rogers\nLocation: Toronto\nProtocol: WireGuard",
            AutoSize = true,
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(225, 238, 255),
            Location = new Point(16, 16)
        };

        infoPanel.Controls.Add(infoLabel);

        bodyPanel.Controls.Add(introLabel);
        bodyPanel.Controls.Add(providerLabel);
        bodyPanel.Controls.Add(_providerBox);
        bodyPanel.Controls.Add(locationLabel);
        bodyPanel.Controls.Add(_locationBox);
        bodyPanel.Controls.Add(_statusLabel);
        bodyPanel.Controls.Add(_connectButton);
        bodyPanel.Controls.Add(_disconnectButton);
        bodyPanel.Controls.Add(infoPanel);

        Controls.Add(headerPanel);
        Controls.Add(bodyPanel);
    }

    private void HandleConnect(object? sender, EventArgs e)
    {
        var provider = _providerBox.SelectedItem?.ToString() ?? "Bell / Rogers";
        var location = _locationBox.SelectedItem?.ToString() ?? "Toronto";
        _statusLabel.Text = $"Connected to {location} via {provider}";
        _statusLabel.ForeColor = Color.FromArgb(102, 255, 153);
    }

    private void HandleDisconnect(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Disconnected";
        _statusLabel.ForeColor = Color.FromArgb(255, 214, 102);
    }
}
