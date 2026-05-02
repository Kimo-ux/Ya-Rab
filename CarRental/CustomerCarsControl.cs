using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CarRental.DB;

namespace CarRental;

public class CustomerCarsControl : UserControl
{
    private readonly CustomerPortalDB db = new CustomerPortalDB();
    private readonly Panel cardPanel;
    private readonly DataGridView dgvCars;

    public CustomerCarsControl()
    {
        BackColor = AppTheme.ContentBg;
        DoubleBuffered = true;

        var title = new Label
        {
            AutoSize = true,
            Text = "Available Cars",
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = AppTheme.TextPrimary,
            Location = new Point(32, 28)
        };

        cardPanel = new Panel
        {
            BackColor = AppTheme.CardBg,
            Location = new Point(32, 80),
            Size = new Size(920, 460),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            Padding = new Padding(1)
        };
        cardPanel.Paint += CardPanel_Paint;

        dgvCars = new DataGridView
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

        AppTheme.StyleDataGridView(dgvCars);
        dgvCars.DataBindingComplete += DgvCars_DataBindingComplete;

        cardPanel.Controls.Add(dgvCars);

        Controls.Add(cardPanel);
        Controls.Add(title);

        Load += CustomerCarsControl_Load;
        Resize += CustomerCarsControl_Resize;
    }

    private void CustomerCarsControl_Load(object? sender, EventArgs e)
    {
        LoadAvailableCars();
    }

    private void CustomerCarsControl_Resize(object? sender, EventArgs e)
    {
        cardPanel.Invalidate();
    }

    private void LoadAvailableCars()
    {
        dgvCars.DataSource = db.GetAvailableCars();
        ApplyGridConfiguration();
    }

    private void DgvCars_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        ApplyGridConfiguration();
    }

    private void ApplyGridConfiguration()
    {
        if (dgvCars.Columns.Count == 0)
        {
            return;
        }

        SetHeaderIfExists("Family", "Model");
        SetHeaderIfExists("VType", "Type");
        SetHeaderIfExists("VYear", "Year");
        SetHeaderIfExists("DailyRate", "Rate/day");
        SetHeaderIfExists("Mileage", "Mileage");

        SetVisibleIfExists("Family", true);
        SetVisibleIfExists("VType", true);
        SetVisibleIfExists("VYear", true);
        SetVisibleIfExists("DailyRate", true);
        SetVisibleIfExists("Mileage", true);

        foreach (DataGridViewColumn column in dgvCars.Columns)
        {
            if (column.Name is "Family" or "VType" or "VYear" or "DailyRate" or "Mileage")
            {
                continue;
            }

            column.Visible = false;
        }
    }

    private void SetHeaderIfExists(string columnName, string headerText)
    {
        if (dgvCars.Columns.Contains(columnName))
        {
            dgvCars.Columns[columnName].HeaderText = headerText;
        }
    }

    private void SetVisibleIfExists(string columnName, bool visible)
    {
        if (dgvCars.Columns.Contains(columnName))
        {
            dgvCars.Columns[columnName].Visible = visible;
        }
    }

    private void CardPanel_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var pen = new Pen(AppTheme.CardBorder);
        var rectangle = new Rectangle(0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
        e.Graphics.DrawRectangle(pen, rectangle);
    }
}