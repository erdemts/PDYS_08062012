namespace PDYS.Report.Design
{
    partial class RptInputOutput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.txtEmployee = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtTime = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtActionType = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtIsScoring = new DevExpress.XtraReports.UI.XRTableCell();
            this.txtDevice = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblEmployee = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblTime = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblActionType = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblScoring = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblDevice = new DevExpress.XtraReports.UI.XRTableCell();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(648.9584F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.txtEmployee,
            this.txtDate,
            this.txtTime,
            this.txtActionType,
            this.txtIsScoring,
            this.txtDevice});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // txtEmployee
            // 
            this.txtEmployee.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeName")});
            this.txtEmployee.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtEmployee.StylePriority.UseFont = false;
            this.txtEmployee.StylePriority.UsePadding = false;
            this.txtEmployee.StylePriority.UseTextAlignment = false;
            this.txtEmployee.Text = "txtEmployee";
            this.txtEmployee.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.txtEmployee.Weight = 1D;
            // 
            // txtDate
            // 
            this.txtDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Date")});
            this.txtDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Name = "txtDate";
            this.txtDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtDate.StylePriority.UseFont = false;
            this.txtDate.StylePriority.UsePadding = false;
            this.txtDate.StylePriority.UseTextAlignment = false;
            this.txtDate.Text = "txtDate";
            this.txtDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.txtDate.Weight = 1D;
            // 
            // txtTime
            // 
            this.txtTime.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Time")});
            this.txtTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Name = "txtTime";
            this.txtTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtTime.StylePriority.UseFont = false;
            this.txtTime.StylePriority.UsePadding = false;
            this.txtTime.StylePriority.UseTextAlignment = false;
            this.txtTime.Text = "txtTime";
            this.txtTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.txtTime.Weight = 0.63315922728767471D;
            // 
            // txtActionType
            // 
            this.txtActionType.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ActionType")});
            this.txtActionType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActionType.Name = "txtActionType";
            this.txtActionType.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtActionType.StylePriority.UseFont = false;
            this.txtActionType.StylePriority.UsePadding = false;
            this.txtActionType.StylePriority.UseTextAlignment = false;
            this.txtActionType.Text = "txtActionType";
            this.txtActionType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.txtActionType.Weight = 0.7401824682761895D;
            // 
            // txtIsScoring
            // 
            this.txtIsScoring.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "IsScoring")});
            this.txtIsScoring.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIsScoring.Name = "txtIsScoring";
            this.txtIsScoring.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtIsScoring.StylePriority.UseFont = false;
            this.txtIsScoring.StylePriority.UsePadding = false;
            this.txtIsScoring.StylePriority.UseTextAlignment = false;
            this.txtIsScoring.Text = "txtIsScoring";
            this.txtIsScoring.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.txtIsScoring.Weight = 0.76459058918614886D;
            // 
            // txtDevice
            // 
            this.txtDevice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Device")});
            this.txtDevice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 100F);
            this.txtDevice.StylePriority.UseFont = false;
            this.txtDevice.StylePriority.UsePadding = false;
            this.txtDevice.StylePriority.UseTextAlignment = false;
            this.txtDevice.Text = "txtDevice";
            this.txtDevice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.txtDevice.Weight = 0.74196345815408127D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 27.08334F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(648.9583F, 72.91666F);
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Personel Giriş & Çıkış Raporu";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.Weight = 4.8798953633218964D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblEmployee,
            this.lblDate,
            this.lblTime,
            this.lblActionType,
            this.lblScoring,
            this.lblDevice});
            this.xrTableRow1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseFont = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow1.Weight = 1D;
            // 
            // lblEmployee
            // 
            this.lblEmployee.CanGrow = false;
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Text = "Personel";
            this.lblEmployee.Weight = 1D;
            // 
            // lblDate
            // 
            this.lblDate.CanGrow = false;
            this.lblDate.Name = "lblDate";
            this.lblDate.Text = "Tarih";
            this.lblDate.Weight = 1D;
            // 
            // lblTime
            // 
            this.lblTime.CanGrow = false;
            this.lblTime.Name = "lblTime";
            this.lblTime.Text = "Saat";
            this.lblTime.Weight = 0.6331593420273911D;
            // 
            // lblActionType
            // 
            this.lblActionType.CanGrow = false;
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Text = "Hareket Tipi";
            this.lblActionType.Weight = 0.74018275512548048D;
            // 
            // lblScoring
            // 
            this.lblScoring.CanGrow = false;
            this.lblScoring.Name = "lblScoring";
            this.lblScoring.Text = "Puantaj";
            this.lblScoring.Weight = 0.76459014141471437D;
            // 
            // lblDevice
            // 
            this.lblDevice.CanGrow = false;
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Text = "Okuyucu";
            this.lblDevice.Weight = 0.7419631247543107D;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 44.16666F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(PDYS.Report.Model.RptInputOutputModel);
            // 
            // RptInputOutput
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataSource = this.bindingSource1;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 44);
            this.Version = "11.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell txtEmployee;
        private DevExpress.XtraReports.UI.XRTableCell txtDate;
        private DevExpress.XtraReports.UI.XRTableCell txtTime;
        private DevExpress.XtraReports.UI.XRTableCell txtActionType;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell lblEmployee;
        private DevExpress.XtraReports.UI.XRTableCell lblDate;
        private DevExpress.XtraReports.UI.XRTableCell lblTime;
        private DevExpress.XtraReports.UI.XRTableCell lblActionType;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRTableCell txtIsScoring;
        private DevExpress.XtraReports.UI.XRTableCell txtDevice;
        private DevExpress.XtraReports.UI.XRTableCell lblScoring;
        private DevExpress.XtraReports.UI.XRTableCell lblDevice;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
    }
}
