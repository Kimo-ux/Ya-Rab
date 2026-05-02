using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CarRental.DB;
using Microsoft.Data.SqlClient;

namespace CarRental;

public class CustomerRentalsControl : UserControl
{
    private readonly int ssn;
    private readonly CustomerPortalDB db = new CustomerPortalDB();
    private readonly Panel cardPanel;
    private readonly DataGridView dgvRentals;

    public CustomerRentalsControl(int ssn)
    {
        this.ssn = ssn;
        BackColor = AppTheme.ContentBg;
        DoubleBuffered = true;

        var title = new Label
        {
            AutoSize = true,
            Text = "My Rentals",
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

        dgvRentals = new DataGridView
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

        AppTheme.StyleDataGridView(dgvRentals);
        dgvRentals.DataBindingComplete += DgvRentals_DataBindingComplete;
        dgvRentals.CellClick += DgvRentals_CellClick;
        dgvRentals.CellFormatting += DgvRentals_CellFormatting;

        cardPanel.Controls.Add(dgvRentals);

        Controls.Add(title);
        Controls.Add(cardPanel);

        Load += CustomerRentalsControl_Load;
        Resize += CustomerRentalsControl_Resize;
    }

    private void CustomerRentalsControl_Load(object? sender, EventArgs e)
    {
        LoadRentals();
    }

    private void CustomerRentalsControl_Resize(object? sender, EventArgs e)
    {
        cardPanel.Invalidate();
    }

    private void LoadRentals()
    {
        dgvRentals.DataSource = db.GetCustomerRentals(ssn);
        ApplyGridConfiguration();
        ApplyRowColors();
        AddCancelButtonColumn();
    }

    private void AddCancelButtonColumn()
    {
        if (dgvRentals.Columns["colCancel"] == null)
        {
            var colCancel = new DataGridViewButtonColumn
            {
                Name = "colCancel",
                HeaderText = "",
                Text = "Cancel",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            dgvRentals.Columns.Add(colCancel);
        }
    }

    private void DgvRentals_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        ApplyGridConfiguration();
        ApplyRowColors();
    }

    private void ApplyGridConfiguration()
    {
        if (dgvRentals.Columns.Count == 0)
        {
            return;
        }

        SetHeaderIfExists("RentalID", "ID");
        SetHeaderIfExists("VehicleModel", "Vehicle");
        SetHeaderIfExists("StartDate", "Start");
        SetHeaderIfExists("EndDate", "Return");
        SetHeaderIfExists("Status_", "Status");

        foreach (DataGridViewColumn column in dgvRentals.Columns)
        {
            if (column.Name is "RentalID" or "VehicleModel" or "StartDate" or "EndDate" or "Status_")
            {
                continue;
            }

            column.Visible = false;
        }
    }

    private void ApplyRowColors()
    {
        if (dgvRentals.Rows.Count == 0 || !dgvRentals.Columns.Contains("Status_"))
        {
            return;
        }

        foreach (DataGridViewRow row in dgvRentals.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            var status = row.Cells["Status_"].Value?.ToString();
            var color = status switch
            {
                "Active" => AppTheme.BadgeGreenText,
                "Overdue" => AppTheme.BadgeRedText,
                _ => AppTheme.BadgeAmberText
            };

            row.DefaultCellStyle.ForeColor = color;
            row.DefaultCellStyle.SelectionForeColor = color;
        }
    }

    private void SetHeaderIfExists(string columnName, string headerText)
    {
        if (dgvRentals.Columns.Contains(columnName))
        {
            dgvRentals.Columns[columnName].HeaderText = headerText;
        }
    }

    private void CardPanel_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var pen = new Pen(AppTheme.CardBorder);
        var rectangle = new Rectangle(0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
        e.Graphics.DrawRectangle(pen, rectangle);
    }

    private void DgvRentals_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
        {
            return;
        }

        var column = dgvRentals.Columns[e.ColumnIndex];
        if (column.Name == "colCancel")
        {
            if (dgvRentals.Rows[e.RowIndex].Cells["RentalID"].Value is not int rentalID)
            {
                return;
            }

            var result = MessageBox.Show(
                "Cancel this rental?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CancelRental(rentalID);
            }
        }
    }

    private void DgvRentals_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.RowIndex < 0)
        {
            return;
        }

        var column = dgvRentals.Columns[e.ColumnIndex];
        if (column.Name == "colCancel")
        {
            var status = dgvRentals.Rows[e.RowIndex].Cells["Status_"].Value?.ToString();
            if (status != "Active")
            {
                e.Value = string.Empty;
            }
        }
    }

    private void CancelRental(int rentalID)
    {
        try
        {
            using var conn = new SqlConnection(DBHelper.ConnStr);
            using var cmd = new SqlCommand("CancelRental", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@RentalID", rentalID);
            conn.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show(
                "Rental cancelled.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LoadRentals();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error cancelling rental: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}