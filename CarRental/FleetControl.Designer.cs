using System.Drawing;
using System.Windows.Forms;

namespace CarRental;

partial class FleetControl
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlTopBar;
    private Label lblTitle;
    private Button btnAddVehicle;
    private Panel pnlSearch;
    private TextBox txtSearch;
    private ComboBox cmbStatus;
    private ComboBox cmbType;
    private Button btnSearch;
    private Panel pnlContent;
    private Panel pnlGrid;
    private DataGridView dgvCars;
    private Panel pnlActions;
    private Button btnEdit;
    private Button btnDelete;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlTopBar = new Panel();
        lblTitle = new Label();
        btnAddVehicle = new Button();
        pnlSearch = new Panel();
        txtSearch = new TextBox();
        cmbStatus = new ComboBox();
        cmbType = new ComboBox();
        btnSearch = new Button();
        pnlContent = new Panel();
        pnlGrid = new Panel();
        dgvCars = new DataGridView();
        pnlActions = new Panel();
        btnEdit = new Button();
        btnDelete = new Button();
        pnlTopBar.SuspendLayout();
        pnlSearch.SuspendLayout();
        pnlContent.SuspendLayout();
        pnlGrid.SuspendLayout();
        pnlActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCars).BeginInit();
        SuspendLayout();
        // 
        // pnlTopBar
        // 
        pnlTopBar.BackColor = Color.FromArgb(15, 15, 22);
        pnlTopBar.Controls.Add(btnAddVehicle);
        pnlTopBar.Controls.Add(lblTitle);
        pnlTopBar.Dock = DockStyle.Top;
        pnlTopBar.Location = new Point(0, 0);
        pnlTopBar.Name = "pnlTopBar";
        pnlTopBar.Size = new Size(1103, 72);
        pnlTopBar.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.ForeColor = Color.FromArgb(230, 230, 230);
        lblTitle.Location = new Point(24, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(148, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Fleet / Cars";
        // 
        // btnAddVehicle
        // 
        btnAddVehicle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAddVehicle.BackColor = Color.FromArgb(0, 210, 190);
        btnAddVehicle.FlatAppearance.BorderSize = 0;
        btnAddVehicle.FlatStyle = FlatStyle.Flat;
        btnAddVehicle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        btnAddVehicle.ForeColor = Color.FromArgb(10, 10, 15);
        btnAddVehicle.Location = new Point(931, 16);
        btnAddVehicle.Name = "btnAddVehicle";
        btnAddVehicle.Size = new Size(148, 40);
        btnAddVehicle.TabIndex = 1;
        btnAddVehicle.Text = "+ Add Vehicle";
        btnAddVehicle.UseVisualStyleBackColor = false;
        btnAddVehicle.Click += btnAddVehicle_Click;
        // 
        // pnlSearch
        // 
        pnlSearch.BackColor = Color.FromArgb(12, 12, 18);
        pnlSearch.Controls.Add(btnSearch);
        pnlSearch.Controls.Add(cmbType);
        pnlSearch.Controls.Add(cmbStatus);
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Dock = DockStyle.Top;
        pnlSearch.Location = new Point(0, 72);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Padding = new Padding(24, 12, 24, 12);
        pnlSearch.Size = new Size(1103, 56);
        pnlSearch.TabIndex = 1;
        // 
        // txtSearch
        // 
        txtSearch.BackColor = Color.FromArgb(22, 22, 32);
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 9F, GraphicsUnit.Point);
        txtSearch.ForeColor = Color.FromArgb(220, 220, 220);
        txtSearch.Location = new Point(24, 12);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search model, plate, type...";
        txtSearch.Size = new Size(260, 32);
        txtSearch.TabIndex = 0;
        // 
        // cmbStatus
        // 
        cmbStatus.BackColor = Color.FromArgb(22, 22, 32);
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Font = new Font("Segoe UI", 9F, GraphicsUnit.Point);
        cmbStatus.ForeColor = Color.FromArgb(220, 220, 220);
        cmbStatus.FormattingEnabled = true;
        cmbStatus.Items.AddRange(new object[] { "All Status", "Available", "Rented", "Maintenance" });
        cmbStatus.Location = new Point(300, 12);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(140, 32);
        cmbStatus.TabIndex = 1;
        // 
        // cmbType
        // 
        cmbType.BackColor = Color.FromArgb(22, 22, 32);
        cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbType.Font = new Font("Segoe UI", 9F, GraphicsUnit.Point);
        cmbType.ForeColor = Color.FromArgb(220, 220, 220);
        cmbType.FormattingEnabled = true;
        cmbType.Items.AddRange(new object[] { "All Types", "Sedan", "SUV", "Luxury" });
        cmbType.Location = new Point(456, 12);
        cmbType.Name = "cmbType";
        cmbType.Size = new Size(130, 32);
        cmbType.TabIndex = 2;
        // 
        // btnSearch
        // 
        btnSearch.BackColor = Color.FromArgb(0, 210, 190);
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        btnSearch.ForeColor = Color.FromArgb(10, 10, 15);
        btnSearch.Location = new Point(602, 12);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 32);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = false;
        btnSearch.Click += btnSearch_Click;
        // 
        // pnlContent
        // 
        pnlContent.BackColor = Color.FromArgb(15, 15, 22);
        pnlContent.Controls.Add(pnlGrid);
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(0, 128);
        pnlContent.Name = "pnlContent";
        pnlContent.Padding = new Padding(24, 16, 24, 20);
        pnlContent.Size = new Size(1103, 779);
        pnlContent.TabIndex = 2;
        // 
        // pnlGrid
        // 
        pnlGrid.BackColor = Color.FromArgb(22, 22, 32);
        pnlGrid.BorderStyle = BorderStyle.None;
        pnlGrid.Controls.Add(dgvCars);
        pnlGrid.Dock = DockStyle.Fill;
        pnlGrid.Location = new Point(24, 16);
        pnlGrid.Name = "pnlGrid";
        pnlGrid.Size = new Size(1055, 743);
        pnlGrid.TabIndex = 0;
        // 
        // dgvCars
        // 
        dgvCars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCars.Dock = DockStyle.Fill;
        dgvCars.Location = new Point(0, 0);
        dgvCars.Name = "dgvCars";
        dgvCars.RowHeadersWidth = 51;
        dgvCars.RowTemplate.Height = 46;
        dgvCars.Size = new Size(1055, 743);
        dgvCars.TabIndex = 0;
        dgvCars.SelectionChanged += dgvCars_SelectionChanged;
        // 
        // pnlActions
        // 
        pnlActions.BackColor = Color.FromArgb(15, 15, 22);
        pnlActions.Controls.Add(btnEdit);
        pnlActions.Controls.Add(btnDelete);
        pnlActions.Location = new Point(0, 0);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(0, 0);
        pnlActions.TabIndex = 3;
        // 
        // btnEdit
        // 
        btnEdit.BackColor = Color.FromArgb(0, 210, 190);
        btnEdit.Enabled = false;
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.ForeColor = Color.FromArgb(10, 10, 15);
        btnEdit.Location = new Point(9, 11);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(114, 48);
        btnEdit.TabIndex = 0;
        btnEdit.Text = "✏ Edit";
        btnEdit.UseVisualStyleBackColor = false;
        btnEdit.Click += btnEdit_Click;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.FromArgb(0, 210, 190);
        btnDelete.Enabled = false;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.ForeColor = Color.FromArgb(10, 10, 15);
        btnDelete.Location = new Point(133, 11);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(114, 48);
        btnDelete.TabIndex = 1;
        btnDelete.Text = "🗑 Delete";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // FleetControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(15, 15, 22);
        Controls.Add(pnlContent);
        Controls.Add(pnlSearch);
        Controls.Add(pnlTopBar);
        Name = "FleetControl";
        Size = new Size(1103, 907);
        pnlTopBar.ResumeLayout(false);
        pnlTopBar.PerformLayout();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        pnlContent.ResumeLayout(false);
        pnlGrid.ResumeLayout(false);
        pnlActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCars).EndInit();
        ResumeLayout(false);
    }
}
