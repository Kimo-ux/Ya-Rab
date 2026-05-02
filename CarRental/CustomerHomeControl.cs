using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CarRental.DB;

namespace CarRental;

public class CustomerHomeControl : UserControl
{
    private readonly int ssn;
    private readonly string customerName;
    private readonly CustomerPortalDB db = new CustomerPortalDB();
    private readonly Label lblActiveRentalsValue;
    private readonly Label lblPendingPaymentsValue;
    private readonly Label lblAvailableCarsValue;

    public CustomerHomeControl(int ssn, string customerName)
    {
        this.ssn = ssn;
        this.customerName = string.IsNullOrWhiteSpace(customerName) ? $"Customer #{ssn}" : customerName.Trim();

        BackColor = AppTheme.ContentBg;

        var title = new Label
        {
            AutoSize = true,
            Text = $"Welcome, {this.customerName}!",
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = AppTheme.TextPrimary,
            Location = new Point(32, 28)
        };

        var summaryPanel = new FlowLayoutPanel
        {
            Location = new Point(32, 90),
            Size = new Size(920, 140),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.Transparent,
            WrapContents = false,
            AutoScroll = false,
            FlowDirection = FlowDirection.LeftToRight
        };

        var activeRentalsCard = CreateSummaryCard("Active Rentals", out lblActiveRentalsValue);
        var pendingPaymentsCard = CreateSummaryCard("Pending Payments", out lblPendingPaymentsValue);
        var availableCarsCard = CreateSummaryCard("Available Cars", out lblAvailableCarsValue);

        summaryPanel.Controls.Add(activeRentalsCard);
        summaryPanel.Controls.Add(pendingPaymentsCard);
        summaryPanel.Controls.Add(availableCarsCard);

        Controls.Add(title);
        Controls.Add(summaryPanel);

        Load += CustomerHomeControl_Load;
    }

    private void CustomerHomeControl_Load(object? sender, EventArgs e)
    {
        try
        {
            LoadSummary();
        }
        catch
        {
            lblActiveRentalsValue.Text = "0";
            lblPendingPaymentsValue.Text = "0";
            lblAvailableCarsValue.Text = "0";
        }
    }

    private void LoadSummary()
    {
        var rentalsTable = db.GetCustomerRentals(ssn);
        var activeRentals = 0;

        foreach (DataRow row in rentalsTable.Rows)
        {
            var status = row.Table.Columns.Contains("Status_") ? row["Status_"]?.ToString() : string.Empty;
            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
            {
                activeRentals++;
            }
        }

        var pendingPayments = db.GetPendingPayments(ssn).Rows.Count;
        var availableCars = db.GetAvailableCars().Rows.Count;

        lblActiveRentalsValue.Text = activeRentals.ToString();
        lblPendingPaymentsValue.Text = pendingPayments.ToString();
        lblAvailableCarsValue.Text = availableCars.ToString();
    }

    private static Panel CreateSummaryCard(string title, out Label valueLabel)
    {
        var panel = new Panel
        {
            BackColor = AppTheme.CardBg,
            Size = new Size(220, 110),
            Margin = new Padding(0, 0, 18, 0)
        };

        panel.Paint += CardPanel_Paint;

        var titleLabel = new Label
        {
            AutoSize = true,
            Text = title,
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            ForeColor = AppTheme.TextMuted,
            Location = new Point(16, 16)
        };

        valueLabel = new Label
        {
            AutoSize = true,
            Text = "0",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = AppTheme.TextPrimary,
            Location = new Point(16, 44)
        };

        panel.Controls.Add(titleLabel);
        panel.Controls.Add(valueLabel);
        return panel;
    }

    private static void CardPanel_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is not Panel panel)
        {
            return;
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var pen = new Pen(AppTheme.CardBorder);
        var rectangle = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
        e.Graphics.DrawRectangle(pen, rectangle);
    }
}