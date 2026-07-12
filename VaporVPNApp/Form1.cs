using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace VaporVPNApp;

public partial class Form1 : Form
{
    private readonly Label _statusLabel;
    private readonly ComboBox _providerBox;
    private readonly ComboBox _ispBox;
    private readonly ComboBox _locationBox;
    private readonly Button _connectButton;
    private readonly Button _disconnectButton;
    private readonly TextBox _connectionNameBox;
    private readonly TextBox _serverBox;
    private readonly ComboBox _vpnTypeBox;
    private readonly ComboBox _signInBox;
    private readonly TextBox _usernameBox;
    private readonly TextBox _passwordBox;
    private readonly PictureBox _logoBox;
    private readonly Label _infoLabel;

    public Form1()
    {
        InitializeComponent();

        Text = "VaporVPN v1.3.1";
        Size = new Size(760, 520);
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
            Text = "Join our custom VaporVPN Ethernet network and connect through secure regional gateways.",
            AutoSize = true,
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(180, 210, 255),
            Location = new Point(24, 62)
        };

        var versionLabel = new Label
        {
            Text = "Version 1.3.1",
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(120, 230, 255),
            Location = new Point(620, 34),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
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
            Text = "Choose a custom gateway and connect in one click.",
            AutoSize = true,
            Font = new Font("Segoe UI", 13),
            ForeColor = Color.FromArgb(215, 232, 255),
            Location = new Point(24, 24)
        };

        var providerLabel = new Label
        {
            Text = "Network Adapter",
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(24, 84)
        };

        _providerBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 11),
            Width = 360,
            Height = 32,
            Location = new Point(24, 108)
        };

        // Enumerate network interfaces and label them as VaporVPN Ethernet Provider #N
        var nics = NetworkInterface.GetAllNetworkInterfaces()
            .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .ToArray();
        if (nics.Length == 0)
        {
            _providerBox.Items.Add("VaporVPN Ethernet Provider #1 - Unknown");
            _providerBox.SelectedIndex = 0;
        }
        else
        {
            for (int i = 0; i < nics.Length; i++)
            {
                var nic = nics[i];
                var label = $"VaporVPN Ethernet Provider #{i + 1} - {nic.Name}";
                _providerBox.Items.Add(label);
            }
            var upIndex = Array.FindIndex(nics, ni => ni.OperationalStatus == OperationalStatus.Up);
            _providerBox.SelectedIndex = upIndex >= 0 ? upIndex : 0;
        }

        // ISP/provider selector (Bell / Rogers / Telus / Other)
        _ispBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 11),
            Width = 240,
            Height = 32,
            Location = new Point(24, 174)
        };
        _ispBox.Items.AddRange(new object[] { "Bell / Rogers", "Telus", "Shaw", "Other" });
        _ispBox.SelectedIndex = 0;

        // Connection details inputs
        var connNameLabel = new Label
        {
            Text = "Connection name",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 84)
        };
        _connectionNameBox = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 300,
            Location = new Point(420, 108)
        };

        var serverLabel = new Label
        {
            Text = "Server name or address",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 144)
        };
        _serverBox = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 300,
            Location = new Point(420, 168)
        };

        var vpnTypeLabel = new Label
        {
            Text = "VPN type",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 204)
        };
        _vpnTypeBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10),
            Width = 220,
            Location = new Point(420, 228)
        };
        _vpnTypeBox.Items.AddRange(new object[] { "Automatic", "PPTP", "L2TP/IPsec", "SSTP", "IKEv2", "WireGuard" });
        _vpnTypeBox.SelectedIndex = 0;

        var signInLabel = new Label
        {
            Text = "Type of sign-in info",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 264)
        };
        _signInBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10),
            Width = 220,
            Location = new Point(420, 288)
        };
        _signInBox.Items.AddRange(new object[] { "Username and password", "Certificate", "Smart card", "None" });
        _signInBox.SelectedIndex = 0;

        var usernameLabel = new Label
        {
            Text = "Username (optional)",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 324)
        };
        _usernameBox = new TextBox { Font = new Font("Segoe UI", 10), Width = 300, Location = new Point(420, 348) };

        var passwordLabel = new Label
        {
            Text = "Password (optional)",
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.White,
            Location = new Point(420, 384)
        };
        _passwordBox = new TextBox { Font = new Font("Segoe UI", 10), Width = 300, Location = new Point(420, 408), UseSystemPasswordChar = true };

        // Logo picture box (loads assets/logo.png if present)
        _logoBox = new PictureBox
        {
            Size = new Size(120, 120),
            Location = new Point(620, 12),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };
        var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "logo.png");
        if (File.Exists(logoPath))
        {
            try { _logoBox.Image = Image.FromFile(logoPath); } catch { /* ignore */ }
        }
        else
        {
            // Try embedded resource fallback
            var asm = typeof(Form1).Assembly;
            var resourceNames = asm.GetManifestResourceNames();
            var resName = resourceNames.FirstOrDefault(n => n.EndsWith("assets.logo.png") || n.EndsWith("logo.png"));
            if (resName != null)
            {
                try
                {
                    using var s = asm.GetManifestResourceStream(resName);
                    if (s != null) _logoBox.Image = Image.FromStream(s);
                }
                catch { }
            }
        }

        var locationLabel = new Label
        {
            Text = "Gateway Location",
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
        _locationBox.Items.AddRange(new object[] { "Calgary", "Halifax", "Montreal", "Ottawa", "Toronto", "Vancouver", "Winnipeg" });
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
            Height = 120,
            Dock = DockStyle.Bottom,
            BackColor = Color.FromArgb(18, 35, 58),
            Padding = new Padding(16)
        };

        _infoLabel = new Label
        {
            Text = "Status: Ready\nNetwork Adapter: (not selected)\nProvider: Bell / Rogers\nLocation: Calgary\nProtocol: Ethernet Tunnel",
            AutoSize = true,
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(225, 238, 255),
            Location = new Point(16, 16)
        };

        infoPanel.Controls.Add(_infoLabel);

        bodyPanel.Controls.Add(introLabel);
        bodyPanel.Controls.Add(providerLabel);
        bodyPanel.Controls.Add(_providerBox);
        bodyPanel.Controls.Add(_logoBox);
        bodyPanel.Controls.Add(_ispBox);
        bodyPanel.Controls.Add(locationLabel);
        bodyPanel.Controls.Add(_locationBox);
        bodyPanel.Controls.Add(connNameLabel);
        bodyPanel.Controls.Add(_connectionNameBox);
        bodyPanel.Controls.Add(serverLabel);
        bodyPanel.Controls.Add(_serverBox);
        bodyPanel.Controls.Add(vpnTypeLabel);
        bodyPanel.Controls.Add(_vpnTypeBox);
        bodyPanel.Controls.Add(signInLabel);
        bodyPanel.Controls.Add(_signInBox);
        bodyPanel.Controls.Add(usernameLabel);
        bodyPanel.Controls.Add(_usernameBox);
        bodyPanel.Controls.Add(passwordLabel);
        bodyPanel.Controls.Add(_passwordBox);
        bodyPanel.Controls.Add(_statusLabel);
        bodyPanel.Controls.Add(_connectButton);
        bodyPanel.Controls.Add(_disconnectButton);
        // Add the info panel last so it docks to the bottom
        bodyPanel.Controls.Add(infoPanel);

        Controls.Add(headerPanel);
        Controls.Add(bodyPanel);
    }

    private void HandleConnect(object? sender, EventArgs e)
    {
        var adapter = _providerBox.SelectedItem?.ToString() ?? "VaporVPN Ethernet Adapter #IDNUMBER";
        var isp = _ispBox.SelectedItem?.ToString() ?? "Bell / Rogers";
        var location = _locationBox.SelectedItem?.ToString() ?? "Calgary";
        _statusLabel.Text = $"Connected to {location} on {adapter} using {isp}";
        _statusLabel.ForeColor = Color.FromArgb(102, 255, 153);
    }

    private void HandleDisconnect(object? sender, EventArgs e)
    {
        _statusLabel.Text = "Disconnected";
        _statusLabel.ForeColor = Color.FromArgb(255, 214, 102);
    }
}
