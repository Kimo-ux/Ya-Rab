using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using CarRental.DB;

namespace CarRental;

public class CustomerPaymentsControl : UserControl
{
    private readonly int ssn;
    private readonly CustomerPortalDB db = new CustomerPortalDB();
    private readonly Panel cardPanel;
    private readonly DataGridView dgvPayments;
    private readonly Label lblTotal;

    public CustomerPaymentsControl(int ssn)
    {
        this.ssn = ssn;
        BackColor = AppTheme.ContentBg;
        DoubleBuffered = true;

        var title = new Label
        {
            AutoSize = true,
            Text = "Pending Payments",
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = AppTheme.TextPrimary,
            Location = new Point(32, 28)
        };

        cardPanel = new Panel
        {
            BackColor = AppTheme.CardBg,
            Location = new Point(32, 88),
            Size = new Size(920, 460),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Padding = new Padding(1)
        };
        cardPanel.Paint += CardPanel_Paint;

        dgvPayments = new DataGridView
        {
            Dock = DockStyle.Fill,
            BackgroundColor = AppTheme.CardBg,
            AutoGenerateColumns = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            ReadOnly = true,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false
        };

        AppTheme.StyleDataGridView(dgvPayments);
        dgvPayments.DataBindingComplete += DgvPayments_DataBindingComplete;

        lblTotal = new Label
        {
            AutoSize = true,
            ForeColor = AppTheme.Accent,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            Location = new Point(32, 560),
            Text = "Total Outstanding: $0.00"
        };

        cardPanel.Controls.Add(dgvPayments);

        Controls.Add(title);
        Controls.Add(cardPanel);
        Controls.Add(lblTotal);

        Load += CustomerPaymentsControl_Load;
        Resize += CustomerPaymentsControl_Resize;
    }

    private void CustomerPaymentsControl_Load(object? sender, EventArgs e)
    {
        LoadPayments();
    }

    private void CustomerPaymentsControl_Resize(object? sender, EventArgs e)
    {
        cardPanel.Invalidate();
    }

    private void LoadPayments()
    {
        dgvPayments.DataSource = db.GetCustomerRentals(ssn);
        UpdateTotalLabel();
    }

    private void DgvPayments_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        UpdateTotalLabel();
    }

    private void UpdateTotalLabel()
    {
        var total = 0m;

        if (dgvPayments.Columns.Contains("Amount"))
        {
            foreach (DataGridViewRow row in dgvPayments.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var value = row.Cells["Amount"].Value;
                if (value == null || value == DBNull.Value)
                {
                    continue;
                }

                if (decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out var amount) ||
                    decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                {
                    total += amount;
                }
            }
        }

        lblTotal.Text = $"Total Outstanding: {total:C}";
    }

    private void CardPanel_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var pen = new Pen(AppTheme.CardBorder);
        var rectangle = new Rectangle(0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
        e.Graphics.DrawRectangle(pen, rectangle);
    }
}